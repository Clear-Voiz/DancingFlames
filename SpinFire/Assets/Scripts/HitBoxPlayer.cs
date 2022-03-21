using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlayer : MonoBehaviour
{
    public int hits { get; private set; }

    private void Start()
    {
        hits = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (hits > 0)
            {
                hits--;
                Destroy(other.gameObject);
            }
        }
    }
}
