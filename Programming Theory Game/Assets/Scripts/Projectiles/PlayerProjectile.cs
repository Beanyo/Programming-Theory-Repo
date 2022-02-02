using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : ProjectileMove
{
    [SerializeField] private float newSpeed = 5.0f;
    public int damageDeal = 1;
    // Start is called before the first frame update
    protected override void Move()
    {
        bulletSpeed = newSpeed;
        base.Move();
    }
}
