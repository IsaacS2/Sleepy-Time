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
        if (playerStats) OnPlayerFound(playerStats);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();

        //
        // Enemy has lost a player they recently spotted
        //
        if (playerStats) OnPlayerLost();
    }
}
