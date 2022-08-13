using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbles : MonoBehaviour
{
    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    public SpriteRenderer sr3;

    Dog dog;

    private void Awake()
    {
        dog = GetComponentInParent<Dog>();
    }

    private void Update()
    {
        for (int i = 0; i < (int)TYPE.MAX-1; i++)
        {
            //dog.interestValues[(int)TYPE];
        }
    }
}
