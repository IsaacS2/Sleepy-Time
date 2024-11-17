using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void CalculateDamage(float damage)
    {
        base.CalculateDamage(damage);

        if (_dead) Destroy(gameObject);
    }
}
