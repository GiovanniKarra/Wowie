using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public OBJECTIVE objective;
    public bool completed;
    [TextArea(1, 10)]
    public string text;
    public bool secret;
}

public enum OBJECTIVE
{
    TREBUCHER
}