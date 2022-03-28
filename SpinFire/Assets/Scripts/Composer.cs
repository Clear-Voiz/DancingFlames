using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
public class Composer : MonoBehaviour
{
    void AddHitbox()
    {
        //gameObject.transform.SetParent(null);
        gameObject.AddComponent<HitBoxPlayer>();
        var trig = GetComponent<BoxCollider2D>();
        trig.enabled = true;
        /*var trig = gameObject.AddComponent<BoxCollider2D>();
        trig.isTrigger = true;*/
    }
}
