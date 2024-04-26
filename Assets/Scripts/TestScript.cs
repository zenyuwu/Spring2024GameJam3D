using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent (typeof (Rigidbody))]
public class TestScript : MonoBehaviour
{

    [SerializeField] float accel = 5f;
    [SerializeField] float decel = 5f;
    [SerializeField] float maxSpeed = 20f;
    //will be replaced by InputManager.Movement
    Vector3 temp;

    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //temp if check
        if (rb != null) 
        {
            rb.AddForce(temp * accel, ForceMode.Force);

            if(rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            rb.AddForce(rb.velocity * -decel, ForceMode.Force);
        }
    }

}
