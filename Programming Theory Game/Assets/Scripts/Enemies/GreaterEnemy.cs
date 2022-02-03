using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterEnemy : EnemyController
{
    private int greaterHealth = 5;
    private int greaterDamage = 2;
    private int greaterValue = 3;
    private float greaterSpeed = 2.0f;
    protected override void AssignValues()
    {
        health = greaterHealth;
        damage = greaterDamage;
        moveSpeed = greaterSpeed;
        targetValue = greaterValue;
    }
}
