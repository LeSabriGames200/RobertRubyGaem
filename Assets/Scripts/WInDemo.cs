using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInDemo : MonoBehaviour
{
    public AudioSource audio;
    public GameObject demoScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            this.gameObject.SetActive(false);
            demoScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
