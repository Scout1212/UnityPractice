using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D sb;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velo = sb.velocity;
        velo.y += Input.GetKeyDown(KeyCode.Space) ? 10 : 0;
        velo.x = 0;
        sb.velocity = velo;
    }
}
