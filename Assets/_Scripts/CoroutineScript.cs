using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineScript : MonoBehaviour
{
    public GameObject settingsPanel, leaderBoardPanel, iAPPanel, achievementsPanel;
    //public Image settingsPanelImage;
    public IEnumerator WaitForCloseButtonClick(string panelName)
    {
        yield return new WaitForSeconds(0.5f);
        switch (panelName)
        {
            case "SettinsPanel":
                if ("SettinsPanel" == panelName)
                {

                    //settingsPanelImage = GetComponent<Image>();
                    //var tempColor = settingsPanelImage.color;
                    //tempColor.a = 0f;
                    //settingsPanelImage.color = tempColor;
                    settingsPanel.gameObject.SetActive(false);

                }
                break;
            case "LeaderBoardPanel":
                if ("LeaderBoardPanel" == panelName)
                {
                    leaderBoardPanel.gameObject.SetActive(false);
                }
                break;
            case "IAPPanel":
                if ("IAPPanel" == panelName)
                {
                    iAPPanel.gameObject.SetActive(false);
                }
                break;
            case "AchievementsPanel":
                if ("AchievementsPanel" == panelName)
                {
                    achievementsPanel.gameObject.SetActive(false);
                }
                break;
        }
    }
    public void closeButtonClick(string panlelName)
    {
        StartCoroutine(WaitForCloseButtonClick(panlelName));
    }
}
