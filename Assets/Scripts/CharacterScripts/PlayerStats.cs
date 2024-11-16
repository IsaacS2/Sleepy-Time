using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private VolumeProfile vp;

    private Vignette _vignette;
    protected float _restStat = 10, _restMaxStat = 10;
    protected int _checkpointIndex = 0;

    public static Action<PlayerStats> OnLevelStart;

    protected virtual void OnEnable()
    {
        if (vp) vp.TryGet(out _vignette);
    }

    protected override void Start()
    {
        OnLevelStart(this);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Weapon weapon = collision.GetComponent<Weapon>();

        if (!weapon)
        {
            CalculateDamage(weapon.GetDamage());
        }

        Debug.Log("Health: " + _health);
    }

    public void IncreaseRestStat(float increaseAmount) 
    {
        _restStat = Mathf.Clamp(_restStat + increaseAmount, 0, _restMaxStat);

        if (_vignette)
        {
            _vignette.intensity.value = 1 - (_restStat / _restMaxStat);
        }
    }

    public float GetStrength()
    {
        return _strength;
    }
}
