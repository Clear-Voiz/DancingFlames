using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Player _player;
    private Vector3 Spacing;
    private float increment = -5f;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        Spacing = new Vector3(0f, 0.7f, -5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null) transform.position = _player.transform.position+Spacing;
        if (_player.isBoosting)
        {
            ZoomOut();
        }
        else
        {
            DefaultZoom();
        }
    }

    private void DefaultZoom()
    {
        increment += 3f * Time.deltaTime;
        increment = Mathf.Clamp(increment, -7f, -5f);
        Spacing.z = increment;
    }

    private void ZoomOut()
    {
        increment -= 3f * Time.deltaTime;
        increment = Mathf.Clamp(increment, -7f, -5f);
        Spacing.z = increment;
        Debug.Log(increment);
    }
}
