using UnityEngine;

public class Movement : MonoBehaviour
{
   private bool boost;
   private float increment = 0.01f;
   private float accel = 0f;
   private bool groundColDir;
   private BoostBar _bB;
   private Player _player;

   private void Awake()
   {
      _bB = FindObjectOfType<BoostBar>();
      _player = FindObjectOfType<Player>();
   }
   
   private void Update()
   {
      RegulateSpeed();

      SetInMotion();

      Boost();

      SetDireSpeed();

      Jump();

      FallAnimation();
   }

  

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Ground"))
      {
         Debug.Log(other.GetContact(0).normal);
         if (other.GetContact(0).normal == Vector2.up)
         {
            _player.isGrounded = true;
            _player.anima.SetInteger("isJumping", 4);
            Debug.Log(_player.anima.GetInteger("isJumping"));
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
   private void Boost()
   {
      if (Input.GetKeyDown(KeyCode.Space) && _player.anima.GetBool("UpKick") == false && _bB.fuel>0f)
      {
         boost = true;
         _player.anima.SetBool("isBoosting",true);
         _player._rig.velocity = Vector2.zero;
         _player._rig.gravityScale = 0;
      }
      else if (_bB.fuel <= 0f)
      {
         boost = false;
         _player.anima.SetBool("isBoosting",false);
         _player._rig.gravityScale = 1;
      }
      
      
      if (Input.GetKeyUp(KeyCode.Space))
      {
         boost = false;
         _player.anima.SetBool("isBoosting",false);
         _player._rig.gravityScale = 1;
      }
   }
   
   private void FallAnimation()
   {
      if (_player._rig.velocity.y >= -1f && _player._rig.velocity.y <= 1f && _player.isGrounded == false)
      {
         _player.anima.SetInteger("isJumping", 2);
         _player.anima.SetBool("UpKick", false);
      }

      if (_player._rig.velocity.y < -1f) _player.anima.SetInteger("isJumping", 3);
   }


   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
         if (_player.isGrounded && boost==false)
         {
            _player._rig.AddForce(Vector2.up * (2f+_player.speed), ForceMode2D.Impulse);
            _player.isGrounded = false;
            _player.anima.SetInteger("isJumping",1);
         }
         else if (_player.isGrounded && boost)
         {
            _player.anima.SetBool("UpKick",true);
            _player._rig.gravityScale = 1;
            _player._rig.AddForce(Vector2.up * (2f+_player.speed), ForceMode2D.Impulse);
            _player.isGrounded = false;
            boost = false;
            Instantiate(_player.PS, transform.position, Quaternion.Euler(0f,0f,-122f));
            //anima.SetBool("isBoosting",false);
         }
      }
   }

   private void RegulateSpeed()
   {
      if (_player.speed < _player.maxSpeed + accel)
         _player.speed += increment;
      if (_player.speed > _player.maxSpeed + accel)
         _player.speed -= increment;
      if (boost)
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
