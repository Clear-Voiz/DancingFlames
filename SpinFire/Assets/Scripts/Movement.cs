using UnityEngine;

public class Movement : MonoBehaviour
{
   private float increment = 0.01f;
   private float accel = 0f;
   private bool groundColDir;
   private Player _player;

   private void Awake()
   {
      _player = FindObjectOfType<Player>();
   }
   
   private void Update()
   {
      RegulateSpeed();

      SetInMotion();

      SetDireSpeed();

      Jump();

      FallAnimation();

      if (_player.currentState!=Player.AniStates.UpKick.ToString())
      {
         if (_player.speed >= _player.speedGear && _player.isBoosting)
            _player.ChangeAniState(Player.AniStates.Boost);
         else if (_player.isGrounded)
         {
            _player.ChangeAniState(Player.AniStates.forwards);
         }
      }
   }
   

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         Debug.Log(other.GetContact(0).normal);
         if (other.GetContact(0).normal == Vector2.up)
         {
            _player.isGrounded = true;
            _player.ChangeAniState(Player.AniStates.Land);
            groundColDir = true;
         }

         if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
         {
            _player.speed = 0;
            _player.face *= -1;
            _player.scaleFact.x = _player.face;
            transform.localScale = _player.scaleFact;
            groundColDir = false;
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
      if (_player._rig.velocity.y >= -1f && _player._rig.velocity.y <= 1f && _player.isGrounded == false && _player.currentState != Player.AniStates.Boost.ToString())
      {
         _player.ChangeAniState(Player.AniStates.Suspend);
      }

      if (_player._rig.velocity.y < -1f)
         _player.ChangeAniState(Player.AniStates.Descend);
   }


   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
         if (_player.isGrounded && _player.isBoosting==false)
         {
            _player._rig.AddForce(Vector2.up * (2f+_player.speed), ForceMode2D.Impulse);
            _player.isGrounded = false;
            _player.ChangeAniState(Player.AniStates.Jump);
         }
         else if (_player.isGrounded && _player.isBoosting)
         {
            _player.ChangeAniState(Player.AniStates.UpKick);
            _player._rig.gravityScale = 1;
            _player._rig.AddForce(Vector2.up * (2f+_player.speed), ForceMode2D.Impulse);
            _player.isGrounded = false;
            _player.isBoosting = false;
            Instantiate(_player.PS, transform.position, Quaternion.Euler(0f,0f,-122f));
         }
      }
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
   private void SetDireSpeed()
   {
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
         if (_player.face == 1f)
         {
            _player.face = -1f;
            _player.scaleFact.x = _player.face;
            transform.localScale = _player.scaleFact;
            _player.speed = 0f;
         }
      }

      if (Input.GetKeyDown(KeyCode.RightArrow))
      {
         if (_player.face == -1f)
         {
            _player.face = 1f;
            _player.scaleFact.x = _player.face;
            transform.localScale = _player.scaleFact;
            _player.speed = 0f;
         }
      }
   }
   private void SetInMotion()
   {
      transform.Translate(_player.face * (_player.speed * Time.deltaTime + accel), 0f, 0f);
   }
}
