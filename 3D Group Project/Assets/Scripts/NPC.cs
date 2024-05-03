using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private AudioClip walkSFX;
    [SerializeField] private AudioSource audioSource;
    [Min(1), SerializeField] private float audioSpeed = 1;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private string[] dialogue;

    private NavMeshAgent agent;
    private Animator animator;
    private float audioLength = 0;
    private RaycastHit hit;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        dialogueText.enabled = false;
        interactText.enabled = false;
    }

    private void Update()
    {
        audioLength += Time.deltaTime;

        MoveNPC();

        Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 5) && hit.collider.CompareTag("NPC") && Input.GetKeyDown(KeyCode.E))
        {
            dialogueText.enabled = true;
            dialogueText.text = "Some dialogue lke fsdhfs";
        }

        interactText.enabled = hit.collider.CompareTag("NPC");
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
