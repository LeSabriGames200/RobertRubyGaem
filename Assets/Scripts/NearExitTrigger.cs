using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearExitTrigger : MonoBehaviour
{
    public GameObject exitBlocker;
    public GameController gc;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") & gc.finalMode & gc.exitReached < 1)
        {
            exitBlocker.SetActive(true);
            gc.PlayExitClosedSound();
            gc.exitReached++;
        }
    }
}
