using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action<Vector3> SetNewPlayerPosition;

    private Vector3 _checkpointPos;
    private bool _previousDeath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;

            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Instance.OnSceneLoaded;
        PlayerStats.OnCheckpointContact += Instance.SetNewCheckpoint;
        PlayerStats.OnDeath += Instance.MarkDeath;
    }

    void OnDisable()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= Instance.OnSceneLoaded;
            PlayerStats.OnCheckpointContact -= Instance.SetNewCheckpoint;
            PlayerStats.OnDeath -= Instance.MarkDeath;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { SetNewPlayerPosition(_checkpointPos); }
        else {
            _checkpointPos = Vector3.zero;
        }
    }

    public void GoToNextScene()
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

    public void SetNewCheckpoint(Vector3 _newCheckpointPos)
    {
        Debug.Log("new checkpoint: " + _newCheckpointPos);

        _checkpointPos = _newCheckpointPos;
    }

    public void MarkDeath(bool _deathBool)
    {
        _previousDeath = _deathBool;
    }
}
