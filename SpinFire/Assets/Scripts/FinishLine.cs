using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Movement mov = _player.GetComponent<Movement>();
            Destroy(mov);
            _player.speed = 0f;
            Debug.Log("Level Complete");
            //_player.ChangeAniState(Player.AniStates.);
            //SceneManager.LoadScene();
        }
    }
}
