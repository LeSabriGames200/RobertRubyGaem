using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobertAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float madValue = 3f;
    public AudioSource audio;
    public Animator sprite;
    public GameController gc;

    private float coolDown;
    private float timer;
    private bool isTimerActive;
    private bool isCoolDown;
    
    // Start is called before the first frame update
    void Start()
    {
        coolDown = madValue;
        isCoolDown = true;
        agent.enabled = false;
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown < 0)
        {
            coolDown = madValue;
            agent.enabled = true;
            agent.SetDestination(player.position);
            isCoolDown = false;
            isTimerActive = true;
            audio.Play();
            sprite.SetTrigger("RobertAngry");
        }
        if(isCoolDown)
        {
            coolDown -= Time.deltaTime;
        }
        if (isTimerActive)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            agent.enabled = false;
            isCoolDown = true;
            isTimerActive = false;
            timer = 0.5f;
        }
    }

    public void Angrier()
    {
        madValue -= 0.1f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gc.GameOver();
        }
    }
}
