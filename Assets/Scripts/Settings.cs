using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Slider mouseSensitivity;
    public TMP_Text sensitivityText;

    public void Start()
    {
        mouseSensitivity.value = PlayerPrefs.GetFloat("MouseSensitivity");
    }

    public void Update()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity.value);
        sensitivityText.text = "Mouse Sensitivity: " + PlayerPrefs.GetFloat("MouseSensitivity");
    }
}
