using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(Awake))]
[RequireComponent(typeof(Asleep))]
public class GameplayInputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference jump, attack, switchRestState;

    private Asleep _asleep;
    private Awake _awake;
    private PlayerStats _playerStats;

    private void OnEnable()
    {
        jump.action.started += Jump;
        attack.action.started += Attack;
        switchRestState.action.started += RestOrSleep;
    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
        attack.action.started -= Attack;
        switchRestState.action.started -= RestOrSleep;
    }

    private void Start()
    {
        _awake.enabled = true;
        _asleep.enabled = false;
    }

    void Update()
    {
        if (_playerStats.IsDead())
        {
            _asleep.enabled = false;
            _awake.enabled = false;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (_awake.enabled) { Debug.Log("Jump"); }
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        if (_awake.enabled) { Debug.Log("Attack"); }
    }

    private void RestOrSleep(InputAction.CallbackContext obj)
    {
        if (!_playerStats.IsDead())
        {
            _asleep.enabled = !_asleep.enabled;
            _awake.enabled = !_awake.enabled;
        }
    }
}
