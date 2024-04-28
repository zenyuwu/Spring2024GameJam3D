using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 2.0f; 
    [SerializeField] bool goingToB = true; 

    void Update()
    {
        if (goingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB.position) <= 0.1f) goingToB = false;
            
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA.position) <= 0.1f) goingToB = true;

        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (rb != null)
        {
            // Rigidbody detached on exit
            rb.transform.parent = null;
            rb = null;
        }
    }
}
