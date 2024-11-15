using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private Image fog;

    protected float _restStat = 10, _restMaxStat = 10;
    protected int _checkpointIndex = 0;

    public void IncreaseRestStat(float increaseAmount) {
        _restStat = Mathf.Clamp(_restStat + increaseAmount, 0, _restMaxStat);

        if (fog)
        {
            fog.color = new Vector4(fog.color.r, fog.color.b, fog.color.g, 1 - (_restStat / _restMaxStat));
        }
    }
}
