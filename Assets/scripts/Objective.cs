using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public bool completed;
    [TextArea(1, 10)]
    public string text;
    public bool secret;
}
