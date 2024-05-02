using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveNPC();
    }

    private void MoveNPC()
    {
        agent.destination = destination.transform.position;
        bool isMoving = Mathf.Abs(agent.velocity.z) > Mathf.Epsilon;
        animator.SetBool("isMoving", isMoving);
    }
}
