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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
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
        UpdateNPCDialogueUI();
    }

    private void UpdateNPCDialogueUI()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPosition = player.transform.position - transform.position;
        bool isPlayerNear = playerPosition.magnitude < dialogueDistance;

        if (!isPlayerNear)
        {
            dialogueText.enabled = false;
        }

        Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, minInteractDistance) && hit.collider.CompareTag("NPC") && allowInteraction && Time.timeScale > 0)
        {
            interactText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueText.enabled = true;
                dialogue = 0;
            }
        }
        else
        {
            interactText.enabled = false;
        }

        if (dialogueText.enabled)
        {
            Time.timeScale = 0;
            dialogueText.text = hit.collider.GetComponent<NPC>().GetDialogue[dialogue];
            if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale > 0)
            {
                dialogue++;
            }
        }

        if (dialogue >= mainDialogue.Length)
        {
            dialogue = 0;
            Time.timeScale = 1;
            dialogueText.enabled = false;
        }

        if (Time.timeScale <= 0)
        {
            dialogueText.enabled = false;
        }
    }

    private void MoveNPC()
    {
        if (!moveToDestination)
        {
            return;
        }
        agent.destination = destination.position;
        bool isMoving = Mathf.Abs(agent.velocity.z) > Mathf.Epsilon;
        animator.SetBool("isMoving", isMoving);
        if (isMoving && audioLength >= walkSFX.length * (1 / audioSpeed))
        {
            audioSource.PlayOneShot(walkSFX, 10);
            audioLength = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueDistance);
    }

    public string[] GetDialogue => mainDialogue;
}
