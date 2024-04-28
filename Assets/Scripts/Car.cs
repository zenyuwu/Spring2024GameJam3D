using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] GameObject kaboom;
    [SerializeField] GameObject car1;
    [SerializeField] GameObject car2;
    [SerializeField]CoolDogController controller;
    public bool carCrash = false;
    [SerializeField] public float bounce = 20f;
    Transform boomTransform;
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
        if(carCrash == false)
        {
            controller.rb.AddForce(Vector3.up * bounce, ForceMode.Impulse);
        }

        car1.GetComponent<SpriteRenderer>().enabled = false;
        car2.GetComponent<SpriteRenderer>().enabled = true;
        //Vector3 scaleChange = gameObject.transform.localScale;
        GetComponent<BoxCollider>().size = new Vector3(3.018f, 1.1f, 1f);
        carCrash = true;
        
        Instantiate(kaboom, transform);
        //gameObject.transform.localScale = scaleChange;
    }
}
