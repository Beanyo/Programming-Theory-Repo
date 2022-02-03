using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : EnemyController
{
    private int normalHealth = 3;
    private int normalDamage = 1;
    private int normalValue = 2;
    private float normalSpeed = 1.0f;
    protected override void AssignValues()
    {
        health = normalHealth;
        damage = normalDamage;
        moveSpeed = normalSpeed;
        targetValue = normalValue;
    }
}