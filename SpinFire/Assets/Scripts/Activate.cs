using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate
{
    //alarms

    public IEnumerator[] alarm;

       
        public IEnumerator Alarm(float time, Action act)
        {
            yield return new WaitForSeconds(time);
            act();
        }

        public Activate(int alarms)
        {
            alarm = new IEnumerator[alarms];
        }
}
