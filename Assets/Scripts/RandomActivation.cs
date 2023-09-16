using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomActivation : MonoBehaviour
{
    public Image[] ToBeActivated;

    public void StartActivation()
    {
        StartCoroutine(ActivateObjects());
    }

    public void ResetClip()
    {
        GetComponent<Animator>().playbackTime = 0;
    }

    public void ResetColor()
    {
        SetColor(Color.red);
    }

    public void SetColor(Color color)
    {
        Array.ForEach(ToBeActivated, e => ChangeColorOfElement(e, color));
    }

    private IEnumerator ActivateObjects()
    {
        var randomList = ToBeActivated.ToList().OrderBy((i) => UnityEngine.Random.Range(0f, 100f));
        foreach (var random in randomList)
        {
            ChangeColorOfElement(random, Color.yellow);
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(0.1f, .5f));
        }
        Array.ForEach(ToBeActivated, e => ChangeColorOfElement(e, Color.green));
    }

    private static void ChangeColorOfElement(Image elementToChange, Color clr)
    {
        Debug.Log($"Change color of '{elementToChange.name}' to '{clr}'");
        elementToChange.color = clr;
        var elements = elementToChange.GetComponentsInChildren<Image>().Where(elem => elem != elementToChange).ToArray();
        Array.ForEach(elements, e => ChangeColorOfElement(e, clr));
    }
}
