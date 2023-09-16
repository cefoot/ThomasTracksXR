using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTransparent : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

}
