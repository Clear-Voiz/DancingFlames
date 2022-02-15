using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public bool isBoosting = false;
    public float fuel;
    public float maxFuel;
    private Image _bar;
    private Movement _mov;
    private Player _player;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        _player = FindObjectOfType<Player>();
        /*_player = GameObject.Find("Player");
        _mov = _player.GetComponent<Movement>();
        _rig = _player.GetComponent<Rigidbody2D>();*/

    }

    private void Start()
    {
        fuel = 40f;
        maxFuel = 40f;
    }

    private void Update()
    {
        Boost();
        Replenish();
        _bar.fillAmount = fuel / maxFuel;
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.Space) && fuel >0f && _player.anima.GetBool("UpKick") == false)
        {
            isBoosting = true;
            fuel -= 0.2f;
        }
        else if (fuel <= 0f)
        {
            fuel = 0f;
            isBoosting = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBoosting = false;
        }
    }

    private void Replenish()
    {
        if (fuel < maxFuel && !isBoosting)
        {
            fuel += 0.1f;
        }
    }
}
