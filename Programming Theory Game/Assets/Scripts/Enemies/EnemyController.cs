using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{

    [SerializeField] protected float moveSpeed = 1.0f;
    // ENCAPSULATION
    public int health { get; protected set; }
    public int damage { get; protected set; }
    public int targetValue { get; protected set; }
    private GameObject player;
    private PlayerController playerController;
    private GameObject mainUI;
    private MainUIHandler scoreUpdate;

    void Start()
    {
        player = GameObject.Find("Player");
        mainUI = GameObject.Find("Canvas");
        AssignValues();
        scoreUpdate = mainUI.GetComponent<MainUIHandler>();
    }


    void Update()
    {
        MoveToPlayer();
    }
    // ABSTRACTION
    protected abstract void AssignValues();

    protected virtual void MoveToPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        transform.position += lookDirection * moveSpeed * Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        // detect if collided with Player
        if (other.gameObject.CompareTag("Player"))
        {
            // assign player's script to local accessible variable
            playerController = other.GetComponent<PlayerController>(); 
            // call UpdateHealth method of player to inflict damage
            playerController.UpdateHealth(damage);
            Destroy(gameObject);

        }
        if (other.gameObject.CompareTag("Bullet"))
        {

            // assign object hit to local projectile variable
            PlayerProjectile bullet = other.GetComponent<PlayerProjectile>();
            // remove bullet from scene
            bullet.gameObject.SetActive(false);
            Damaged(bullet.damageDeal);

        }
    }
    protected void Damaged(int hurt)
    {
        health -= hurt;
        if(health <= 0)
        {
            scoreUpdate.currentScore += targetValue;
            Destroy(gameObject);
        }
    }

}
