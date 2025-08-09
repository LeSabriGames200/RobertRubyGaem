using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyMaze : MonoBehaviour
{
    public RubyGame rb;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            rb.CheckifRuby();
            gameObject.SetActive(false);
        }
    }
}
