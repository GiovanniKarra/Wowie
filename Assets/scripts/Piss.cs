using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piss : MonoBehaviour
{
    private void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.7f, 0.85f), Time.deltaTime * 0.8f);
    }
}
