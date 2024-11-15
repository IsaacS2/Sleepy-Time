using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected Character character;

    protected float _health, _strength;
    protected bool _invincible, _dead;

    protected virtual void Awake()
    {
        _health = character.health;
        _strength = character.strength;
        _invincible = character.invincible;
    }

    public void CalculateDamage(float damage)
    {
        if (!_invincible) _health -= damage;
        if (_health <= 0) _dead = true;
    }

    public bool IsDead()
    {
        return _dead;
    }
}
