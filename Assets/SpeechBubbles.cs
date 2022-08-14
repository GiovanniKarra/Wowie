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

        for (int i = 0; i < (int)TYPE.MAX-1; i++)
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

        if (dog.interest == null)
        {
            spr1.sprite = null;
            spr2.sprite = sprites[(int)rankedTypes[0]];
            spr3.sprite = sprites[(int)rankedTypes[1]];

            sr1.transform.localScale = Vector2.zero;
            sr2.transform.localScale = Vector2.one * dog.interestValues[(int)rankedTypes[0]] / 100f;
            sr3.transform.localScale = Vector2.one * dog.interestValues[(int)rankedTypes[1]] / 100f;
        }
        else
        {
            rankedTypes.Remove(dog.interest.type);

            spr1.sprite = sprites[dog.interest.Type];
            spr2.sprite = sprites[(int)rankedTypes[0]];
            spr3.sprite = sprites[(int)rankedTypes[1]];

            sr1.transform.localScale = Vector2.one * dog.interestValues[dog.interest.Type] / 100f;
            sr2.transform.localScale = Vector2.one * dog.interestValues[(int)rankedTypes[0]] / 100f;
            sr3.transform.localScale = Vector2.one * dog.interestValues[(int)rankedTypes[1]] / 100f;
        }
    }
}
