using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DragonGame;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject mainMenuPanel, gamePanel, gameoverPanel,iapPanel,youDontHaveDiamonds;
    public GameObject dangerIndication;
    public GameObject particleHealthBar;
    public Slider healthBar;
    public CoinAnimation coinAnimationGold;
    public CoinAnimation coinAnimationDiamond;
    public bool isGameRestart;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void PlayButton()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        //DragonController.instance.isMove = true;
        //LevelGeneration.instance.GenerationEnvironment();
    }
    public void HomeButton()
    {
        Time.timeScale = 1;
        ScoreManager.instance.Coins = PlayerPrefs.GetInt("Coins") + ScoreManager.instance.CurrentCoins;
        PlayerPrefs.SetInt("Coins", ScoreManager.instance.Coins);
        ScoreManager.instance.totalCoinsTextGameover.text = PlayerPrefs.GetInt("Coins").ToString();
        SceneManager.LoadScene(0);
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
    }
    public void ReplayButton()
    {
        if (isGameRestart) return;
        isGameRestart = true;
        ScoreManager.instance.Coins = PlayerPrefs.GetInt("Coins") + ScoreManager.instance.CurrentCoins;
        PlayerPrefs.SetInt("Coins", ScoreManager.instance.Coins);
        ScoreManager.instance.totalCoinsTextGameover.text = PlayerPrefs.GetInt("Coins").ToString();
        if (ScoreManager.instance.CurrentCoins == 0)
        {
            
        }
        else
        {
            UIController.instance.coinAnimationGold.AddCoins(GameManager.instance.fromCoinImageGold.transform.position, 5);
        }
        StartCoroutine(WaitForReplayButtonClick());
    }
    public IEnumerator WaitForReplayButtonClick()
    {
        yield return new WaitForSeconds(1.5f);
        isGameRestart = false;
        ScoreManager.instance.CurrentCoins = 0;
        ScoreManager.instance.coinCountText.text = ScoreManager.instance.CurrentCoins.ToString();

        gameoverPanel.SetActive(false);
        gamePanel.SetActive(true);
        UIController.instance.dangerIndication.SetActive(false);
        DragonController.instance.cinemachineVirtualCamera.m_Follow = DragonController.instance.transform;
        DragonController.instance.rigidbody.isKinematic = false;
        DragonController.instance.forwardSpeed = 450;
        //DragonController.instance.rigidbody.useGravity = false;
        foreach (GameObject u in LevelGeneration.instance.activeTiles)
        {
            u.gameObject.SetActive(false);
        }
        LevelGeneration.instance.activeTiles.Clear();
        LevelGeneration.instance.activeGroundTiles.Clear();
        LevelGeneration.instance.zSpawn = 20f;
        LevelGeneration.instance.zSpawnForGround = 0;
        LevelGeneration.instance.GenerationEnvironment();
        ScoreManager.instance.CurrentGameScore = 0;
        ScoreManager.instance.currentGameScoreText.text = ScoreManager.instance.CurrentGameScore.ToString();
    }
}
