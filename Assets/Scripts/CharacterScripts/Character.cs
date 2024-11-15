using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Characeter", menuName = "Character")]
public class Character : ScriptableObject
{
    public bool invincible;

    public float strength;
    public float health;
}
