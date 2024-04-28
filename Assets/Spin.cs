using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float angle;
    void Update()
    {
        transform.Rotate(0, angle * Time.deltaTime, 0);
    }
}
