using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHighScoreScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
