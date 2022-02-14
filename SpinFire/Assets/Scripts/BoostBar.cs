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

    private void Awake()
    {
        _bar = GetComponent<Image>();
        _mov = FindObjectOfType<Movement>();
    }

    private void Start()
    {
        fuel = 20f;
        maxFuel = 20f;
    }

    private void Update()
    {
        Boost();
        Replenish();
        _bar.fillAmount = fuel / maxFuel;
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.Space) && fuel >0f)
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
