using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{

    private void Awake()
    {
        AkSoundEngine.SetState("Sleeping", "Title");
    }

    private void Start()
    {
      AkSoundEngine.PostEvent("Play_AMB", gameObject);
      AkSoundEngine.PostEvent("Play_MUS_Game", gameObject);


      if (SceneManager.GetActiveScene().buildIndex == 0) {
        AkSoundEngine.SetState("Sleeping", "Title");

      }

      if (SceneManager.GetActiveScene().buildIndex == 1) {
        AkSoundEngine.SetState("Sleeping", "Awake");

      }

      if (SceneManager.GetActiveScene().buildIndex == 2) {
        AkSoundEngine.SetState("Sleeping", "Win");
      }

    }


    public void GoToNextScene()
    {
      AkSoundEngine.PostEvent("Play_SFX_UI_Splatter", gameObject);
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
