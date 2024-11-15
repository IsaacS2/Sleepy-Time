using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    protected float _restStat, _restMaxStat;
    protected int _checkpointIndex = 0;

    public void IncreaseRestStat(float increaseAmount) {
        _restStat = Mathf.Clamp(_restStat + increaseAmount, 0, _restMaxStat);
    }
}
