using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CutsceneHandler : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GameObject cat;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject evilCorp;
    [SerializeField] GameObject button;

    private void Start()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        //beginning setting everything properly
        dog.SetActive(false);
        cat.SetActive(false);
        enemy.SetActive(false);
        button.SetActive(false);

        //begin animation
        dog.SetActive(true);
        cat.SetActive(true);
        yield return new WaitForSeconds(2);

        //dog leaves
        LeanTween.moveX(dog, -50, 1f);
        yield return new WaitForSeconds(2);
        dog.SetActive(false);

        //dog leaves, enemy shows up
        enemy.SetActive(true);
        yield return new WaitForSeconds(2);

        //enemy starts leaving with cat
        LeanTween.moveX(enemy, 1090f, 2f);
        LeanTween.moveX(cat, 1100f, 2f);
        LeanTween.moveX(evilCorp, 1100f, 2f);
        LeanTween.scale(enemy, new Vector3(0.2f,0.2f,0.2f), 2f);
        LeanTween.scale(cat, new Vector3(0.2f, 0.2f, 0.2f), 2f);
        LeanTween.moveLocalY(enemy, 250f, 2f);
        LeanTween.moveLocalY(cat, 250f, 2f);
        dog.SetActive(true);
        LeanTween.moveX(dog, 0f, 0f);
        LeanTween.moveX(dog, 100f, 1f);
        yield return new WaitForSeconds(3);

        LeanTween.moveX(dog, 300f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        LeanTween.rotateY(dog, 180f, 0.5f);
        yield return new WaitForSeconds(1f);

        LeanTween.rotateY(dog, 0f, 0.5f);
        yield return new WaitForSeconds(1f);

        //cutscene ends, turn button on
        button.SetActive(true);

        yield return null;
    }
}
