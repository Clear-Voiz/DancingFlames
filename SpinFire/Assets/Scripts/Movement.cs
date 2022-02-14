using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
   private float speed;
   private bool boost;
   private float increment = 0.01f;
   private float maxspeed = 3f;
   private float face = 1f;
   private float accel = 0f;
   private Rigidbody2D _rig;
   private bool isgrounded = true;
   private Vector3 scaleFact;
   public Animator anima;
   private BoostBar _bB;

   private void Awake()
   {
      anima = GetComponent<Animator>();
      _bB = FindObjectOfType<BoostBar>();
   }

   private void Start()
   {
      _rig = GetComponent<Rigidbody2D>();
      scaleFact = new Vector3(1f, 1f, 1f);
   }

   private void Update()
   {
      RegulateSpeed();

      SetInMotion();

      Boost();

      SetDireSpeed();

      Jump();

      if (_rig.velocity.y >= -1f && _rig.velocity.y <= 1f && isgrounded == false)
      {
         anima.SetInteger("isJumping",2);
         anima.SetBool("UpKick",false);
      }

      if (_rig.velocity.y < -1f) anima.SetInteger("isJumping",3);
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         isgrounded = true;
         Debug.Log(other.GetContact(0));
         anima.SetInteger("isJumping",4);
         Debug.Log(anima.GetInteger("isJumping"));
      }
   }

   private void OnCollisionExit2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         isgrounded = false;
      }
   }

   private void SetInMotion()
   {
      transform.Translate(face * (speed * Time.deltaTime + accel), 0f, 0f);
   }

   private void RegulateSpeed()
   {
      if (speed < maxspeed + accel)
         speed += increment;
      if (speed > maxspeed + accel)
         speed -= increment;
      if (boost)
      {
         maxspeed = 6f;
         increment = 0.1f;
      }
      else
      {
         maxspeed = 3f;
         increment = 0.1f;
      }
   }

   private void Boost()
   {
      if (Input.GetKeyDown(KeyCode.Space) && anima.GetBool("UpKick") == false && _bB.fuel>0f)
      {
         boost = true;
         anima.SetBool("isBoosting",true);
         _rig.velocity = Vector2.zero;
         _rig.gravityScale = 0;
      }
      else if (_bB.fuel <= 0f)
      {
         boost = false;
         anima.SetBool("isBoosting",false);
         _rig.gravityScale = 1;
      }
      
      
      if (Input.GetKeyUp(KeyCode.Space))
      {
         boost = false;
         anima.SetBool("isBoosting",false);
         _rig.gravityScale = 1;
      }
   }

   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
         if (isgrounded && boost==false)
         {
            _rig.AddForce(Vector2.up * (2f+speed), ForceMode2D.Impulse);
            isgrounded = false;
            anima.SetInteger("isJumping",1);
         }
         else if (isgrounded && boost)
         {
            anima.SetBool("UpKick",true);
            _rig.gravityScale = 1;
            _rig.AddForce(Vector2.up * (2f+speed), ForceMode2D.Impulse);
            isgrounded = false;
            boost = false;
            //anima.SetBool("isBoosting",false);
         }
      }
   }

   private void SetDireSpeed()
   {
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
         if (face == 1f)
         {
            face = -1f;
            scaleFact.x = face;
            transform.localScale = scaleFact;
            speed = 0f;
         }
      }

      if (Input.GetKeyDown(KeyCode.RightArrow))
      {
         if (face == -1f)
         {
            face = 1f;
            scaleFact.x = face;
            transform.localScale = scaleFact;
            speed = 0f;
         }
      }
   }
}
