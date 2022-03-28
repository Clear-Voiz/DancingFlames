using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public bool isDefeated;

    public CharaStateManager chara;

    public SpriteRenderer rend;
    //public int damage;
    private void Awake()
    {
        chara = FindObjectOfType<CharaStateManager>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDefeated)
        {
            if (rend != null)
            {
                rend.color = Color.red;
            }
            StartCoroutine(Destroyer());
        }
    }

    public IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
