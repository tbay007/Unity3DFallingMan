using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadScene(int level)
	{
		loadingImage.SetActive(true);
        SceneManager.LoadScene(level);
	}

	void Start () {
       
    }
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
