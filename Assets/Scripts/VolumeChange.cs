using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour {
    public GameManager gameManager;

    void Start()
    {
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
                source.volume = (float)settingsModel.Volume;
            }
            gameManager.SaveItem(settingsModel);

            GameObject volumeSliderGO = GameObject.FindGameObjectWithTag("VolumeSlider");
            Slider sliderComponent = volumeSliderGO.GetComponent<Slider>();
            sliderComponent.value = (float)settingsModel.Volume;
        }
    }

    public void VolumeChanged(float sliderComponent)
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        AudioSource source = mainCamera.GetComponent<AudioSource>();
        source.volume = sliderComponent * 0.10f;

        using (gameManager = new GameManager())
        {
            gameManager.CreateStorageFile();
            var settingsModel = gameManager.GetGameSettingsAndHighScores();
            if (settingsModel == null)
            {
                settingsModel = new GameSettingsModel();
                settingsModel.Exploded = false;
                settingsModel.DateEntered = System.DateTime.UtcNow.ToString();
                settingsModel.HighScores = new List<HighScoreModel>();
                settingsModel.Id = 1;
                settingsModel.platformSpeed = 2f;
                settingsModel.spawnSpeed = 2f;
                settingsModel.Volume = (decimal)sliderComponent;
            }
            else
            {
                settingsModel.Volume = (decimal)sliderComponent;
            }

            gameManager.SaveItem(settingsModel);
        }

       
    }

}
