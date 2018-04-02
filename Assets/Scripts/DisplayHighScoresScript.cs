using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScoresScript : MonoBehaviour {
    public static GameSettingsModel settingsModel;
    public GameManager gameManager;

    public Button m_testText;
    void Start () {
        using (gameManager = new GameManager())
        {
            gameManager.CreateStorageFile();
            settingsModel = gameManager.GetGameSettingsAndHighScores();
            if (settingsModel != null)
            {
                var panel = GameObject.Find("HighScorePanel");
                if (panel != null)
                {
                    settingsModel.HighScores = settingsModel.HighScores.OrderByDescending(x => x.Score).ThenByDescending(x => x.Level).ToList();
                    foreach(var item in settingsModel.HighScores)
                    {
                        var positionofpanel = panel.transform;
                        Button newButtonInstance = MonoBehaviour.Instantiate(m_testText, positionofpanel) as Button;
                        Text textObject = newButtonInstance.GetComponentInChildren<Text>();
                        textObject.text = "Score: " + item.Score + "  Level: " + item.Level;
                        textObject.gameObject.SetActive(true);

                        newButtonInstance.transform.SetParent(positionofpanel);
                        newButtonInstance.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
