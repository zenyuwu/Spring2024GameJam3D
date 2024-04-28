using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashNStretch : MonoBehaviour
{
    [SerializeField] public float squashAmount = 0.5f; 
    [SerializeField] public Rigidbody rb; 

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = transform.localScale;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        float yVelocity = rb != null ? rb.velocity.y : Input.GetAxis("Vertical");
        float squashFactor = Mathf.Abs(yVelocity) * squashAmount; 

        float newScaleY = originalScale.y - (squashFactor * Mathf.Sign(yVelocity));
        float newPosition = originalPosition.y - (squashFactor * Mathf.Sign(yVelocity));

        transform.localScale = new Vector3(originalScale.x, Mathf.Clamp(newScaleY, 0.1f, 2.0f), originalScale.z);
        transform.localPosition = new Vector3(originalPosition.x, Mathf.Clamp(newPosition, 0.0f, 2.0f), originalPosition.z);
    }
}