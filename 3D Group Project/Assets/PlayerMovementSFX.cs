using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSFX : MonoBehaviour
{
    [SerializeField] private AudioClip walkSFX;
    [Min(1), SerializeField] private int audioSpeed = 1;

    private CharacterController character;
    private float audioLength = 0;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        audioLength += Time.deltaTime;

        bool isMoving = Mathf.Abs(character.velocity.z) > Mathf.Epsilon;
        if (isMoving && audioLength >= walkSFX.length * (1 / audioSpeed))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(walkSFX, 10);
            audioLength = 0;
        }
    }
}
