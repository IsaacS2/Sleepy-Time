using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pit : MonoBehaviour
{   private void start()
    {
        AkSoundEngine.PostEvent("Play_SFX_Pit,", gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>())
        {
            //
            // TODO: Insert falling sound-effect
            AkSoundEngine.PostEvent("Play_DGX_Player_Fall", gameObject);
            Debug.Log("Player falls");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
