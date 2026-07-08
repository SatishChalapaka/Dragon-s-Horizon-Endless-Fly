using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public GameObject tutorialPanel,increaseHealthText, decreaseHealthText,slider, decreaseHealthBG;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            tutorialPanel.SetActive(true);
            //increaseHealthText.SetActive(false);
        }
        else
        {
            tutorialPanel.SetActive(false);
        }



        // if (PlayerPrefs.GetInt("FirstTimeOpen") == 0)
        // {
        //     nameWindow.SetActive(true);
            
        // }
        // else
        // {
        //     nameWindow.SetActive(false);
        // }
    }

    public void TutorialPanelClick()
    {
        tutorialPanel.SetActive(false);
        StartCoroutine(WaitForDisableDecreaseHealthText());
        slider.GetComponent<Animator>().SetBool("isSliderAnimate", true);
    }
    public IEnumerator WaitForDisableDecreaseHealthText()
    {
        //decreaseHealthText.SetActive(true);
        //decreaseHealthBG.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        slider.GetComponent<Animator>().SetBool("isSliderAnimate", false);
        PlayerPrefs.SetInt("Tutorial", 1);
        decreaseHealthText.SetActive(false);
        decreaseHealthBG.SetActive(false);
        increaseHealthText.SetActive(true);
    }
    public void TutorialPanelButton()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        // PlayerPrefs.SetInt("FirstTimeOpen", 1);
    }
}
