using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerToFollow;
    private Vector3 offset = new Vector3(0, 10, 0);
    [SerializeField] private float fieldRange = 9.5f;
    private Vector3 toSpot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        toSpot = LimitWindow();
        transform.position = toSpot;
    }

    private Vector3 LimitWindow()
    {
        Vector3 limitStop = playerToFollow.transform.position + offset;
        if (limitStop.x < -fieldRange)
        {
            limitStop.x = -fieldRange;
        }
        if (limitStop.x > fieldRange)
        {
            limitStop.x = fieldRange;
        }
        if (limitStop.z > fieldRange)
        {
            limitStop.z = fieldRange;
        }
        if (limitStop.z < -fieldRange)
        {
            limitStop.z = -fieldRange;
        }
        return limitStop;
    }
}
