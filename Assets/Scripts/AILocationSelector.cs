using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocationSelector : MonoBehaviour
{
    public Transform[] locations;

    private int id;

    public void NewTarget()
    {
        id = Mathf.RoundToInt(Random.Range(0f, 21f));
        base.transform.position = locations[id].position;
    }
}
