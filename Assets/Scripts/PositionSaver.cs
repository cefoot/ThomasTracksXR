using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaver : MonoBehaviour
{
    private string KEY_PosX;
    private string KEY_PosY;
    private string KEY_PosZ;
    private string KEY_RotX;
    private string KEY_RotY;
    private string KEY_RotZ;
    private string KEY_SclX;
    private string KEY_SclY;
    private string KEY_SclZ;

    private void OnEnable()
    {
        KEY_PosX = $"{name}_posx";
        KEY_PosY = $"{name}_posy";
        KEY_PosZ = $"{name}_posz";
        KEY_RotX = $"{name}_rotx";
        KEY_RotY = $"{name}_roty";
        KEY_RotZ = $"{name}_rotz";
        KEY_SclX = $"{name}_scalx";
        KEY_SclY = $"{name}_scaly";
        KEY_SclZ = $"{name}_scalz";
        if (!PlayerPrefs.HasKey(KEY_PosX)) return;
        transform.position = new Vector3(PlayerPrefs.GetFloat(KEY_PosX), PlayerPrefs.GetFloat(KEY_PosY), PlayerPrefs.GetFloat(KEY_PosZ));
        transform.rotation = Quaternion.Euler(new Vector3(PlayerPrefs.GetFloat(KEY_RotX), PlayerPrefs.GetFloat(KEY_RotY), PlayerPrefs.GetFloat(KEY_RotZ)));
        transform.localScale = new Vector3(PlayerPrefs.GetFloat(KEY_SclX), PlayerPrefs.GetFloat(KEY_SclY), PlayerPrefs.GetFloat(KEY_SclZ));
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat(KEY_SclX, transform.localScale.x);
        PlayerPrefs.SetFloat(KEY_SclY, transform.localScale.y);
        PlayerPrefs.SetFloat(KEY_SclZ, transform.localScale.z);
        PlayerPrefs.SetFloat(KEY_PosX, transform.position.x);
        PlayerPrefs.SetFloat(KEY_PosY, transform.position.y);
        PlayerPrefs.SetFloat(KEY_PosZ, transform.position.z);
        PlayerPrefs.SetFloat(KEY_RotX, transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat(KEY_RotY, transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat(KEY_RotZ, transform.rotation.eulerAngles.z);
        PlayerPrefs.Save();

    }
}
