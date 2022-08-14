using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMan : MonoBehaviour
{
    public static ObjectiveMan inst;

    public Objective[] objectives;

    private void Awake()
    {
        inst = this;
    }
}
