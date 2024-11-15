using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private VolumeProfile vp;

    private Vignette _vignette;
    protected float _restStat = 10, _restMaxStat = 10;
    protected int _checkpointIndex = 0;

    private void OnEnable()
    {
        if (vp) vp.TryGet(out _vignette);
    }

    public void IncreaseRestStat(float increaseAmount) {
        _restStat = Mathf.Clamp(_restStat + increaseAmount, 0, _restMaxStat);

        if (_vignette)
        {
            _vignette.intensity.value = 1 - (_restStat / _restMaxStat);
        }
    }
}
