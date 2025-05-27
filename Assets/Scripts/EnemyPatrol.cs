using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float Speed;
    private bool MovingRight = true;
    public float distance;
    public Transform groundDetection;
    
    void Update()
    {
     transform.Translate(Vector2.right * Speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (MovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                MovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                MovingRight = true;
            }
        }
    }
}
    