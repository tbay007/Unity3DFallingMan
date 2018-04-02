using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustAudioFromSettings : MonoBehaviour {
    private GameManager gameManager;
	// Use this for initialization
	void Start () {
        using (gameManager = new GameManager())
        {
            gameManager.CreateStorageFile();
            var settingsModel = gameManager.GetGameSettingsAndHighScores();
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            AudioSource source = mainCamera.GetComponent<AudioSource>();
            
            if (settingsModel == null)
            {
                source.volume = 10;
            }
            else
            {
                source.volume = (float)settingsModel.Volume * 0.10f;
            }

            gameManager.SaveItem(settingsModel);
        }
        

        
    }
	
	
}
