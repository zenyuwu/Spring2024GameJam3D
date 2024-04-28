using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] MoveTo target;
    [SerializeField] CoolDogController dog;
    [SerializeField] AudioSource source;
    public bool canTeleport = true;
    
    //always called, even if teleported to
    private void OnTriggerEnter(Collider other)
    {
        target.canTeleport = false;
        if (canTeleport)
        {
            source.pitch = (Random.Range(0.7f, 1.4f));
            source.Play();
            Vector3 targetPosition = target.transform.position; // Replace with your desired coordinates
            dog.rb.position = targetPosition;
            canTeleport = false;
        }
        StartCoroutine(CooldownCoroutine());
   }

    //not called by first portal, as you never 'exit'
    //only called by second portal????
/*    private void OnTriggerExit(Collider other)
    {
        canTeleport = true;
    }*/

    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // 1 second cooldown
        canTeleport = true;
    }
    IEnumerator OtherCooldownCoroutine()
    {
        yield return new WaitForSeconds(0.25f); // 1 second cooldown
        target.canTeleport = false;
    }

}
