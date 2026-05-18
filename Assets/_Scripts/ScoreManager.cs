using DragonGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text currentGameScoreText, currentGameScoreTextGameover, coinCountText,totalCoinsTextMainMenu,coinCountTextGameover, 
        totalCoinsTextIAP, totalCoinsTextCharacterShop, totalCoinsTextGameover, highScoreText, highScoreTextGameover;
    public Text diamondtotalCoinsTextMainMenu, diamondtotalCoinsTextIAP, diamondtotalCoinsTextCharacterShop, diamondtotalCoinsTextGameover;
    public int unlimitedCoins, unlimitedDiamondCoins;
    private int currentGameScore;
    private int coins;
    private int diamondCoins;
    private int currentCoins;
    private int diamondcoinsCurrentCoins;
    public int CurrentGameScore { get => currentGameScore; set => currentGameScore = value; }
    public int Coins { get => coins; set => coins = value; }
    public int DiamondCoins { get => diamondCoins; set => diamondCoins = value; }
    public int CurrentCoins { get => currentCoins; set => currentCoins = value; }
    public int DiamondCurrentCoins { get => diamondcoinsCurrentCoins; set => diamondcoinsCurrentCoins = value; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Coins = PlayerPrefs.GetInt("Coins");
        DiamondCoins = PlayerPrefs.GetInt("DiamondCoins");
        currentGameScoreText.text = CurrentGameScore.ToString();
        coinCountText.text = CurrentCoins.ToString();
        //coins text reference
        totalCoinsTextMainMenu.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextIAP.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextGameover.text = PlayerPrefs.GetInt("Coins").ToString();
        //diamond text reference
        diamondtotalCoinsTextMainMenu.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextIAP.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextGameover.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        highScoreTextGameover.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void AddCoinBalance(int number)
    {
        CurrentCoins += number;
        coinCountText.text = CurrentCoins.ToString();
    }
    public void DeductDiamondCoinBalance(int number)
    {
        switch (number)
        {
            case 1:
                if (PlayerPrefs.GetInt("DiamondCoins") >= 100)
                {
                    SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("coin"));
                    UIController.instance.coinAnimationGold.AddCoins(GameManager.instance.pack1Gold.transform.position, 5);
                    IAPAddCoinBalance(100);
                    DeductDiamondCoinBalanceIAP(100);
                }
                else
                {
                    UIController.instance.youDontHaveDiamonds.SetActive(true);
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("DiamondCoins") >= 200)
                {
                    SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("coin"));
                    UIController.instance.coinAnimationGold.AddCoins(GameManager.instance.pack2Gold.transform.position, 5);
                    IAPAddCoinBalance(200);
                    DeductDiamondCoinBalanceIAP(200);
                }
                else
                {
                    UIController.instance.youDontHaveDiamonds.SetActive(true);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("DiamondCoins") >= 300)
                {
                    SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("coin"));
                    UIController.instance.coinAnimationGold.AddCoins(GameManager.instance.pack3Gold.transform.position, 5);
                    IAPAddCoinBalance(300);
                    DeductDiamondCoinBalanceIAP(300);
                }
                else
                {
                    UIController.instance.youDontHaveDiamonds.SetActive(true);
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("DiamondCoins") >= 400)
                {
                    SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("coin"));
                    UIController.instance.coinAnimationGold.AddCoins(GameManager.instance.pack4Gold.transform.position, 5);
                    IAPAddCoinBalance(400);
                    DeductDiamondCoinBalanceIAP(400);
                }
                else
                {
                    UIController.instance.youDontHaveDiamonds.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
    public void DeductDiamondCoinBalanceIAP(int number)
    {
        DiamondCoins = PlayerPrefs.GetInt("DiamondCoins");
        DiamondCoins -= number;
        PlayerPrefs.SetInt("DiamondCoins", DiamondCoins);
        diamondtotalCoinsTextMainMenu.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextIAP.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextGameover.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
    }
    public void IAPAddCoinBalance(int number)
    {
        Coins = PlayerPrefs.GetInt("Coins");
        Coins += number;
        PlayerPrefs.SetInt("Coins", Coins);
        totalCoinsTextMainMenu.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextIAP.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextGameover.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void IAPAddDiamondCoinBalance(int number)
    {
        SoundManager.instance.PlaySFX(SoundManager.instance.GetAudioClip("coin"));
        DiamondCoins = PlayerPrefs.GetInt("DiamondCoins");
        DiamondCoins += number;
        PlayerPrefs.SetInt("DiamondCoins", DiamondCoins);
        diamondtotalCoinsTextMainMenu.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextIAP.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        diamondtotalCoinsTextGameover.text = PlayerPrefs.GetInt("DiamondCoins").ToString();
        
    }
    public void WatchAddCoinBalance(int number)
    {
        Coins = PlayerPrefs.GetInt("Coins");
        Coins += number;
        PlayerPrefs.SetInt("Coins", Coins);
        totalCoinsTextMainMenu.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextIAP.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextGameover.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void Add2XCoinBalance(int number)
    {
        int num = (ScoreManager.instance.CurrentCoins * number) - ScoreManager.instance.CurrentCoins;
        Coins = PlayerPrefs.GetInt("Coins");
        Coins += num;
        PlayerPrefs.SetInt("Coins", Coins);
        totalCoinsTextMainMenu.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextIAP.text = PlayerPrefs.GetInt("Coins").ToString();
        totalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("Coins").ToString();
        coinCountTextGameover.text = (ScoreManager.instance.CurrentCoins * number).ToString();
        GameManager.instance.levelCompleteWatchAdButton.gameObject.SetActive(false);
    }
}
