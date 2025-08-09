using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameController gc;
    public Transform player;
    public Material doorClosed;
    public Material doorOpen;
    public MeshCollider trigger;
    public MeshRenderer[] doorMesh = new MeshRenderer[2];
    public MeshCollider[] colider = new MeshCollider[2];
    public AudioClip open;
    public AudioClip closed;
    public AudioSource audio;

    private float timer = 3f;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit) && ((Object)raycastHit.collider == (Object)trigger & Vector3.Distance(player.position, base.transform.position) < 15f))
            {
                DoorOpen();
            }
        }
        if (isOpen && Time.timeScale != 0f)
        {
            timer -= 1f * Time.deltaTime;
        }
        if (timer <= 0f)
        {
            timer = 3f;
            DoorClosed();
        }
    }

    public void DoorOpen()
    {
        doorMesh[0].material = doorOpen;
        doorMesh[1].material = doorOpen;
        colider[0].enabled = false;
        colider[1].enabled = false;
        audio.PlayOneShot(open);
        isOpen = true;
    }
    public void DoorClosed()
    {
        doorMesh[0].material = doorClosed;
        doorMesh[1].material = doorClosed;
        colider[0].enabled = true;
        colider[1].enabled = true;
        audio.PlayOneShot(closed);
        isOpen = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") || other.CompareTag("Robert"))
        {
            DoorOpen();
        }
        
    }
}
