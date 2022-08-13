using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer rope;
    Transform dog;
    Transform playerHand;

    private void Awake()
    {
        rope = GetComponent<LineRenderer>();
        dog = FindObjectOfType<Dog>().transform;
        playerHand = GameObject.Find("Hand").transform;
    }

    private void Update()
    {
        rope.SetPosition(0, playerHand.position); rope.SetPosition(1, dog.position);
    }
}
