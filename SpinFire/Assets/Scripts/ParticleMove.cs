using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    
    private Vector3 relaPos;
    private Player _player;

    private void Awake()
    {
        relaPos = new Vector3(-1.15f,-1.05f, 0f);
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.position = _player.transform.position+(relaPos);
        if (_player.face == -1f)
        {
            transform.rotation = Quaternion.Euler(-180,0f,-220f);
        }
    }
}
