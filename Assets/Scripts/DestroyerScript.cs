using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour {

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("FloorShortBarrierTransparent") || collision.name.Equals("FloorLongBarrierTransparent"))
        {
            PublicSettingsManagerScript.Score += 1;
            PublicSettingsManagerScript.ScoreString = "Score: " + PublicSettingsManagerScript.Score.ToString();
            PublicSettingsManagerScript.CheckLevel();
        }

        if(collision.gameObject.transform.parent)
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

}
