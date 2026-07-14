using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DragonGame;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject mainMenuPanel, gamePanel, gameoverPanel,iapPanel,youDontHaveDiamonds,pausePanel;
    public GameObject dangerIndication;
    public GameObject particleHealthBar;
    public Slider healthBar;
    public CoinAnimation coinAnimationGold;
    public CoinAnimation coinAnimationDiamond;
    public bool isGameRestart;
    [Header("Countdown")]
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField] private float countdownStepDuration = 1f;
    private Coroutine countdownCoroutine;
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
        SetupCountdownText();
    }
    public void PlayButton()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        Shop.instance.GeneratePlayer();
        StartCountdownThenMove();
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
        countdownText.gameObject.SetActive(false);  
        pausePanel.SetActive(true);
        Time.timeScale = 0;

    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        countdownText.gameObject.SetActive(true);   
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
        DragonController.instance.forwardSpeed = 500F;
        //DragonController.instance.rigidbody.useGravity = false;
        // foreach (GameObject u in LevelGeneration.instance.activeTiles)
        // {
        //     u.gameObject.SetActive(false);
        // }
        // LevelGeneration.instance.activeTiles.Clear();
        // LevelGeneration.instance.activeGroundTiles.Clear();
        // LevelGeneration.instance.zSpawn = 20f;
        // LevelGeneration.instance.zSpawnForGround = 0;
        // LevelGeneration.instance.GenerationEnvironment();
        LevelGenerator.Instance.RestartLevel();
        ScoreManager.instance.CurrentGameScore = 0;
        ScoreManager.instance.currentGameScoreText.text = ScoreManager.instance.CurrentGameScore.ToString();
        StartCountdownThenMove();
    }

    private void StartCountdownThenMove()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        if (DragonController.instance != null)
        {
            DragonController.instance.isMove = false;
        }

        DragonController selectedDragon = Shop.instance.playersGameobjects[Shop.instance.savedPlayerNumber].GetComponent<DragonController>();
        if (selectedDragon != null)
        {
            selectedDragon.isMove = false;

            DragonLivesController livesController = selectedDragon.GetComponent<DragonLivesController>();
            if (livesController != null)
            {
                livesController.ResetLives();
            }
        }

        countdownCoroutine = StartCoroutine(CountdownThenMoveRoutine(selectedDragon));
    }

    private IEnumerator CountdownThenMoveRoutine(DragonController selectedDragon)
    {
        SetupCountdownText();
        string[] countdownValues = { "3", "2", "1", "GO" ,""};

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        foreach (string value in countdownValues)
        {
            if (countdownText != null)
            {
                countdownText.text = value;
            }

            yield return new WaitForSeconds(countdownStepDuration);
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        if (selectedDragon != null)
        {
            selectedDragon.isMove = true;
        }

        if (DragonController.instance != null)
        {
            DragonController.instance.isMove = true;
        }

        countdownCoroutine = null;
    }

    private void SetupCountdownText()
    {
        
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
            return;
        }

        Canvas canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }

        if (canvas == null)
        {
            return;
        }

        GameObject countdownObject = new GameObject("CountdownText");
        countdownObject.transform.SetParent(canvas.transform, false);
        
        countdownText = countdownObject.AddComponent<TextMeshProUGUI>();
        countdownText.alignment = TextAlignmentOptions.Center;
        countdownText.fontSize = 120f;
        countdownText.fontStyle = FontStyles.Bold;
        countdownText.color = Color.white;
        countdownText.raycastTarget = false;

        RectTransform rectTransform = countdownText.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        countdownObject.SetActive(false);
    }
}
