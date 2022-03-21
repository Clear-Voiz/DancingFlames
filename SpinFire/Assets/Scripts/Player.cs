using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D _rig;
    public BoxCollider2D boxCollider2D;
    public float face;
    public bool isGrounded;
    public bool isAttacking;
    public bool wait;
    public float fuel;
    public float maxFuel;
    public Vector3 scaleFact;
    public Animator anima;
    public GameObject PS;
    public float speed;
    public float maxSpeed;
    public bool isBoosting;
    public bool isWallSliding;
    public bool invenci;
    public string currentState;
    public float increment = 0.01f;
    public float accel;
    public bool wallColl;
    public float speedGear; //changes between running animations depending on speed
    public GameObject KickFX;
    public GameObject Up_Impulse;
    public GameObject AirKickFX;
    public GameObject PierceFX;
    public CenterActions centerActions;

    public enum AniStates {forwards, Jump, Suspend, Descend, Land, Boost, UpKick, WallSlide, LenaKick,PierceKick,FromAbove,Dive, AerialSweep,Dash,Lenalee_stand}

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        maxSpeed = 3f;
        boxCollider2D = GetComponent<BoxCollider2D>();
        KickFX = Resources.Load("KickFX") as GameObject;
        Up_Impulse = Resources.Load("Upwards_Impulse") as GameObject;
        AirKickFX = Resources.Load("AirKickFX") as GameObject;
        PierceFX = Resources.Load("PierceFX") as GameObject;
        centerActions = FindObjectOfType<CenterActions>();
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
    
    public void RegulateSpeed()
    {
        if (speed < maxSpeed + accel)
            speed += increment;
        if (speed > maxSpeed + accel)
            speed -= increment;
    }

}
