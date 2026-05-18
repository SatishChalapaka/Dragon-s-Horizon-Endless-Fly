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
    [SerializeField] ShopItem[] shopItem;
    public GameObject shopItemPrefab;
    public List<GameObject> shopItemPrefabInstance = new List<GameObject>();
    public Image referenceCharacterImage;
    public Sprite greenImage;
    public Transform shopContainer1;
    public GameObject[] playersGameobjects,playerCanvasGameobjects;
    public int unlockPlayerNumber, savedPlayerNumber;
    public List<int> findPlayerPrefsNumber = new List<int>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    //public void OnEnable()
    //{
    //    SimpleScrollSnap.instance.movementDeligate += onRightClick;
    //}
    private void Start()
    {
        savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
        PopulateShop();
        GeneratePlayer();
    }

    public void PopulateShop()
    {
        for (int i = 0; i < playersGameobjects.Length; i++)
        {
            ShopItem si = shopItem[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer1);
            //itemObject.transform.GetChild(0).GetComponent<Image>().sprite = si.sprite;
            itemObject.transform.GetChild(0).GetComponent<RawImage>().texture = si.rawImage;
            itemObject.transform.GetChild(1).GetComponent<Text>().text = si.itemName;
            itemObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = si.cost.ToString();
            itemObject.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnButtonClick(si.number));
            shopItemPrefabInstance.Add(itemObject);
        }
        for (int i = 0; i < playersGameobjects.Length; i++)
        {
            if (PlayerPrefs.GetInt("PlayerNumber" + i) >= i)
            {
                shopItemPrefabInstance[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned";
                shopItemPrefabInstance[i].transform.GetChild(2).GetComponent<Image>().sprite = greenImage;
                shopItemPrefabInstance[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            }
        }
    }
    public void PopulateText()
    {
        for (int i = 0; i < playersGameobjects.Length; i++)
        {
            if (PlayerPrefs.GetInt("PlayerNumber" + i) >= i)
            {
                if (savedPlayerNumber == i)
                {
                    return;
                }
                shopItemPrefabInstance[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned";
                shopItemPrefabInstance[i].transform.GetChild(2).GetComponent<Image>().sprite = greenImage;
                shopItemPrefabInstance[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
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
                shopItemPrefabInstance[savedPlayerNumber].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
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
    public void BuyCharacter(int number)
    {

        if (PlayerPrefs.GetInt("Coins") >= shopItem[number].cost)
        {
            if (shopItemPrefabInstance[number].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text == "Owned")
            {
                //NativeUI.AlertPopup alert = NativeUI.Alert("Already", "Owned");
                Debug.Log("Already Owned");
                PlayerPrefs.SetInt("SavedPlayerNumber", number);
                savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
                GeneratePlayer();
                PopulateText();
            }
            else
            {
                PlayerPrefs.SetInt("PlayerNumber" + number, number);
                shopItemPrefabInstance[number].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned";
                shopItemPrefabInstance[number].transform.GetChild(2).GetComponent<Image>().sprite = greenImage;
                shopItemPrefabInstance[number].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
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
            if (shopItemPrefabInstance[number].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text == "Owned")
            {
                //NativeUI.AlertPopup alert = NativeUI.Alert("Already", "Owned");
                Debug.Log("Already Owned");
                shopItemPrefabInstance[number].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
                PlayerPrefs.SetInt("SavedPlayerNumber", number);
                savedPlayerNumber = PlayerPrefs.GetInt("SavedPlayerNumber");
                GeneratePlayer();
            }
            else
            {
                //NativeUI.AlertPopup alert = NativeUI.Alert("Failed", "You Have No Coins");
                if (shopItemPrefabInstance[number].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text == "Selected")
                {
                    PopulateText();
                    return;
                }
                UIController.instance.iapPanel.SetActive(true);
                Debug.Log("You Have No Coins");
            }
        }
        PopulateText();
    }
    public void PurchaseAllPack()
    {
        for (int i = 0; i < playersGameobjects.Length; i++)
        {
            if (PlayerPrefs.GetInt("PlayerNumber" + i) >= i)
            {
                shopItemPrefabInstance[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned";
                shopItemPrefabInstance[i].transform.GetChild(2).GetComponent<Image>().sprite = greenImage;
                shopItemPrefabInstance[i].transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            }
        }
        GeneratePlayer();
    }
    public void onRightClick()
    {
        GameManager.instance.playerCameraShop.transform.position = new Vector3(GameManager.instance.playerCameraShop.transform.position.x - 2,
            GameManager.instance.playerCameraShop.transform.position.y, GameManager.instance.playerCameraShop.transform.position.z);
    }
    public void onLefttClick()
    {
        GameManager.instance.playerCameraShop.transform.position = new Vector3(GameManager.instance.playerCameraShop.transform.position.x + 2,
            GameManager.instance.playerCameraShop.transform.position.y, GameManager.instance.playerCameraShop.transform.position.z);
    }

    //private void OnDisable()
    //{
    //    SimpleScrollSnap.instance.movementDeligate -= onRightClick;
    //}
}
