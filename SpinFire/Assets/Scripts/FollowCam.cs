﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Player _player;
    private Vector3 Spacing;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        Spacing = new Vector3(0f, 0.7f, -3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null) transform.position = _player.transform.position+Spacing;
    }
}