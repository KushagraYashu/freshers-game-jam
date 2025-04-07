using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    [Header("UI Elements")]
    [SerializeField] Transform leaderboardParent;
    [SerializeField] List<TextMeshProUGUI> ranks;
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> times;

    //internal variables
    private string leaderboardID = "WrongFloorSpeedrunLB";
    LeaderboardScoresPage leaderBoard;

    TimeSpan seconds;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SignIn(string name)
    {
        StartCoroutine(SignInCoroutine(name));
    }

    IEnumerator SignInCoroutine(string name)
    {
        yield return new WaitUntil(() => SignInWithName(name) == Task.CompletedTask);
    }

    public string TruncateAtHash(string input)
    {
        int index = input.IndexOf('#');

        if (index != -1)
        {
            return input.Substring(0, index);
        }

        return input;
    }

    public async Task SignInWithName(string playerName)
    {
        await UnityServices.InitializeAsync();

        if (AuthenticationService.Instance.IsAuthorized)
        {
            var curName = TruncateAtHash(AuthenticationService.Instance.PlayerName);
            if (curName == playerName)
                return;
            else
                AuthenticationService.Instance.SignOut();
        }

        AuthenticationService.Instance.ClearSessionToken();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        Debug.Log("Signed in as new player: " + AuthenticationService.Instance.PlayerId);

        string safeName = playerName.Replace(" ", "_");

        await AuthenticationService.Instance.UpdatePlayerNameAsync(safeName);

        Debug.LogError("Signed in with the name " + safeName);
    }

    async Task AddScore(float time)
    {
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID,
            time);
    }

    public async void ShowLeaderboard(string name, float time)
    {
        await SignInWithName(name);
        await AddScore(time);
        await UpdateLeaderboard();

        leaderboardParent.gameObject.SetActive(true);
    }

    async Task UpdateLeaderboard()
    {
        leaderBoard = await LeaderboardsService.Instance.GetScoresAsync(
            leaderboardID, 
            new GetScoresOptions { Offset = 0, Limit = 10 });

        i = 0;
        foreach(LeaderboardEntry entry in leaderBoard.Results)
        {
            ranks[i].text = "" + (i + 1);
            names[i].text = entry.PlayerName;
            seconds = TimeSpan.FromSeconds(entry.Score);
            times[i].text = seconds.ToString("mm':'ss'.'fff");

            i++;
        }
    }

    public void HideLeaderboard()
    {
        leaderboardParent.gameObject.SetActive(false);

        LevelManager.instance.EnablePlayerControls();
    }
}
