using System;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    
    private Vector3 relaPos;
    private Player _player;
    private Vector3 scaleFactor;

    private void Awake()
    {
        relaPos = new Vector3(0f,-0.1f, 0f);
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        scaleFactor = new Vector3(-1f, 1f, 1f);
    }

    private void Update()
    {
        
        if (_player.face == -1f)
        {
            relaPos.x = 0f;
            transform.localScale = scaleFactor;
        }
        
        transform.position = _player.transform.position+(relaPos);
    }
}
