using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    private enum PlayerState
    {
        Idle,
        IdleBreaker,
        Walk,
        Run,


        Jumping,
        Attacking,
        Charging,
        OwnHit,
        ReceiveDamage,
        Blocked,
        Fail

    }
    public Animator _characterAnimator;
    private PlayerState _currentState;
    void Start()
    {
        //_characterAnimator.SetBool("Idle", true);
        //SetState(PlayerState.Idle);
        _characterAnimator.SetInteger("Controlador", 0);

    }

    // Update is called once per frame
    void Update()
    {
        // PlayerState newState = DeterminateState();
        //if (newState != _currentState)
        //  SetState(newState);
        //TheInput();
        StartCoroutine(waiting());

    }


    private void SetState(PlayerState newState)
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", false);
                break;
            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", false);
                break;
            case PlayerState.Walk:
                _characterAnimator.SetBool("Walking", false);
                break;
            case PlayerState.Run:
                _characterAnimator.SetBool("Running", false);
                break;
            case PlayerState.Jumping:
                _characterAnimator.SetBool("Jumping", false);

                break;
            case PlayerState.Attacking:
                _characterAnimator.SetBool("Attacking", false);
                break;
        }
        switch (newState)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", true);
                break;
            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", true);
                break;
            case PlayerState.Walk:
                _characterAnimator.SetBool("Walking", true);
                break;
            case PlayerState.Run:
                _characterAnimator.SetBool("Running", true);
                break;
            case PlayerState.Jumping: _characterAnimator.SetBool("Jumping", true);

                break;
            case PlayerState.Attacking:
                _characterAnimator.SetBool("Attacking", true);
                break;
        }

        _currentState = newState;
    }


    private PlayerState DeterminateState()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return PlayerState.IdleBreaker;
        else if (IsRunning())
        {
            return PlayerState.Run;
        }
        else if (IsWalking())
        {
            return PlayerState.Walk;
        }
        else if (IsRunning())
        {
            return PlayerState.Idle;
        }
        else if (isAttacking())
        {
            return PlayerState.Attacking;
        }
        else if (isJumping())
        {
            return PlayerState.Jumping;
        }
        else { return PlayerState.Idle; }
    }

    private bool IsWalking()
    {
        return Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift);
    }
    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift);
    }
    private bool isJumping()
    {
        return Input.GetKey(KeyCode.S);

    }
    private bool isAttacking()
    {
        return Input.GetKey(KeyCode.A);

    }
    private void TheInput()
    {
        if (Input.GetKey(KeyCode.A)) {
            _characterAnimator.SetInteger("Controlador", 1);
            StartCoroutine(waiting());
            Debug.Log("Esperé");
            //_characterAnimator.SetInteger("Controlador", 0);
        }
       else if (Input.GetKey(KeyCode.S))
        {
            _characterAnimator.SetInteger("Controlador", 2);
            StartCoroutine(waiting());
            //_characterAnimator.SetInteger("Controlador", 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            _characterAnimator.SetInteger("Controlador", 3);
            StartCoroutine(waiting());

            //_characterAnimator.SetInteger("Controlador", 0);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            _characterAnimator.SetInteger("Controlador", 4);
            StartCoroutine(waiting());

            //_characterAnimator.SetInteger("Controlador", 0);
        }
        else if (Input.GetKey(KeyCode.G))
        {
            _characterAnimator.SetInteger("Controlador", 5);
            StartCoroutine(waiting());

            //_characterAnimator.SetInteger("Controlador", 0);
        }
        else if (Input.GetKey(KeyCode.H))
        {
            _characterAnimator.SetInteger("Controlador", 6);
            StartCoroutine(waiting());

            //_characterAnimator.SetInteger("Controlador", 0);
        }
    }
    IEnumerator waiting()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _characterAnimator.SetInteger("Controlador", 1);
            yield return new WaitForSeconds(1);
            Debug.Log("Esperé");
            _characterAnimator.SetInteger("Controlador", 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _characterAnimator.SetInteger("Controlador", 2);
            yield return new WaitForSeconds(1);
            _characterAnimator.SetInteger("Controlador", 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            _characterAnimator.SetInteger("Controlador", 3);
            yield return new WaitForSeconds(1);

            _characterAnimator.SetInteger("Controlador", 0);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            _characterAnimator.SetInteger("Controlador", 4);
            yield return new WaitForSeconds(1);

            _characterAnimator.SetInteger("Controlador", 0);
        }
        else if (Input.GetKey(KeyCode.G))
        {
            _characterAnimator.SetInteger("Controlador", 5);
            yield return new WaitForSeconds(1);

            _characterAnimator.SetInteger("Controlador", 0);
        }
        else if (Input.GetKey(KeyCode.H))
        {
            _characterAnimator.SetInteger("Controlador", 6);
            yield return new WaitForSeconds(1);

            _characterAnimator.SetInteger("Controlador", 0);
        }
    }
}