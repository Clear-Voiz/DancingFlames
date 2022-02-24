using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float secs;
    private float maxsecs;
    public TMP_Text watch;
    //private bool ready;

    private void Start()
    {
        secs = 180;
    }

    private void Update()
    {
        InverseCrono();
    }

    private void InverseCrono()
    {
        if (secs > 0)
        {
            secs -= 1f * Time.deltaTime;
            watch.text = "Time: " + Mathf.Ceil(secs);
            
        }
        else
        {
            //ready = true;
        }
    }

    private void Cronological()
    {
        if (secs < maxsecs)
        {
            Mathf.Clamp(secs, 0f, 10f);
            secs += 1f * Time.deltaTime;
            watch.text = "Time: " + Mathf.Floor(secs);
            //ready = true;
        }
    }
}
