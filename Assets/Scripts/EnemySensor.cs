using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public event Action<PlayerStats> OnPlayerFound = (_playerStats) => { };
    public event Action OnPlayerLost = () => { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();

        //
        // Enemy has spotted a nearby player
        //
        if (playerStats)
        {
            Debug.Log("player found again");
            OnPlayerFound(playerStats);
            AkSoundEngine.PostEvent("Play_DGX_Enemy_Aggro", gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();

        //
        // Enemy has lost a player they recently spotted
        //
        if (playerStats) OnPlayerLost();
        else { Debug.Log("not a player"); }
    }
}
