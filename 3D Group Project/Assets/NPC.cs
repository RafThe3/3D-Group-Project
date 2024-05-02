using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private AudioClip walkSFX;
    [SerializeField] private AudioSource audioSource;
    [Min(1), SerializeField] private float audioSpeed = 1;

    private NavMeshAgent agent;
    private Animator animator;
    private float audioLength = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        audioLength += Time.deltaTime;

        MoveNPC();
    }

    private void MoveNPC()
    {
        agent.destination = destination.position;
        bool isMoving = Mathf.Abs(agent.velocity.z) > Mathf.Epsilon;
        animator.SetBool("isMoving", isMoving);
        if (isMoving && audioLength >= walkSFX.length * (1 / audioSpeed))
        {
            audioSource.PlayOneShot(walkSFX, 10);
            audioLength = 0;
        }
    }
}
