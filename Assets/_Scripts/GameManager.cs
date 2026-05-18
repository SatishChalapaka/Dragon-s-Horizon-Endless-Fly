using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DragonGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public bool isBoost,isJetpack,isJetPackCompleted;
        public GameObject splashScreen;
        public ParticleSystem confettiParticleGameover,coinParticleCollect,bulletDestroyedParticle;
        public Camera playerCameraShop;
        public Button watchAdButton;
        public Button levelCompleteWatchAdButton;
        public Transform fromCoinImageGold, toCoinImageGold, fromCoinImageDiamond, toCoinImageDiamond, pack1Diamond, pack2Diamond, pack3Diamond, pack4Diamond,
            pack1Gold, pack2Gold, pack3Gold, pack4Gold;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            GameManager.instance.watchAdButton.onClick.AddListener(() => AdsInitializer.instance.rewardedAdsButton.ShowAd());
            GameManager.instance.levelCompleteWatchAdButton.onClick.AddListener(() => AdsInitializer.instance.rewardedAdsButton.ShowAd("_2XCoins"));
        }
        private void Start()
        {
            PlayerPrefs.GetInt("GameOpened");
            if (PlayerPrefs.GetInt("GameOpened") == 0)
            {
                splashScreen.gameObject.SetActive(true);
                PlayerPrefs.SetInt("GameOpened", 1);
            }
            else
            {
                splashScreen.gameObject.SetActive(false);
            }
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("GameOpened", 0);
        }
        public void PrivacyPolicy()
        {
            Application.OpenURL("https://www.runelordegamesstudios.com/privacy-policy");
        }
        public void RateUs()
        {
            Application.OpenURL("https://www.runelordegamesstudios.com/privacy-policy");
        }
        public void BoostEnable()
        {
            isBoost = true;
        }
        public void JetpackEnable()
        {
            isJetpack = true;
        }
        public void Vibrate()
        {
            Handheld.Vibrate();
        }
    }
}