using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Challenges : MonoBehaviour
{
    [Serializable]
    public struct ChallengesData
    {
        public GameObject Challenge1_1GameObject;
        public Slider Challenge1_1_Slider;
        public Button Challenge1_1_Button;
        public int challenge1_1_targetValues;
        public int diamondValues;
        public float challenge1_1_AchieveValues;
        public Text challenge1_1_targetValueText;
    }
    [Serializable]
    public struct ChallengesData2
    {
        public GameObject Challenge2_1GameObject;
        public Slider Challenge2_1_Slider;
        public Button Challenge2_1_Button;
        public int challenge2_1_targetValues;
        public int diamondValues;
        public int challenge2_1_AchieveValues;
        public Text challenge2_1_targetValueText;
    }
    [Serializable]
    public struct ChallengesData3
    {
        public GameObject Challenge3_1GameObject;
        public Slider Challenge3_1_Slider;
        public Button Challenge3_1_Button;
        public int challenge3_1_targetValues;
        public int diamondValues;
        public int challenge3_1_AchieveValues;
        public Text challenge3_1_targetValueText;
    }
    [Serializable]
    public struct ChallengesData4
    {
        public GameObject Challenge4_1GameObject;
        public Slider Challenge4_1_Slider;
        public Button Challenge4_1_Button;
        public int challenge4_1_targetValues;
        public int diamondValues;
        public int challenge4_1_AchieveValues;
        public Text challenge4_1_targetValueText;
    }
    [Serializable]
    public struct ChallengesData5
    {
        public GameObject Challenge5_1GameObject;
        public Slider Challenge5_1_Slider;
        public Button Challenge5_1_Button;
        public int challenge5_1_targetValues;
        public int diamondValues;
        public int challenge5_1_AchieveValues;
        public Text challenge5_1_targetValueText;
    }
    [Serializable]
    public struct ChallengesData6
    {
        public GameObject Challenge6_1GameObject;
        public Slider Challenge6_1_Slider;
        public Button Challenge6_1_Button;
        public int challenge6_1_targetValues;
        public int diamondValues;
        public int challenge6_1_AchieveValues;
        public Text challenge6_1_targetValueText;
    }
    [Serializable]
    public struct ChallengesData7
    {
        public GameObject Challenge7_1GameObject;
        public Slider Challenge7_1_Slider;
        public Button Challenge7_1_Button;
        public int challenge7_1_targetValues;
        public int diamondValues;
        public int challenge7_1_AchieveValues;
        public Text challenge7_1_targetValueText;
    }
    public static Challenges instance;
    public List<ChallengesData> challengesData = new List<ChallengesData>();
    public List<ChallengesData2> challengesData2 = new List<ChallengesData2>();
    public List<ChallengesData3> challengesData3 = new List<ChallengesData3>();
    public List<ChallengesData4> challengesData4 = new List<ChallengesData4>();
    public List<ChallengesData5> challengesData5 = new List<ChallengesData5>();
    public List<ChallengesData6> challengesData6 = new List<ChallengesData6>();
    public List<ChallengesData7> challengesData7 = new List<ChallengesData7>();
    public int challenge1SubCompleted;
    public int challenge2SubCompleted;
    public int challenge3SubCompleted;
    public int challenge4SubCompleted;
    public int challenge5SubCompleted;
    public int challenge6SubCompleted;
    public int challenge7SubCompleted;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        challenge1SubCompleted = PlayerPrefs.GetInt("challenge1SubCompleted");
        challenge2SubCompleted = PlayerPrefs.GetInt("challenge2SubCompleted");
        challenge3SubCompleted = PlayerPrefs.GetInt("challenge3SubCompleted");
        challenge4SubCompleted = PlayerPrefs.GetInt("challenge4SubCompleted");
        challenge5SubCompleted = PlayerPrefs.GetInt("challenge5SubCompleted");
        challenge6SubCompleted = PlayerPrefs.GetInt("challenge6SubCompleted");
        challenge7SubCompleted = PlayerPrefs.GetInt("challenge7SubCompleted");
        ActivateChallenges1_1();
        CheckChallenge1_1();
        ActivateChallenges2_1();
        CheckChallenge2_1();
        ActivateChallenges3_1();
        CheckChallenge3_1();
        ActivateChallenges4_1();
        CheckChallenge4_1();
        ActivateChallenges5_1();
        CheckChallenge5_1();
        ActivateChallenges6_1();
        CheckChallenge6_1();
        ActivateChallenges7_1();
        CheckChallenge7_1();
    }
    //Challenge 1
    public void ActivateChallenges1_1()
    {
        if (challenge1SubCompleted == challengesData.Count)
        {
            return;
        }
        for (int i = 0; i < challengesData.Count; i++)
        {
            if (i == challenge1SubCompleted)
            {
                challengesData[i].Challenge1_1GameObject.SetActive(true);
            }
            else
            {
                challengesData[i].Challenge1_1GameObject.SetActive(false);
            }
        }
    }
    public void Challenge1_1(float number)
    {
        if (challenge1SubCompleted == challengesData.Count || number < PlayerPrefs.GetFloat("Challenge1_" + challenge1SubCompleted + "AchieveValues"))
        {
            return;
        }
        challengesData[challenge1SubCompleted].Challenge1_1_Slider.value = number / challengesData[challenge1SubCompleted].challenge1_1_targetValues;
        PlayerPrefs.SetFloat("Challenge1_" + challenge1SubCompleted + "AchieveValues", number);
        PlayerPrefs.SetFloat("Challenge1_" + challenge1SubCompleted, challengesData[challenge1SubCompleted].Challenge1_1_Slider.value);
        //if (number >= challengesData[challenge1SubCompleted].challenge1_1_targetValues)
        //{
        //    challenge1SubCompleted++;
        //    PlayerPrefs.SetFloat("Challenge1_" + challenge1SubCompleted + "AchieveValues", number);
        //    PlayerPrefs.SetFloat("Challenge1_" + challenge1SubCompleted, challengesData[challenge1SubCompleted].Challenge1_1_Slider.value);
        //}
    }
    public void CheckChallenge1_1()
    {
        if (challenge1SubCompleted == challengesData.Count)
        {
            return;
        }
        challengesData[challenge1SubCompleted].Challenge1_1_Slider.value = PlayerPrefs.GetFloat("Challenge1_"+ challenge1SubCompleted);
        if (PlayerPrefs.GetFloat("Challenge1_" + challenge1SubCompleted + "AchieveValues") >= challengesData[challenge1SubCompleted].challenge1_1_targetValues)
        {
            PlayerPrefs.SetFloat("Challenge1_" + challenge1SubCompleted + "AchieveValues", challengesData[challenge1SubCompleted].challenge1_1_targetValues);
        }
        challengesData[challenge1SubCompleted].challenge1_1_targetValueText.text = PlayerPrefs.GetFloat("Challenge1_" + challenge1SubCompleted + "AchieveValues") + "/" + challengesData[challenge1SubCompleted].challenge1_1_targetValues;
        challengesData[challenge1SubCompleted].Challenge1_1_Button.interactable = false;
        if (challengesData[challenge1SubCompleted].Challenge1_1_Slider.value >= 1)
        {
            challengesData[challenge1SubCompleted].Challenge1_1_Button.interactable = true;
        }
    }
    public void ClaimChallenge1_1()
    {
        if (challengesData[challenge1SubCompleted].Challenge1_1_Slider.value >= 1)
        {
            ScoreManager.instance.IAPAddDiamondCoinBalance(challengesData[challenge1SubCompleted].diamondValues);
            challengesData[challenge1SubCompleted].Challenge1_1GameObject.SetActive(false);
            PlayerPrefs.SetInt("Challenge1_1Completed", 1);
            challenge1SubCompleted++;
            PlayerPrefs.SetInt("challenge1SubCompleted", challenge1SubCompleted);
            ActivateChallenges1_1();
        }
    }
    // Challenge 2
    public void ActivateChallenges2_1()
    {
        if (challenge2SubCompleted == challengesData2.Count)
        {
            return;
        }
        for (int i = 0; i < challengesData2.Count; i++)
        {
            if (i == challenge2SubCompleted)
            {
                challengesData2[i].Challenge2_1GameObject.SetActive(true);
            }
            else
            {
                challengesData2[i].Challenge2_1GameObject.SetActive(false);
            }
        }
    }
    public float challenge2_1_AchieveValues;
    public void Challenge2_1(float number)
    {
        if (challenge2SubCompleted == challengesData2.Count)
        {
            return;
        }
        PlayerPrefs.SetFloat("Challenge2_1_AchieveValuesCoins", (PlayerPrefs.GetFloat("Challenge2_1_AchieveValuesCoins") + number));
        challenge2_1_AchieveValues = PlayerPrefs.GetFloat("Challenge2_1_AchieveValuesCoins");
        challengesData2[challenge2SubCompleted].Challenge2_1_Slider.value = challenge2_1_AchieveValues / challengesData2[challenge2SubCompleted].challenge2_1_targetValues;
        PlayerPrefs.SetFloat("Challenge2_" + challenge2SubCompleted + "AchieveValues", challenge2_1_AchieveValues);
        PlayerPrefs.SetFloat("Challenge2_" + challenge2SubCompleted, challengesData2[challenge2SubCompleted].Challenge2_1_Slider.value);
    }
    public void CheckChallenge2_1()
    {
        if (challenge2SubCompleted == challengesData2.Count)
        {
            return;
        }
        challengesData2[challenge2SubCompleted].Challenge2_1_Slider.value = PlayerPrefs.GetFloat("Challenge2_" + challenge2SubCompleted);
        if (PlayerPrefs.GetFloat("Challenge2_" + challenge2SubCompleted + "AchieveValues") >= challengesData2[challenge2SubCompleted].challenge2_1_targetValues)
        {
            PlayerPrefs.SetFloat("Challenge2_" + challenge2SubCompleted + "AchieveValues", challengesData2[challenge2SubCompleted].challenge2_1_targetValues);
        }
        challengesData2[challenge2SubCompleted].challenge2_1_targetValueText.text = PlayerPrefs.GetFloat("Challenge2_" + challenge2SubCompleted + "AchieveValues") + "/" + challengesData2[challenge2SubCompleted].challenge2_1_targetValues;
        challengesData2[challenge2SubCompleted].Challenge2_1_Button.interactable = false;
        if (challengesData2[challenge2SubCompleted].Challenge2_1_Slider.value >= 1)
        {
            challengesData2[challenge2SubCompleted].Challenge2_1_Button.interactable = true;
        }
    }
    public void ClaimChallenge2_1()
    {
        if (challengesData2[challenge2SubCompleted].Challenge2_1_Slider.value >= 1)
        {
            ScoreManager.instance.IAPAddDiamondCoinBalance(challengesData2[challenge2SubCompleted].diamondValues);
            challengesData2[challenge2SubCompleted].Challenge2_1GameObject.SetActive(false);
            PlayerPrefs.SetInt("Challenge2_1Completed", 1);
            challenge2SubCompleted++;
            PlayerPrefs.SetFloat("Challenge2_1_AchieveValuesCoins", 0);
            PlayerPrefs.SetInt("challenge2SubCompleted", challenge2SubCompleted);
            ActivateChallenges2_1();
        }
    }
    //Challeneg 3
    public void ActivateChallenges3_1()
    {
        if (challenge3SubCompleted == challengesData3.Count)
        {
            return;
        }
        for (int i = 0; i < challengesData3.Count; i++)
        {
            if (i == challenge3SubCompleted)
            {
                challengesData3[i].Challenge3_1GameObject.SetActive(true);
            }
            else
            {
                challengesData3[i].Challenge3_1GameObject.SetActive(false);
            }
        }
    }
    public void Challenge3_1(float number)
    {
        if (challenge3SubCompleted == challengesData3.Count || number < PlayerPrefs.GetFloat("Challenge3_" + challenge3SubCompleted + "AchieveValues"))
        {
            return;
        }
        challengesData3[challenge3SubCompleted].Challenge3_1_Slider.value = number / challengesData3[challenge3SubCompleted].challenge3_1_targetValues;
        PlayerPrefs.SetFloat("Challenge3_" + challenge3SubCompleted + "AchieveValues", number);
        PlayerPrefs.SetFloat("Challenge3_" + challenge3SubCompleted, challengesData3[challenge3SubCompleted].Challenge3_1_Slider.value);
    }
    public void CheckChallenge3_1()
    {
        if (challenge3SubCompleted == challengesData3.Count)
        {
            return;
        }
        challengesData3[challenge3SubCompleted].Challenge3_1_Slider.value = PlayerPrefs.GetFloat("Challenge3_" + challenge3SubCompleted);
        if (PlayerPrefs.GetFloat("Challenge3_" + challenge3SubCompleted + "AchieveValues") >= challengesData3[challenge3SubCompleted].challenge3_1_targetValues)
        {
            PlayerPrefs.SetFloat("Challenge3_" + challenge3SubCompleted + "AchieveValues", challengesData3[challenge3SubCompleted].challenge3_1_targetValues);
        }
        challengesData3[challenge3SubCompleted].challenge3_1_targetValueText.text = PlayerPrefs.GetFloat("Challenge3_" + challenge3SubCompleted + "AchieveValues") + "/" + challengesData3[challenge3SubCompleted].challenge3_1_targetValues;
        challengesData3[challenge3SubCompleted].Challenge3_1_Button.interactable = false;
        if (challengesData3[challenge3SubCompleted].Challenge3_1_Slider.value >= 1)
        {
            challengesData3[challenge3SubCompleted].Challenge3_1_Button.interactable = true;
        }
    }
    public void ClaimChallenge3_1()
    {
        if (challengesData3[challenge3SubCompleted].Challenge3_1_Slider.value >= 1)
        {
            ScoreManager.instance.IAPAddDiamondCoinBalance(challengesData3[challenge3SubCompleted].diamondValues);
            challengesData3[challenge3SubCompleted].Challenge3_1GameObject.SetActive(false);
            PlayerPrefs.SetInt("Challenge3_1Completed", 1);
            challenge3SubCompleted++;
            PlayerPrefs.SetInt("challenge3SubCompleted", challenge3SubCompleted);
            ActivateChallenges3_1();
        }
    }
    // Challenge 4
    public void ActivateChallenges4_1()
    {
        if (challenge4SubCompleted == challengesData4.Count)
        {
            return;
        }
        for (int i = 0; i < challengesData4.Count; i++)
        {
            if (i == challenge4SubCompleted)
            {
                challengesData4[i].Challenge4_1GameObject.SetActive(true);
            }
            else
            {
                challengesData4[i].Challenge4_1GameObject.SetActive(false);
            }
        }
    }
    public float challenge4_1_AchieveValues;
    public void Challenge4_1(float number)
    {
        if (challenge4SubCompleted == challengesData4.Count)
        {
            return;
        }
        PlayerPrefs.SetFloat("Challenge4_1_AchieveValuesScore", (PlayerPrefs.GetFloat("Challenge4_1_AchieveValuesScore") + number));
        challenge4_1_AchieveValues = PlayerPrefs.GetFloat("Challenge4_1_AchieveValuesScore");
        challengesData4[challenge4SubCompleted].Challenge4_1_Slider.value = challenge4_1_AchieveValues / challengesData4[challenge4SubCompleted].challenge4_1_targetValues;
        PlayerPrefs.SetFloat("Challenge4_" + challenge4SubCompleted + "AchieveValues", challenge4_1_AchieveValues);
        PlayerPrefs.SetFloat("Challenge4_" + challenge4SubCompleted, challengesData4[challenge4SubCompleted].Challenge4_1_Slider.value);
    }
    public void CheckChallenge4_1()
    {
        if (challenge4SubCompleted == challengesData4.Count)
        {
            return;
        }
        challengesData4[challenge4SubCompleted].Challenge4_1_Slider.value = PlayerPrefs.GetFloat("Challenge4_" + challenge4SubCompleted);
        if (PlayerPrefs.GetFloat("Challenge4_" + challenge4SubCompleted + "AchieveValues") >= challengesData4[challenge4SubCompleted].challenge4_1_targetValues)
        {
            PlayerPrefs.SetFloat("Challenge4_" + challenge4SubCompleted + "AchieveValues", challengesData4[challenge4SubCompleted].challenge4_1_targetValues);
        }
        challengesData4[challenge4SubCompleted].challenge4_1_targetValueText.text = PlayerPrefs.GetFloat("Challenge4_" + challenge4SubCompleted + "AchieveValues") + "/" + challengesData4[challenge4SubCompleted].challenge4_1_targetValues;
        challengesData4[challenge4SubCompleted].Challenge4_1_Button.interactable = false;
        if (challengesData4[challenge4SubCompleted].Challenge4_1_Slider.value >= 1)
        {
            challengesData4[challenge4SubCompleted].Challenge4_1_Button.interactable = true;
        }
    }
    public void ClaimChallenge4_1()
    {
        if (challengesData4[challenge4SubCompleted].Challenge4_1_Slider.value >= 1)
        {
            ScoreManager.instance.IAPAddDiamondCoinBalance(challengesData4[challenge4SubCompleted].diamondValues);
            challengesData4[challenge4SubCompleted].Challenge4_1GameObject.SetActive(false);
            PlayerPrefs.SetInt("Challenge4_1Completed", 1);
            challenge4SubCompleted++;
            PlayerPrefs.SetFloat("Challenge4_1_AchieveValuesScore", 0);
            PlayerPrefs.SetInt("challenge4SubCompleted", challenge4SubCompleted);
            ActivateChallenges4_1();
        }
    }
    // Challenge 5
    public void ActivateChallenges5_1()
    {
        if (challenge5SubCompleted == challengesData5.Count)
        {
            return;
        }
        for (int i = 0; i < challengesData5.Count; i++)
        {
            if (i == challenge5SubCompleted)
            {
                challengesData5[i].Challenge5_1GameObject.SetActive(true);
            }
            else
            {
                challengesData5[i].Challenge5_1GameObject.SetActive(false);
            }
        }
    }
    public float challenge5_1_AchieveValues;
    public void Challenge5_1(float number)
    {
        if (challenge5SubCompleted == challengesData5.Count)
        {
            return;
        }
        PlayerPrefs.SetFloat("Challenge5_1_AchieveValuesDragons", (PlayerPrefs.GetFloat("Challenge5_1_AchieveValuesDragons") + number));
        challenge5_1_AchieveValues = PlayerPrefs.GetFloat("Challenge5_1_AchieveValuesDragons");
        challengesData5[challenge5SubCompleted].Challenge5_1_Slider.value = challenge5_1_AchieveValues / challengesData5[challenge5SubCompleted].challenge5_1_targetValues;
        PlayerPrefs.SetFloat("Challenge5_" + challenge5SubCompleted + "AchieveValues", challenge5_1_AchieveValues);
        PlayerPrefs.SetFloat("Challenge5_" + challenge5SubCompleted, challengesData5[challenge5SubCompleted].Challenge5_1_Slider.value);
    }
    public void CheckChallenge5_1()
    {
        if (challenge5SubCompleted == challengesData5.Count)
        {
            return;
        }
        challengesData5[challenge5SubCompleted].Challenge5_1_Slider.value = PlayerPrefs.GetFloat("Challenge5_" + challenge5SubCompleted);
        if (PlayerPrefs.GetFloat("Challenge5_" + challenge5SubCompleted + "AchieveValues") >= challengesData5[challenge5SubCompleted].challenge5_1_targetValues)
        {
            PlayerPrefs.SetFloat("Challenge5_" + challenge5SubCompleted + "AchieveValues", challengesData5[challenge5SubCompleted].challenge5_1_targetValues);
        }
        challengesData5[challenge5SubCompleted].challenge5_1_targetValueText.text = PlayerPrefs.GetFloat("Challenge5_" + challenge5SubCompleted + "AchieveValues") + "/" + challengesData5[challenge5SubCompleted].challenge5_1_targetValues;
        challengesData5[challenge5SubCompleted].Challenge5_1_Button.interactable = false;
        if (challengesData5[challenge5SubCompleted].Challenge5_1_Slider.value >= 1)
        {
            challengesData5[challenge5SubCompleted].Challenge5_1_Button.interactable = true;
        }
    }
    public void ClaimChallenge5_1()
    {
        if (challengesData5[challenge5SubCompleted].Challenge5_1_Slider.value >= 1)
        {
            ScoreManager.instance.IAPAddDiamondCoinBalance(challengesData5[challenge5SubCompleted].diamondValues);
            challengesData5[challenge5SubCompleted].Challenge5_1GameObject.SetActive(false);
            PlayerPrefs.SetInt("Challenge5_1Completed", 1);
            challenge5SubCompleted++;
            PlayerPrefs.SetFloat("Challenge5_1_AchieveValuesDragons", 0);
            PlayerPrefs.SetInt("challenge5SubCompleted", challenge5SubCompleted);
            ActivateChallenges5_1();
        }
    }
    // Challenge 6
    public void ActivateChallenges6_1()
    {
        if (challenge6SubCompleted == challengesData6.Count)
        {
            return;
        }
        for (int i = 0; i < challengesData6.Count; i++)
        {
            if (i == challenge6SubCompleted)
            {
                challengesData6[i].Challenge6_1GameObject.SetActive(true);
            }
            else
            {
                challengesData6[i].Challenge6_1GameObject.SetActive(false);
            }
        }
    }
    public float challenge6_1_AchieveValues;
    public void Challenge6_1(float number)
    {
        if (challenge6SubCompleted == challengesData6.Count)
        {
            return;
        }
        PlayerPrefs.SetFloat("Challenge6_1_AchieveValuesGameCounts", (PlayerPrefs.GetFloat("Challenge6_1_AchieveValuesGameCounts") + number));
        challenge6_1_AchieveValues = PlayerPrefs.GetFloat("Challenge6_1_AchieveValuesGameCounts");
        challengesData6[challenge6SubCompleted].Challenge6_1_Slider.value = challenge6_1_AchieveValues / challengesData6[challenge6SubCompleted].challenge6_1_targetValues;
        PlayerPrefs.SetFloat("Challenge6_" + challenge6SubCompleted + "AchieveValues", challenge6_1_AchieveValues);
        PlayerPrefs.SetFloat("Challenge6_" + challenge6SubCompleted, challengesData6[challenge6SubCompleted].Challenge6_1_Slider.value);
    }
    public void CheckChallenge6_1()
    {
        if (challenge6SubCompleted == challengesData6.Count)
        {
            return;
        }
        challengesData6[challenge6SubCompleted].Challenge6_1_Slider.value = PlayerPrefs.GetFloat("Challenge6_" + challenge6SubCompleted);
        if (PlayerPrefs.GetFloat("Challenge6_" + challenge6SubCompleted + "AchieveValues") >= challengesData6[challenge6SubCompleted].challenge6_1_targetValues)
        {
            PlayerPrefs.SetFloat("Challenge6_" + challenge6SubCompleted + "AchieveValues", challengesData6[challenge6SubCompleted].challenge6_1_targetValues);
        }
        challengesData6[challenge6SubCompleted].challenge6_1_targetValueText.text = PlayerPrefs.GetFloat("Challenge6_" + challenge6SubCompleted + "AchieveValues") + "/" + challengesData6[challenge6SubCompleted].challenge6_1_targetValues;
        challengesData6[challenge6SubCompleted].Challenge6_1_Button.interactable = false;
        if (challengesData6[challenge6SubCompleted].Challenge6_1_Slider.value >= 1)
        {
            challengesData6[challenge6SubCompleted].Challenge6_1_Button.interactable = true;
        }
    }
    public void ClaimChallenge6_1()
    {
        if (challengesData6[challenge6SubCompleted].Challenge6_1_Slider.value >= 1)
        {
            ScoreManager.instance.IAPAddDiamondCoinBalance(challengesData6[challenge6SubCompleted].diamondValues);
            challengesData6[challenge6SubCompleted].Challenge6_1GameObject.SetActive(false);
            PlayerPrefs.SetInt("Challenge6_1Completed", 1);
            challenge6SubCompleted++;
            PlayerPrefs.SetFloat("Challenge6_1_AchieveValuesGameCounts", 0);
            PlayerPrefs.SetInt("challenge6SubCompleted", challenge6SubCompleted);
            ActivateChallenges6_1();
        }
    }
    // Challenge 7
    public void ActivateChallenges7_1()
    {
        if (challenge7SubCompleted == challengesData7.Count)
        {
            return;
        }
        for (int i = 0; i < challengesData7.Count; i++)
        {
            if (i == challenge7SubCompleted)
            {
                challengesData7[i].Challenge7_1GameObject.SetActive(true);
            }
            else
            {
                challengesData7[i].Challenge7_1GameObject.SetActive(false);
            }
        }
    }
    public float challenge7_1_AchieveValues;
    public void Challenge7_1(float number)
    {
        if (challenge7SubCompleted == challengesData7.Count)
        {
            return;
        }
        PlayerPrefs.SetFloat("Challenge7_1_AchieveValuesScoreBabyDragons", (PlayerPrefs.GetFloat("Challenge7_1_AchieveValuesScoreBabyDragons") + number));
        challenge7_1_AchieveValues = PlayerPrefs.GetFloat("Challenge7_1_AchieveValuesScoreBabyDragons");
        challengesData7[challenge7SubCompleted].Challenge7_1_Slider.value = challenge7_1_AchieveValues / challengesData7[challenge7SubCompleted].challenge7_1_targetValues;
        PlayerPrefs.SetFloat("Challenge7_" + challenge7SubCompleted + "AchieveValues", challenge7_1_AchieveValues);
        PlayerPrefs.SetFloat("Challenge7_" + challenge7SubCompleted, challengesData7[challenge7SubCompleted].Challenge7_1_Slider.value);
    }
    public void CheckChallenge7_1()
    {
        if (challenge7SubCompleted == challengesData7.Count)
        {
            return;
        }
        challengesData7[challenge7SubCompleted].Challenge7_1_Slider.value = PlayerPrefs.GetFloat("Challenge7_" + challenge7SubCompleted);
        if (PlayerPrefs.GetFloat("Challenge7_" + challenge7SubCompleted + "AchieveValues") >= challengesData7[challenge7SubCompleted].challenge7_1_targetValues)
        {
            PlayerPrefs.SetFloat("Challenge7_" + challenge7SubCompleted + "AchieveValues", challengesData7[challenge7SubCompleted].challenge7_1_targetValues);
        }
        challengesData7[challenge7SubCompleted].challenge7_1_targetValueText.text = PlayerPrefs.GetFloat("Challenge7_" + challenge7SubCompleted + "AchieveValues") + "/" + challengesData7[challenge7SubCompleted].challenge7_1_targetValues;
        challengesData7[challenge7SubCompleted].Challenge7_1_Button.interactable = false;
        if (challengesData7[challenge7SubCompleted].Challenge7_1_Slider.value >= 1)
        {
            challengesData7[challenge7SubCompleted].Challenge7_1_Button.interactable = true;
        }
    }
    public void ClaimChallenge7_1()
    {
        if (challengesData7[challenge7SubCompleted].Challenge7_1_Slider.value >= 1)
        {
            ScoreManager.instance.IAPAddDiamondCoinBalance(challengesData7[challenge7SubCompleted].diamondValues);
            challengesData7[challenge7SubCompleted].Challenge7_1GameObject.SetActive(false);
            PlayerPrefs.SetInt("Challenge7_1Completed", 1);
            challenge7SubCompleted++;
            PlayerPrefs.SetFloat("Challenge7_1_AchieveValuesScoreBabyDragons", 0);
            PlayerPrefs.SetInt("challenge7SubCompleted", challenge7SubCompleted);
            ActivateChallenges7_1();
        }
    }
}
