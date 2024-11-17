using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected Character character;
    [SerializeField] protected float _maxFlickerTime, _maxInvincibilityTime;

    protected SpriteRenderer _sprRend;
    protected float _health, _strength, _flickerTimer, _invincibilityTimer;
    protected bool _invincible, _dead;

    protected virtual void Awake()
    {
        _health = character.health;
        _strength = character.strength;
        _invincible = character.invincible;
        _invincibilityTimer = _maxInvincibilityTime;
        _flickerTimer = _maxFlickerTime;
    }

    protected virtual void Start()
    {
        _sprRend = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (_invincibilityTimer < _maxInvincibilityTime)
        {
            _flickerTimer += _invincibilityTimer += Time.deltaTime;
            
            if (_flickerTimer >= _maxFlickerTime && _sprRend)
            {
                _flickerTimer = 0;
                _sprRend.enabled = !_sprRend.enabled;
            }

            if (_invincibilityTimer >= _maxInvincibilityTime)
            {
                _flickerTimer = _maxFlickerTime;
                _invincible = false;
                if (_sprRend) _sprRend.enabled = true;
            }
        }
    }

    public virtual void CalculateDamage(float damage)
    {
        if (!_invincible)
        {
            _health -= damage;

            if (damage > 0)
            {
                _invincibilityTimer = 0;
                _invincible = true;
            }
            
            Debug.Log("Health: " + _health);
        }

        if (_health <= 0) _dead = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();

        if (weapon)
        {
            CalculateDamage(weapon.GetDamage());
        }
    }

    public bool IsDead()
    {
        return _dead;
    }


    public float GetStrength()
    {
        return _strength;
    }
}
