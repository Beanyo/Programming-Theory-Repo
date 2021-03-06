using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class LesserEnemy : EnemyController
{
    private int lesserHealth = 1;
    private int lesserDamage = 1;
    private int lesserValue = 1;
    private float lesserSpeed = 0.5f;
    // POLYMORPHISM
    protected override void AssignValues()
    {
        health = lesserHealth;
        damage = lesserDamage;
        moveSpeed = lesserSpeed;
        targetValue = lesserValue;
    } 
}
