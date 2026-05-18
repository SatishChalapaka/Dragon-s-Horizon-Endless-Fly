using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
//using EasyMobile;
public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;
    public GameObject rowPrefab;
    public Transform rowsParent;

    public GameObject nameWindow;
    public GameObject leaderboardWindow;

    public GameObject nameError;
    public InputField nameInput;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Login();
        if (PlayerPrefs.GetInt("FirstTimeOpen", 0) == 0)
        {
            nameWindow.SetActive(true);
            
        }
        else
        {
            nameWindow.SetActive(false);
        }
    }
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
            new StatisticUpdate{
                StatisticName="Score",
                Value=score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    public void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }
    public void GetLeaderboard()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //NativeUI.AlertPopup alert = NativeUI.Alert("Network", "No Internet Access");
            return;
        }
        leaderboardWindow.SetActive(true);
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + "" + item.DisplayName + "" + item.StatValue);
        }
    }
    public void SubmitNameButton()
    {
        if (nameInput.GetComponent<InputField>().text == "")
        {
            //NativeUI.AlertPopup alert = NativeUI.Alert("Error", "Enter Your Name");
        }
        else
        {
            nameWindow.SetActive(false);
            PlayerPrefs.SetInt("FirstTimeOpen", 1);
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameInput.text,
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);

        }


    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
    }
}
