using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCutsceneHandler : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject desc;

    void Start()
    {
        StartCoroutine(FlashArrow());
    }

    IEnumerator FlashArrow()
    {
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(false);

        desc.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(true);
    }
}
