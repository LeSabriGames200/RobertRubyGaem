using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nonunningAI : MonoBehaviour
{
    public AILocationSelector aiLocationSelector;
    public Transform wanderTarget;
    public NavMeshAgent navMeshAgent;
    public Player player;
    public Transform playerTransform;
    public SpriteRenderer sprite;
    public Sprite normalSprite;
    public Sprite angrySprite;

    private bool angry;
    private bool isChasePlayer;
    private bool isWandering;
    private float coolDown;

    void Start()
    {
        
    }

    void Update()
    {
        if (angry)
        {
            sprite.sprite = angrySprite;
            TargetPlayer();
        }
        else
        {
            sprite.sprite = normalSprite;
            if (!isChasePlayer)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && coolDown <= 0f)
                {
                    Wander();
                }
            }
        }
        if (isChasePlayer)
        {
            if (!angry)
            {
                angry = true;
            }
        }
        if (coolDown > 0f)
        {
            coolDown -= 1f * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (!angry)
        {
            Vector3 direction = playerTransform.position - base.transform.position;
            RaycastHit raycastHit;
            if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & player.isRunning & !angry)
            {
                if (raycastHit.collider.CompareTag("Player") && player.isRunning)
                {
                    isChasePlayer = true;
                    angry = true;
                    isWandering = false;
                }
                else
                {
                    isChasePlayer = false;
                    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && coolDown <= 0f)
                    {
                        Wander();
                    }
                }
            }
        }
    }

    public void Wander()
    {
        aiLocationSelector.NewTarget();
        navMeshAgent.SetDestination(wanderTarget.position);
        isWandering = true;
    }

    public void TargetPlayer()
    {
        navMeshAgent.SetDestination(playerTransform.position);
        coolDown = 1;
    }
}
