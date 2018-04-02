using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSetString : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        var guiTextObject = this.gameObject.GetComponent<Text>();
        guiTextObject.text = PublicSettingsManagerScript.LevelString;
	}
}
