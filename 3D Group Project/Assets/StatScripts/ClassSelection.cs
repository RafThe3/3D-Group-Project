using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClassSelection : MonoBehaviour
{
    ClassFunctions classes;

    void Start()
    {
        classes = GetComponent<ClassFunctions>();
    }

    public void IsRanger()
    {
        classes.isRanger = true;
    }

    public void IsWarrior()
    {
        classes.isWarrior = true;
    }

    public void IsMage()
    {
        classes.isMage = true;
    }
}
