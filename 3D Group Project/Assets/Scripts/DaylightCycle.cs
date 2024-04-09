using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycle : MonoBehaviour
{
    private const float defaultSpeed = 0.001f;
    [Min(0), SerializeField] private float speed = 1;

    private void Start()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        CycleDay();
    }

    private void CycleDay()
    {
        if (Time.timeScale == 1)
        {
            transform.Rotate(new Vector3(defaultSpeed * speed, 0, 0));
        }
    }
}
