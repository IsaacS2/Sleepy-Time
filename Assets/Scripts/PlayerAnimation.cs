using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer _sprRend;
    private Animator _anim;
    private int _directionInt = 0;

    private void Start()
    {
        _sprRend = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Movement.MovementChange += ChangeDirection;
        Asleep.OnAsleep += ChangeRestStatus;
        Awake.OnAwake += ChangeRestStatus;
    }

    private void OnDisable()
    {
        Movement.MovementChange -= ChangeDirection;
        Asleep.OnAsleep -= ChangeRestStatus;
        Awake.OnAwake -= ChangeRestStatus;
    }

    public void ChangeDirection(Vector2 _newDirection)
    {
        int _tempDirectionInt;

        if (_newDirection == Vector2.zero && _anim) _anim.speed = 1;

        else if (_anim)
        {
            _anim.speed = 1;

            if (_newDirection.y > 0)
            {
                _tempDirectionInt = 2;
            }
            else if (_newDirection.y < 0)
            {
                _tempDirectionInt = 0;
            }
            else
            {
                _tempDirectionInt = 1;
            }

            if (_tempDirectionInt != _directionInt)
            {
                _directionInt = _tempDirectionInt;
                _anim.SetInteger("Direction", _directionInt);
            }

            if (_newDirection == Vector2.left && _tempDirectionInt == 1) _sprRend.flipX = true;
            else _sprRend.flipX = false;
        }
    }

    public void ChangeRestStatus(bool _asleep)
    {
        if (_anim) _anim.SetBool("Asleep", !_asleep);
    }
}
