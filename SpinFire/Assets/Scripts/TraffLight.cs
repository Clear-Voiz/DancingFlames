using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraffLight : MonoBehaviour
{
    private CharaStateManager _machine;
    public int state;
    private SpriteRenderer spRe;
    private float secs = 1f;
    private float waitTime;
    private Animator anima;

    public enum Phase {Green, Yellow, Red}

    private void Awake()
    {
        _machine = FindObjectOfType<CharaStateManager>();
        anima = GetComponentInParent<Animator>();
        spRe = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        state = 0;
        spRe.color = new Color(0f, 1f, 0.3f, 0.67f);
        anima.Play("Green");
        secs = 1f;
    }

    private void Update()
    {
        Cronological();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == 1)
            {
                _machine.player.speed = 10f;
            }
            else if (state == 2)
            {
                _machine.player.wait = true;
                Debug.Log(_machine.player.wait);
                if (_machine.player.isGrounded && _machine.currentState != _machine.stand)
                {
                    _machine.player._rig.velocity = Vector2.right * 2.2f * _machine.player.face;
                    _machine.SwitchState(_machine.stand);
                }
                else if (!_machine.player.isGrounded && _machine.currentState != _machine.dive)
                {
                    _machine.SwitchState(_machine.dive);
                }
            }
        }
    }

    private void Cronological()
    {
        if (secs > 0)
        {
            secs -= 1f * Time.deltaTime;
        }
        else
        {
            if (state == 0)
            {
                state = (int)Phase.Yellow;
                anima.Play("Yellow");
                secs = 3f;
                spRe.color = new Color(1f, 0.75f, 0f, 0.67f);
            }
            else if (state == 1)
            {
                state = (int)Phase.Red;
                anima.Play("Red");
                secs = 3f;
                spRe.color = new Color(1f, 0f, 0.2f, 0.67f);
            }
            else if (state == 2)
            {
                state = (int)Phase.Green;
                anima.Play("Green");
                secs = 3f;
                spRe.color = new Color(0f, 1f, 0.3f, 0.67f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == 2)
            {
                _machine.player.wait = false;
                Debug.Log(_machine.player.wait);
            }
            if (state == 0 && _machine.currentState == _machine.stand)
            {
                _machine.SwitchState(_machine._forwards);
                _machine.player._rig.velocity = Vector2.right * 10f * _machine.player.face;
                Debug.Log("shouldwork");
            }
        }
    }
}
