using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>())
        {
            //
            // TODO: Insert falling sound-effect
            //
            Debug.Log("Player falls");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
