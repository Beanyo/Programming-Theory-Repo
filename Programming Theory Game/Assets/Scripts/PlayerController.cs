using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject gunLocation;
    [SerializeField] private float speed = 3.0f;
    private float horizontalInput;
    private float verticalInput;
    private float fieldRangeX = 19f;
    private float fieldRangeZ = 15f;
    private bool gameOver = false;
    private Camera m_mainCamera;
    private MainUIHandler scoreUpdate;
    public int health = 10;

    void Start()
    {
        m_mainCamera = Camera.main;
        scoreUpdate = mainUI.GetComponent<MainUIHandler>();
        scoreUpdate.playerHealth = health;
    }

    void Update()
    {
        if (!gameOver)
        {
            Movement();
            CheckBoundary();
            RotateCharacter();
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            scoreUpdate.GameOverGenerate();

        }
    }
    private void CheckBoundary()
    {
        Vector3 limitStop = transform.position;
        if (limitStop.x < -fieldRangeX)
        {
            limitStop.x = -fieldRangeX;
        }
        if (limitStop.x > fieldRangeX)
        {
            limitStop.x = fieldRangeX;
        }
        if (limitStop.z > fieldRangeZ)
        {
            limitStop.z = fieldRangeZ;
        }
        if (limitStop.z < -fieldRangeZ)
        {
            limitStop.z = -fieldRangeZ;
        }
        transform.position = limitStop;
    }
    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // relative to world
        transform.position += Vector3.right * Time.deltaTime * speed * horizontalInput;
        transform.position += Vector3.forward * Time.deltaTime * speed * verticalInput;

    }

    private void RotateCharacter()
    {
        // generate ray from camera to mouse position
        Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        // create new instance of plane y 1 for ray to hit
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            // position loaded from distance ray travelled to hit plane
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            //rotate player to direction from target
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public void UpdateHealth(int damage)
    {
        if (!gameOver)
        {
            health -= damage;
            scoreUpdate.playerHealth = health;
        }
        if(health <= 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        gameOver = true;
    }

    private void Shoot()
    {
        GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.rotation = transform.rotation; // rotation same as player's
            pooledProjectile.transform.position = gunLocation.transform.position; // position it at player's gun
        }
    }
}
