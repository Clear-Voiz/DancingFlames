using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate
{
    //alarms

    public float[] alarm;


        public float Alarm(float time, Action<CharaStateManager> act, CharaStateManager machine)
        {
            if (time > 0f)
            {
                time -= 1f * Time.deltaTime;
                if (time <= 0f)
                {
                    act(machine);
                }
            }

            return time;
        }
        

        public Activate(int alarms)
        {
            alarm = new float[alarms];
        }
        
        //public void trial(CharaStateManager machine){}
}
