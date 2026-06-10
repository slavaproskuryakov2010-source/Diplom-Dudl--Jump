using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera : MonoBehaviour
{
    public Transform target;

    public void Update()
    {
        if (target.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
