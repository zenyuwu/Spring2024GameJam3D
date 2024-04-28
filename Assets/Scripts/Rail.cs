using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Rail : MonoBehaviour
{
    [SerializeField] public bool isRight;
    [SerializeField] public bool isOnRail;
    [SerializeField] public CoolDogController controller;

    [SerializeField] public AudioSource grind;
    [SerializeField] public AudioSource style;



    private void OnTriggerStay(Collider other)
    {
        //disable inputs for everything but jump and bark
        grind.Play();

        if (isOnRail == true)
        {
            float moveDirection = isRight ? 1 : -1;

            Vector2 velocity = controller.rb.velocity;
            velocity.x = moveDirection * ((controller.cool / controller.coolNerf) + controller.baseSpeed * 2);
            controller.rb.velocity = velocity;


            other.gameObject.GetComponent<CoolDogController>().isOnRail = true;
            other.gameObject.GetComponent<CoolDogController>().isRailRight = isRight;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //force player off rail
        grind.Stop();
        style.Play();

        controller.rb.velocity += new Vector3(0, 7.5f, 0);

        isOnRail = false;
        other.gameObject.GetComponent<CoolDogController>().isOnRail = false;
    }
}
