using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonGame;
using DanielLochner.Assets.SimpleScrollSnap;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public ScoreManager scoreManagerScript;
   public GameObject selectedText;
   public GameObject costImage;
   public GameObject selectedGreenImage;

    [SerializeField] ShopItem[] shopItem;
    [Header("Tab references")]
    public UnityEngine.UI.Button[] tabButtons; // assign Tiger/Mouse/Sea tab buttons
    public GameObject[] tabSelectedImages; // assign the SelectedImage child for each tab
    
    // The UI slots are assigned manually in the inspector (no prefab instantiation).
    public List<GameObject> shopItemPrefabInstance = new List<GameObject>();
    public TMP_Text dragonNameText,dragonCostText;
    public GameObject[] playersGameobjects,playerCanvasGameobjects;
    public int unlockPlayerNumber, savedPlayerNumber;
    public List<int> findPlayerPrefsNumber = new List<int>();
    public List<Button> shopItemButtons = new List<Button>();
    public Button buyButton;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
        
        GeneratePlayer();
    }

    
    // Switch the visible/selected tab. tabIndex should match index in tabButtons/tabSelectedImages.
    public void SwitchToTab(int tabIndex)
    {
        if (tabSelectedImages != null)
        {
            for (int i = 0; i < tabSelectedImages.Length; i++)
            {
                tabSelectedImages[i].SetActive(i == tabIndex);
            }
        }

    }
    
    public void GeneratePlayer()
    {
        for (int j = 0; j < playersGameobjects.Length; j++)
        {
            if (j == savedPlayerNumber)
            {
                playersGameobjects[savedPlayerNumber].SetActive(true);
                playerCanvasGameobjects[savedPlayerNumber].SetActive(true);
                if (savedPlayerNumber < shopItemPrefabInstance.Count && shopItemPrefabInstance[savedPlayerNumber] != null)
                {
                    selectedText.gameObject.SetActive(true);
                    costImage.SetActive(false);
                    selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    SelectCharacter(j);
                }
                playersGameobjects[savedPlayerNumber].GetComponent<DragonController>().isMove = false;
                DragonController.instance.cinemachineVirtualCamera.m_Follow = Shop.instance.playersGameobjects[savedPlayerNumber].transform;
                DragonController.instance.cinemachineVirtualCamera.m_LookAt = Shop.instance.playersGameobjects[savedPlayerNumber].transform;
            }
            else
            {
                playersGameobjects[j].SetActive(false);
                playerCanvasGameobjects[j].SetActive(false);
            }
        }
        
    }
    private void OnButtonClick(int num)
    {
        BuyCharacter(num);
    }
    public void SelectCharacter(int number)
    {
        
        for (int j = 0; j < playersGameobjects.Length; j++)
        {
            if (j == number)
            {
                shopItemButtons[j].transform.GetChild(1).transform.gameObject.SetActive(true);
                dragonNameText.text = shopItem[j].itemName;
                dragonCostText.text = shopItem[j].cost.ToString();
                shopItemPrefabInstance[j].SetActive(true);
            }
            else
            {
                shopItemButtons[j].transform.GetChild(1).transform.gameObject.SetActive(false); 
                shopItemPrefabInstance[j].SetActive(false);
            }
        }
         for (int i = 0; i < shopItemPrefabInstance.Count; i++)
        {
            if (i == number)
            {

                if (number < shopItemPrefabInstance.Count && shopItemPrefabInstance[number] != null)
                {

                    selectedText.gameObject.SetActive(true);
                    costImage.SetActive(false);
                    selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
            else
            {
                if (i < shopItemPrefabInstance.Count && shopItemPrefabInstance[i] != null)
                {
                    selectedText.gameObject.SetActive(false);
                    costImage.SetActive(true);
                    selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
                }
            }

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => OnButtonClick(number));

        }}

    public void BuyCharacter(int number)
    {

        if (PlayerPrefs.GetInt("Coins") >= shopItem[number].cost)
        {
            if (number < shopItemPrefabInstance.Count && selectedText.GetComponent<TextMeshProUGUI>().text == "Owned")
            {
                Debug.Log("Already Owned");
                PlayerPrefs.SetInt("SavedPlayerNumber", number);
                savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
                GeneratePlayer();
            }
            else
            {
                PlayerPrefs.SetInt("PlayerNumber" + number, number);
                if (number < shopItemPrefabInstance.Count && shopItemPrefabInstance[number] != null)
                {
                    selectedText.GetComponent<TextMeshProUGUI>().text = "Owned";
                    costImage.SetActive(false);
                    selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                scoreManagerScript.Coins -= shopItem[number].cost;
                PlayerPrefs.SetInt("Coins", scoreManagerScript.Coins);
                scoreManagerScript.totalCoinsTextMainMenu.text = PlayerPrefs.GetInt("Coins").ToString();
                scoreManagerScript.totalCoinsTextIAP.text = PlayerPrefs.GetInt("Coins").ToString();
                scoreManagerScript.totalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("Coins").ToString();
                scoreManagerScript.totalCoinsTextGameover.text = PlayerPrefs.GetInt("Coins").ToString();
                PlayerPrefs.SetInt("SavedPlayerNumber", number);
                savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
                GeneratePlayer();
                Challenges.instance.Challenge5_1(1);
                Challenges.instance.ActivateChallenges5_1();
                Challenges.instance.CheckChallenge5_1();
            }
        }
        else
        {
            if (number < shopItemPrefabInstance.Count && selectedText.GetComponent<TextMeshProUGUI>().text == "Owned")
            {
                //NativeUI.AlertPopup alert = NativeUI.Alert("Already", "Owned");
                Debug.Log("Already Owned");
                if (number < shopItemPrefabInstance.Count && shopItemPrefabInstance[number] != null)
                {
                    selectedText.GetComponent<TextMeshProUGUI>().text = "Selected";
                    costImage.SetActive(false);
                    selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                PlayerPrefs.SetInt("SavedPlayerNumber", number);
                savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
                GeneratePlayer();
                
            }
            else
            {
                //NativeUI.AlertPopup alert = NativeUI.Alert("Failed", "You Have No Coins");
                if (number < shopItemPrefabInstance.Count && shopItemPrefabInstance[number] != null &&
                    selectedText.GetComponent<TextMeshProUGUI>().text == "Selected")
                {
                    return;
                }
                UIController.instance.iapPanel.SetActive(true);
                Debug.Log("You Have No Coins");
            }
        }
    }
    public void PurchaseAllPack()
    {
        for (int i = 0; i < playersGameobjects.Length; i++)
        {
            if (PlayerPrefs.GetInt("PlayerNumber" + i) >= i)
            {
                if (i < shopItemPrefabInstance.Count && shopItemPrefabInstance[i] != null)
                {
                    selectedText.GetComponent<TextMeshProUGUI>().text = "Owned";
                }
            }
        }
        GeneratePlayer();
    }
    

}
