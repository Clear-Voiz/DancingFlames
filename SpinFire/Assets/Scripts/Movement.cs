using UnityEngine;

public class Movement : MonoBehaviour
{
   private float increment = 0.01f;
   private float accel = 0f;
   private bool groundColDir;
   private Player _player;
   private GameObject Up_Impulse;

   private void Awake()
   {
      _player = FindObjectOfType<Player>();
      Up_Impulse = Resources.Load("Upwards_Impulse") as GameObject;
   }
   
   private void Update()
   {
      RegulateSpeed();

      SetInMotion();

      FallAnimation();

      if (_player.currentState!=Player.AniStates.UpKick.ToString())
      {
         if (_player.speed >= _player.speedGear && _player.isBoosting && _player.currentState != "WallSlide")
            _player.ChangeAniState(Player.AniStates.Boost);
         else if (_player.isGrounded && _player.currentState != "WallSlide")
         {
            _player.ChangeAniState(Player.AniStates.forwards);
         }
      }
   }
   

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         if (other.GetContact(0).normal == Vector2.up)
         {
            _player.isGrounded = true;
            _player.ChangeAniState(Player.AniStates.Land);
            groundColDir = true;
         }

         if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
         {
            groundColDir = false;
            if (_player.isBoosting)
            {
               _player.isWallSliding = true;
               _player.ChangeAniState(Player.AniStates.WallSlide);
               Instantiate(Up_Impulse, _player.transform.position, Quaternion.identity);
               _player.isGrounded = false;
            }
            else
            {
               _player.speed = 0;
            _player.face *= -1;
            _player.scaleFact.x = _player.face;
            transform.localScale = _player.scaleFact;
            }
         }
      }
   }

   private void OnCollisionExit2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         if (groundColDir) _player.isGrounded = false;
      }
   }

   private void FallAnimation()
   {
      if (_player._rig.velocity.y >= -1f && _player._rig.velocity.y <= 1f && _player.isGrounded == false && !_player.isBoosting && !_player.isWallSliding && _player.currentState != "Dive" && _player.currentState != "AerialSweep")
      {
         _player.ChangeAniState(Player.AniStates.Suspend);
      }

      if (_player._rig.velocity.y < -1f  && _player.currentState != "Dive" && _player.currentState != "AerialSweep")
         _player.ChangeAniState(Player.AniStates.Descend);
   }

   private void RegulateSpeed()
   {
      if (_player.speed < _player.maxSpeed + accel)
         _player.speed += increment;
      if (_player.speed > _player.maxSpeed + accel)
         _player.speed -= increment;
      if (_player.isBoosting)
      {
         _player.maxSpeed = 6f;
         increment = 0.1f;
      }
      else
      {
         _player.maxSpeed = 3f;
         increment = 0.1f;
      }
   }
   
   private void SetInMotion()
   {
      if (_player.currentState != "Dive" && _player.currentState != "Lenalee_stand")
      {
         transform.Translate(_player.isWallSliding?0f:_player.face * (_player.speed * Time.deltaTime + accel),_player.isWallSliding?_player.speed * Time.deltaTime + accel:0f,0f);
      }
   }
}
