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
    // Optional per-item status texts. If not assigned, `selectedText` is used as a fallback for the currently-selected item.
    public TMP_Text[] statusTexts;
    public GameObject[] playersGameobjects,playerCanvasGameobjects,tickMarkGameobjects;
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
        // Ensure tick marks reflect owned state for all items
        for (int t = 0; t < tickMarkGameobjects.Length; t++)
        {
            if (t < tickMarkGameobjects.Length)
            {
                bool owned = PlayerPrefs.GetInt("PlayerNumber" + t) >= t;
                tickMarkGameobjects[t].SetActive(owned);
            }
        }

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
            bool isCurrent = (i == number);
            bool hasInstance = (i < shopItemPrefabInstance.Count && shopItemPrefabInstance[i] != null);
            bool owned = PlayerPrefs.GetInt("PlayerNumber" + i) >= i;
            bool selected = PlayerPrefs.GetInt("SavedPlayerNumber") == i;

            // Update per-item status text if provided, otherwise use shared `selectedText` only for the currently selected item
            if (statusTexts != null && i < statusTexts.Length && statusTexts[i] != null)
            {
                if (owned)
                    statusTexts[i].text = selected ? "Selected" : "Owned";
                else
                    statusTexts[i].text = "";
            }
            else if (isCurrent)
            {
                selectedText.gameObject.SetActive(true);
                if (owned)
                    selectedText.GetComponent<TextMeshProUGUI>().text = selected ? "Selected" : "Owned";
                else
                    selectedText.GetComponent<TextMeshProUGUI>().text = "";
            }

            // Tick mark reflect ownership
            if (i < tickMarkGameobjects.Length && tickMarkGameobjects[i] != null)
                tickMarkGameobjects[i].SetActive(owned);

            // Visual selection for item previews
            if (isCurrent && hasInstance)
            {
                costImage.SetActive(!owned);
                selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                shopItemPrefabInstance[i].SetActive(true);
            }
            else if (hasInstance)
            {
                shopItemPrefabInstance[i].SetActive(false);
            }
        }

        // Attach buy/select listener once for the currently selected number
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => OnButtonClick(number));
    }

    public void BuyCharacter(int number)
    {
        bool alreadyOwned = PlayerPrefs.GetInt("PlayerNumber" + number) >= number;
        if (PlayerPrefs.GetInt("Coins") >= shopItem[number].cost && !alreadyOwned)
        {
            // Purchase flow
            PlayerPrefs.SetInt("PlayerNumber" + number, number);
            scoreManagerScript.Coins -= shopItem[number].cost;
            PlayerPrefs.SetInt("Coins", scoreManagerScript.Coins);
            scoreManagerScript.totalCoinsTextMainMenu.text = PlayerPrefs.GetInt("Coins").ToString();
            scoreManagerScript.totalCoinsTextIAP.text = PlayerPrefs.GetInt("Coins").ToString();
            scoreManagerScript.totalCoinsTextCharacterShop.text = PlayerPrefs.GetInt("Coins").ToString();
            scoreManagerScript.totalCoinsTextGameover.text = PlayerPrefs.GetInt("Coins").ToString();
            // mark as saved/selected
            PlayerPrefs.SetInt("SavedPlayerNumber", number);
            savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
            // update UI
            if (number < shopItemPrefabInstance.Count && shopItemPrefabInstance[number] != null)
            {
                selectedText.GetComponent<TextMeshProUGUI>().text = "Selected";
                costImage.SetActive(false);
                selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                tickMarkGameobjects[number].SetActive(true);
            }
            GeneratePlayer();
            Challenges.instance.Challenge5_1(1);
            Challenges.instance.ActivateChallenges5_1();
            Challenges.instance.CheckChallenge5_1();
            return;
        }

        // If already owned or insufficient coins but item is owned: simply select it
        if (alreadyOwned)
        {
            PlayerPrefs.SetInt("SavedPlayerNumber", number);
            savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
            if (number < shopItemPrefabInstance.Count && shopItemPrefabInstance[number] != null)
            {
                selectedText.GetComponent<TextMeshProUGUI>().text = "Selected";
                costImage.SetActive(false);
                selectedGreenImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            GeneratePlayer();
            return;
        }

        // Not enough coins and not owned: show IAP
        if (PlayerPrefs.GetInt("Coins") < shopItem[number].cost)
        {
            if (number < shopItemPrefabInstance.Count && shopItemPrefabInstance[number] != null &&
                selectedText.GetComponent<TextMeshProUGUI>().text == "Selected")
            {
                return;
            }
            UIController.instance.iapPanel.SetActive(true);
            Debug.Log("You Have No Coins");
        }
    }
    public void PurchaseAllPack()
    {
        for (int i = 0; i < playersGameobjects.Length; i++)
        {
            PlayerPrefs.SetInt("PlayerNumber" + i, i);
            if (i < shopItemPrefabInstance.Count && shopItemPrefabInstance[i] != null)
            {
                tickMarkGameobjects[i].SetActive(true);
            }
        }
        GeneratePlayer();
    }
    

}
