using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbles : MonoBehaviour
{
    public Sprite[] sprites;

    [Header("Bubbles")]
    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    public SpriteRenderer sr3;
    [Header("In the bubbles")]
    public SpriteRenderer spr1;
    public SpriteRenderer spr2;
    public SpriteRenderer spr3;

    Dog dog;

    private void Awake()
    {
        dog = GetComponentInParent<Dog>();
    }

    private void Update()
    {
        List<TYPE> rankedTypes = new List<TYPE>();
        List<float> scores = new List<float>();

        for (int i = 0; i < (int)TYPE.MAX; i++)
        {
            float score = dog.interestValues[i];

            for (int j = 0; j < 3; j++)
            {
                if (rankedTypes.Count <= j ||score > scores[j])
                {
                    rankedTypes.Insert(j, (TYPE)i);
                    scores.Insert(j, score);
                    break;
                }
            }
        }

        print($"{rankedTypes[0]} / {rankedTypes[1]} / {rankedTypes[2]}");
    }
}
