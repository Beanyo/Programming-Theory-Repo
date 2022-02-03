using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class PlayerProjectile : ProjectileMove
{
    private float newSpeed = 10.0f;
    public int damageDeal = 1;
    // POLYMORPHISM
    protected override void Move()
    {
        bulletSpeed = newSpeed;
        base.Move();
    }
}
