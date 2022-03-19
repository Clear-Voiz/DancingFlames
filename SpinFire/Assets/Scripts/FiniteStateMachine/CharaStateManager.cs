using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharaStateManager : MonoBehaviour
{
    public CharaBaseState currentState;
    public Stand_EX stand = new Stand_EX();
    public StandUp_EX standUp = new StandUp_EX();
    public Forwards _forwards = new Forwards();
    public Fall_EX fall = new Fall_EX();
    public Boost_Ex boost = new Boost_Ex();
    public Collapse_EX collapse = new Collapse_EX();
    public Jump_EX jump = new Jump_EX();
    public Suspended_EX suspended = new Suspended_EX();
    public LenaKick_EX lenakick = new LenaKick_EX();
    public Descend_EX descend = new Descend_EX();
    public Land_EX land = new Land_EX();
    public Dive_EX dive = new Dive_EX();
    public UpKick_EX upKick = new UpKick_EX();
    public AirKick_EX airKick = new AirKick_EX();
    public Dash_EX dash = new Dash_EX();
    public Aerial_Sweep_EX aerialSweep = new Aerial_Sweep_EX();
    public WallSlide_EX wallSlide = new WallSlide_EX();
    public PierceKick_EX pierceKick = new PierceKick_EX();
    public Activate ring;
    
    //Buttons
    public RightActions rightActions;
    public LeftActions leftActions;
    public UpActions upActions;
    public DownActions downActions;

    public Player player;
    public BoostBar BB;

    /*alarm info
     alarm[0] = AirKick.SwitchState
     alarm[1] = Collapse.SwitchState
     alarm[2] = StandUp.SwitchState
     */

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        BB = FindObjectOfType<BoostBar>();
        rightActions = FindObjectOfType<RightActions>();
        leftActions = FindObjectOfType<LeftActions>();
        upActions = FindObjectOfType<UpActions>();
        downActions = FindObjectOfType<DownActions>();
    }

    void Start()
    {
        ring = new Activate(7);
        currentState = stand;
        currentState.EnterState(this);
    }

    
    void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState.ToString());

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
       /* if (other.collider.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.up)
            {
                //player.wallColl = true;
            }

            if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            {
                player.wallColl = false;
                if (player.isBoosting)
                {
                    //player.isWallSliding = true;
                    //Instantiate(player.Up_Impulse, player.transform.position, Quaternion.identity);
                }
            }  
        }*/
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            
            //player.isWallSliding = false;
        }
    }

    private void OnDisable()
    {
        currentState.OnDisableState(this);
    }

    private void FixedUpdate()
    {
        IsGrounded();
        WallColl();
        currentState.FixedUpdateState(this);
    }

    public void ReverseFace()
    {
        player.face *= -1f;
        player.scaleFact.x = player.face;
        player.transform.localScale = player.scaleFact;

        if (Input.GetKeyDown(KeyCode.R)) //de recarga
        {
            //StartCoroutine(Activate.)
        }
    }

    private void IsGrounded()
    {
        var extraHeight = 0.02f;
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
        
        player.isGrounded = raycastHit.collider != null;
    }

    private void WallColl()
    {
        var extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.Raycast(player.boxCollider2D.bounds.center + new Vector3(0f,-player.boxCollider2D.bounds.extents.y), Vector2.right * player.face,
            player.boxCollider2D.bounds.extents.x + extraHeight, 1 << 8);
        
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        
        Debug.DrawRay(player.boxCollider2D.bounds.center + new Vector3(0f,-player.boxCollider2D.bounds.extents.y),Vector2.right*player.face*(player.boxCollider2D.bounds.extents.x+extraHeight),rayColor);
        //Debug.Log(raycastHit.collider);
        
        player.wallColl = raycastHit.collider != null;
    }

    public void SwitchState(CharaBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        //Debug.Log(state);
        state.EnterState(this);
    }
}
