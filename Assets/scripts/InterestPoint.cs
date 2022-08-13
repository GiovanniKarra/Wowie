using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    public TYPE type;
    public int Type { get { return (int)type; } }
    public float value;
    public float radius;
    [HideInInspector] public bool available;

    private void Awake()
    {
        available = true;
    }
}

public enum TYPE
{
    PISS,
    HORNINESS,
    AGGRO,
    MAX
}
