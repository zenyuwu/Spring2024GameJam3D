using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float angle;
    [SerializeField] bool spinX;
    [SerializeField] bool spinY;
    [SerializeField] bool spinZ;
    void Update()
    {
        if(spinX) transform.Rotate(angle * Time.deltaTime, 0,0);
        if(spinY) transform.Rotate(0, angle * Time.deltaTime, 0);
        if(spinZ) transform.Rotate(0, 0, angle * Time.deltaTime);

    }
}
