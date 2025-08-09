using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionNumber : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        this.text.text = "V" + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
