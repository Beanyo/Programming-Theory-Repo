using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speed = 3.0f;
    private float xRange = 20;
    [SerializeField] private GameObject gunLocation;
    private Camera m_mainCamera;

    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        m_mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundary();
        Movement();
        RotateCharacter();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void CheckBoundary()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
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
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Game Over");
        }
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
