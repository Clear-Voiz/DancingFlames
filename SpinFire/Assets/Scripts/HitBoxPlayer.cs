using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlayer : MonoBehaviour
{
    public int hits { get; private set; }
    public HashSet<Collider2D> StrikeSet = new HashSet<Collider2D>();
    public GameObject Explosion;
    private SoundManager _soundManager;
    private AudioSource _boomer;

    private void Awake()
    {
        Explosion = Resources.Load("ExplosionFX") as GameObject;
        _soundManager = FindObjectOfType<SoundManager>();
        _boomer = _soundManager.GetComponent<AudioSource>();
    }

    private void Start()
    {
        hits = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!StrikeSet.Contains(other))
            {
                StrikeSet.Add(other);
                if (other.GetComponent<EnemyStats>() != null)
                {
                    EnemyStats ene = other.GetComponent<EnemyStats>();
                    ene.isDefeated = true;
                }
                _boomer.Play();
                Instantiate(Explosion, other.transform);
                //print(other);
                //Destroy(other.gameObject);
            }
        }
    }
}
