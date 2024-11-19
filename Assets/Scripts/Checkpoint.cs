using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //TODO
    //Checkpoint Choices, or use both, one for checkpoint, one for level clear
    //AkSoundEngine.PostEvent("Play_SFX_Checkpoint_Distorted_Chime_Stinger", gameObject);
    //AkSoundEngine.PostEvent("Play_SFX_Checkpoint_String_Stingerr", gameObject);
    //End

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AkSoundEngine.PostEvent("Play_SFX_Checkpoint_Distorted_Chime_Stinger", gameObject);
    }
}
