using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadStateMachine : MonoBehaviour
{
    public RadBaseState currentState;

    public InactiveState inactiveState = new InactiveState();
    public ActiveState activeState = new ActiveState();

    [HideInInspector] public Collider2D _col;
    public Animator anima;
    public bool isDefeated;
    public GameObject bullet;
    public EnemyStats eneStats;
    [HideInInspector]public Collider2D _mainCol;
    [HideInInspector] public Player player;
    private float replenish = 5f;

    private void Awake()
    {
        _col = GetComponentInChildren<Collider2D>();
        anima = GetComponent<Animator>();
        bullet = Resources.Load("LaserShot") as GameObject;
        eneStats = GetComponent<EnemyStats>();
        _mainCol = GetComponent<Collider2D>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        currentState = inactiveState;
        currentState.EnterState(this);
    }
    
    void Update()
    {
        currentState.UpdateState(this);
    }
    
    public void SwitchState(RadBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (player.fuel < player.maxFuel - replenish)
        {
            player.fuel += replenish;
        }
        else
        {
            player.fuel = player.maxFuel;
        }
    }
}
