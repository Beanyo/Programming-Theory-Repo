using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speed = 3.0f;
    private float fieldRangeX = 19f;
    private float fieldRangeZ = 15f;
    [SerializeField] private GameObject gunLocation;
    private Camera m_mainCamera;
    [SerializeField] private GameObject mainUI;
    private MainUIHandler scoreUpdate;
    private bool gameOver = false;

    public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        m_mainCamera = Camera.main;
        scoreUpdate = mainUI.GetComponent<MainUIHandler>();
        scoreUpdate.playerHealth = health;
    }

    // Update is called once per frame
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

        //relative to object
        /*        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput); 
                transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);*/
    }

    private void RotateCharacter()
    {
        //begin testing turning player to face mouse position
        Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
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
        Debug.Log("Game Over");
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
