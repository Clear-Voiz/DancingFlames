using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaStateManager : MonoBehaviour
{
    public CharaBaseState currentState;
    public Forwards _forwards = new Forwards();
    public Boost_Ex boost = new Boost_Ex();
    public Jump_EX jump = new Jump_EX();
    public Suspended_EX suspended = new Suspended_EX();
    public LenaKick_EX lenakick = new LenaKick_EX();
    public Descend_EX descend = new Descend_EX();
    public Land_EX land = new Land_EX();
    public Dive_EX dive = new Dive_EX();
    public UpKick_EX upKick = new UpKick_EX();
    public Dash_EX dash = new Dash_EX();
    public Aerial_Sweep_EX aerialSweep = new Aerial_Sweep_EX();
    public WallSlide_EX wallSlide = new WallSlide_EX();
    public PierceKick_EX pierceKick = new PierceKick_EX();

    public Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        currentState = _forwards;
        currentState.EnterState(this);
    }

    
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) player.isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.up)
            {
                player.isGrounded = true;
            }
        }
    }

    public void SwitchState(CharaBaseState state)
    {
        currentState = state;
        Debug.Log(state);
        state.EnterState(this);
    }
}
