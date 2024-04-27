using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    [SerializeField] public bool isRight;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<CoolDogController>().isOnRail = true;
        other.gameObject.GetComponent<CoolDogController>().isRailRight = isRight;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<CoolDogController>().isOnRail = false;
    }
}
