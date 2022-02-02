using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileMove : MonoBehaviour
{
    protected float bulletSpeed = 1.0f; // default bullet speed
    private float topBound = 10.0f; // extent to let bullets fly on z axis
    private float sideBound = 10.0f; // extent to let bullets fly on z axis

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // move projectile forward
        DestroyOutOfBounds(); // if out of play area, deactivate bullet
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        //Debug.Log(bulletSpeed);
    }

    protected void DestroyOutOfBounds()
    {
        if(Math.Abs(transform.position.z) > topBound)
        {
            gameObject.SetActive(false);
           // Debug.Log("Destroyed TopBound");
        }
        if (Math.Abs(transform.position.x) > sideBound)
        {
            gameObject.SetActive(false);
           // Debug.Log("Destroyed SideBound");
        }
    }
}
