using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public bool isDefeated;

    public CharaStateManager chara;
    //public int damage;
    private void Awake()
    {
        chara = FindObjectOfType<CharaStateManager>();
    }

    private void Update()
    {
        if (isDefeated)
        {
            StartCoroutine(Destroyer());
        }
    }

    public IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
