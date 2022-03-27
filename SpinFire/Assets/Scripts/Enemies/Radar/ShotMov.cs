using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMov : MonoBehaviour
{
    private float speed = 4f;

    private Animator _animator;
    private Rigidbody2D _rig;
    public RadStateMachine _rad;
    private Vector2 dir;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject,3f);
    }

    private void FixedUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
        {
            if (_rad != null)
            {
                dir = _rad.transform.right;
            }
            _rig.velocity = dir * speed * transform.localScale.x;
        }  
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
