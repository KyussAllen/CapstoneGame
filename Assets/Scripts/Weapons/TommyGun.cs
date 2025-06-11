using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Permissions;

public class Weapon : MonoBehaviour
{
    public Transform Firepoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, Firepoint.position, Firepoint.rotation);
    }
}
