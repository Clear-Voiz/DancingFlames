﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rig;
    public float face;
    public bool isGrounded;
    public float fuel;
    public float maxFuel;
    public Vector3 scaleFact;
    public Animator anima;
    public GameObject PS;
    public float speed;
    public float maxSpeed;
    public bool isBoosting;
    public bool isWallSliding;
    public string currentState;
    public float speedGear; //changes between running animations depending on speed
    

    public enum AniStates {forwards, Jump, Suspend, Descend, Land, Boost, UpKick, WallSlide, LenaKick,PierceKick}

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        maxSpeed = 3f;
    }

    private void Start()
    {
        face = 1f;
        maxFuel = 40;
        fuel = maxFuel;
        scaleFact = new Vector3(1f, 1f, 1f);
        speedGear = 4f;
    }

    public void ChangeAniState(Enum recievedState)
    {
        string newState = recievedState.ToString();
        if (currentState == newState) return;
        
        anima.Play(newState);

        currentState = newState;
    }

}
