using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetStomped()
    {
        Vector3 scaleChange = gameObject.transform.localScale;
        scaleChange.y = 0.3f;
        gameObject.transform.localScale = scaleChange;
    }
}
