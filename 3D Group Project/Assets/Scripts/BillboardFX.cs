
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFX : MonoBehaviour
{
    [SerializeField] private Transform camTransform;

    private Quaternion originalRotation;

    private void Start()
    {
        if (!Camera.main.enabled)
        {
            return;
        }

        if (camTransform == null)
        {
            camTransform = Camera.main.transform;
        }

        originalRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}