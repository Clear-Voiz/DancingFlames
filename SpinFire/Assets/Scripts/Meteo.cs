using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    private Player _player;
    private float scaleFact = 1f;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        scaleFact += 1 * Time.deltaTime;
        transform.localScale = new Vector3(scaleFact, scaleFact, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            //if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            Destroy(gameObject);
        }
    }
}
