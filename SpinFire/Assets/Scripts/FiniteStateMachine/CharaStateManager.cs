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
    public BoostBar BB;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        BB = FindObjectOfType<BoostBar>();
    }

    void Start()
    {
        currentState = _forwards;
        currentState.EnterState(this);
    }

    
    void Update()
    {
        currentState.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BB.isClicked = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            BB.isClicked = false;
            player.isBoosting = false;
        }
        
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter(this,other);
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.up)
            {
                player.isGrounded = true;
                player.groundColDir = true;
            }

            if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            {
                player.groundColDir = false;
                if (player.isBoosting)
                {
                    player.isWallSliding = true;
                    //Instantiate(player.Up_Impulse, player.transform.position, Quaternion.identity);
                    //player.isGrounded = false;
                }
            }  
        }
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = false;
            //
            //player.isWallSliding = false;
        }
    }

    private void FixedUpdate()
    {
        IsGrounded();
    }

    public void ReverseFace()
    {
        player.face *= -1f;
        player.scaleFact.x = player.face;
        player.transform.localScale = player.scaleFact;
    }

    private bool IsGrounded()
    {
        var extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(player.boxCollider2D.bounds.center, player.boxCollider2D.size, 0f, Vector2.down,
            extraHeight,1<<8);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(player.boxCollider2D.bounds.center + new Vector3(player.boxCollider2D.bounds.extents.x,0),Vector2.down * (player.boxCollider2D.bounds.extents.y+extraHeight),rayColor);
        Debug.DrawRay(player.boxCollider2D.bounds.center - new Vector3(player.boxCollider2D.bounds.extents.x,0),Vector2.down * (player.boxCollider2D.bounds.extents.y+extraHeight),rayColor);
        Debug.DrawRay(player.boxCollider2D.bounds.center - new Vector3(player.boxCollider2D.bounds.extents.x,player.boxCollider2D.bounds.extents.y+extraHeight),Vector2.right * player.boxCollider2D.bounds.extents.x*2f,rayColor);
        //Debug.Log(raycastHit.collider);
        
        return raycastHit.collider != null;
    }

    public void SwitchState(CharaBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        //Debug.Log(state);
        state.EnterState(this);
    }
}
