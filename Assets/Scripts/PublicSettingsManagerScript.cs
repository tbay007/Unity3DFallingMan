using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Advertisements;
using System.Linq;
using Assets.Scripts.Models;

public class PublicSettingsManagerScript : MonoBehaviour
{

    public static GameSettingsModel settingsModel;
    public GameManager gameManager;
    public static int Level = 1;
    public static string LevelString = "Level: " + Level;
    public static int Score = 0;
    public static string ScoreString = "Score: 0";
#if UNITY_IOS
    private string gameId = "1752898";
#elif UNITY_ANDROID
    private string gameId = "1752899";
#endif

    public PublicSettingsManagerScript()
    {

    }

    private void Start()
    {
        try
        {
            Advertisement.Initialize(gameId, false);

            using (gameManager = new GameManager())
            {
                gameManager.CreateStorageFile();
                settingsModel = gameManager.GetGameSettingsAndHighScores();
                if (settingsModel == null)
                {
                    settingsModel = new GameSettingsModel();
                    settingsModel.Exploded = false;
                    settingsModel.DateEntered = DateTime.UtcNow.ToString();
                    settingsModel.HighScores = new List<HighScoreModel>();
                    settingsModel.Id = 1;
                    settingsModel.platformSpeed = 2f;
                    settingsModel.spawnSpeed = 2f;
                    settingsModel.Volume = 1;
                }
                else
                {
                    Level = 1;
                    LevelString = "Level: " + Level;
                    Score = 0;
                    ScoreString = "Score: 0";
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void CheckLevel()
    {
        if (Score % 5 == 0 && Level != 100)
        {
            if (settingsModel != null)
            {
                settingsModel.platformSpeed += 0.05f;
                settingsModel.spawnSpeed = settingsModel.spawnSpeed - (settingsModel.spawnSpeed * 0.05f);
                Level += 1;
                LevelString = "Level: " + Level.ToString();
            }
        }
    }

    private void Update()
    {
        if (settingsModel != null)
        {
            if (settingsModel.Exploded)
            {
                GameObject gameOver = GameObject.FindGameObjectWithTag("GameOver");
                if (gameOver != null)
                {
                    settingsModel.platformSpeed = 0;
                    settingsModel.spawnSpeed = 0;
                    var gameOverSprite = gameOver.GetComponent<SpriteRenderer>();
                    gameOverSprite.enabled = true;
                }
                GameObject ps = GameObject.FindGameObjectWithTag("Explosion");
                ParticleSystem particles = ps.GetComponent<ParticleSystem>();
                if (particles != null)
                {
                    if (particles.isStopped)
                    {
                        Advertisement.Show();
                        LoadBeginningLevel();
                    }
                }
            }
        }

    }

    public void LoadBeginningLevel()
    {
        if (settingsModel != null)
        {
            settingsModel.platformSpeed = 2f;
            settingsModel.spawnSpeed = 2f;
            settingsModel.Exploded = false;
            settingsModel.HighScores = CalculateTop5HighScore(Score);
            using (gameManager = new GameManager())
            {
                try
                {
                    gameManager.SaveItem(settingsModel);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        Level = 1;
        Score = 0;
        SceneManager.LoadScene(0);
    }

    public List<HighScoreModel> CalculateTop5HighScore(int NewGameScore)
    {
        List<HighScoreModel> modelList = new List<HighScoreModel>();
        using (gameManager = new GameManager())
        {
            GameSettingsModel model = gameManager.GetGameSettingsAndHighScores();
            if (model != null && model.HighScores != null && model.HighScores.Count > 0)
            {
                model.HighScores = model.HighScores.OrderByDescending(x => x.Score).ThenByDescending(y => y.ScoreDate).ToList();
                HighScoreModel newHighScore = new HighScoreModel() { Id = model.HighScores.Max(x => x.Id) + 1, Level = Level, Score = NewGameScore, ScoreDate = DateTime.UtcNow.ToString() };
                model.HighScores.Add(newHighScore);
                if (model.HighScores.Count > 5)
                {
                    model.HighScores = model.HighScores.OrderByDescending(x => x.Score).ThenByDescending(y => y.ScoreDate).ToList();
                    model.HighScores.RemoveRange(5, 1);
                }
                modelList.AddRange(model.HighScores);
            }
            else
            {
                HighScoreModel item = new HighScoreModel();
                item.Id = 1;
                item.Level = Level;
                item.Score = NewGameScore;
                item.ScoreDate = DateTime.UtcNow.ToString();
                modelList.Add(item);
            }
        }

        return modelList;
    }

}
