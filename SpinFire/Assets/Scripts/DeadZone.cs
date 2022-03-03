using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private Camera _cam;
    private float secs;
    private bool initiation;
    private Player _player;
    public Transform spawnPoint;

    private void Awake()
    {
        _cam = FindObjectOfType<Camera>();
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        secs = 1f;
    }

    private void Update()
    {
        if (initiation)
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_cam.GetComponent<FollowCam>() != null)
            {
                FollowCam follower = _cam.GetComponent<FollowCam>();
                Movement _mov = _player.GetComponent<Movement>();
                Destroy(_mov);
                Destroy(follower);
                initiation = true;
            }
        }
    }

    private void Respawn()
    {
        if (secs > 0)
        {
            secs -= 1f * Time.deltaTime;
        }
        else
        {
            Rigidbody2D _rig = _player.GetComponent<Rigidbody2D>();
            _rig.Sleep();
            _player.transform.position = spawnPoint.position;
            //_player.gameObject.AddComponent<Movement>();
            _cam.gameObject.AddComponent<FollowCam>();
            _rig.WakeUp();
            secs = 1f;
            initiation = false;
        }
    }
    
}
