using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NPC : MonoBehaviour
{
    [TextArea(3, 5), SerializeField] private string[] mainDialogue;
    //[TextArea(3, 5), SerializeField] private string[] randomDialogue;
    [SerializeField] private bool allowInteraction = true;
    [SerializeField] private bool moveToDestination = false;
    [SerializeField] private Transform destination;
    [SerializeField] private AudioClip walkSFX;
    [SerializeField] private AudioSource audioSource;
    [Min(1), SerializeField] private float audioSpeed = 1;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI interactText;
    [Min(0), SerializeField] private float minInteractDistance = 1;
    [SerializeField] private float dialogueDistance = 1;

    private NavMeshAgent agent;
    private Animator animator;
    private float audioLength = 0;
    private RaycastHit hit;
    private int dialogue = 0;
    private GameObject[] NPCs;
    private bool isPlayer = false;
    private CapsuleCollider cldr;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        cldr = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        dialogueText.enabled = false;
        interactText.enabled = false;
        cldr.radius = dialogueDistance;
    }

    private void Update()
    {
        audioLength += Time.deltaTime;

        MoveNPC();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && allowInteraction)
        {
            isPlayer = true;
            Vector3 playerPosition = other.transform.position - transform.position;
            bool isPlayerNear = playerPosition.magnitude < dialogueDistance;

            if (isPlayerNear && isPlayer && Time.timeScale > 0)
            {
                interactText.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogue = 0;
                    dialogueText.enabled = true;
                }
            }
            else
            {
                interactText.enabled = false;
                dialogueText.enabled = false;
            }

            if (dialogueText.enabled)
            {
                dialogueText.text = mainDialogue[dialogue];
                if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale > 0)
                {
                    dialogue++;
                }
            }

            if (dialogue >= mainDialogue.Length)
            {
                dialogue = 0;
                dialogueText.enabled = false;
            }
        }
    }

    private void MoveNPC()
    {
        if (moveToDestination)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueDistance);
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayer = false;
    }
}
