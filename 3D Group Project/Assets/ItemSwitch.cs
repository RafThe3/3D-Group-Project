using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitch : MonoBehaviour
{
    [SerializeField] private GameObject item1, item2;
    [SerializeField] private bool enableItem2 = false;

    private readonly KeyCode one = KeyCode.Alpha1, two = KeyCode.Alpha2;

    private void Start()
    {
        item1.SetActive(true);
        item2.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (item2 && enableItem2)
        {
            if (Input.GetKeyDown(one))
            {
                item1.SetActive(true);
                item2.SetActive(false);
            }
            else if (Input.GetKeyDown(two))
            {
                item1.SetActive(false);
                item2.SetActive(true);
            }
        }
    }

    public void EnableItem2()
    {
        enableItem2 = true;
        item1.SetActive(false);
        item2.SetActive(true);
    }
}
