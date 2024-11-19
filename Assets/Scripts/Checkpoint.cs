using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool end;
    //TODO
    //Checkpoint Choices, or yse both, one for checkpoint, one for level clear
    //AkSoundEngine.PostEvent("Play_SFX_Checkpoint_Distorted_Chime_Stinger", gameObject);
    //AkSoundEngine.PostEvent("Play_SFX_Checkpoint_String_Stingerr", gameObject);
    //End

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AkSoundEngine.PostEvent("Play_SFX_Checkpoint_Distorted_Chime_Stinger", gameObject);

        if (end)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
