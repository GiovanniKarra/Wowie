using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer rope;
    Transform dog;
    Transform playerHand;

    int playerSortingOrder;

    private void Awake()
    {
        rope = GetComponent<LineRenderer>();
        dog = FindObjectOfType<Dog>().transform;
        playerHand = GameObject.Find("Hand").transform;
        playerSortingOrder = playerHand.GetComponentInParent<SpriteRenderer>().sortingOrder;
    }

    private void Update()
    {
        if (dog.position.y < playerHand.position.y)
        {
            rope.sortingLayerName = "Default";
            rope.sortingOrder = playerSortingOrder + 1;
        }
        else
        {
            rope.sortingLayerName = "Under";
        }
        rope.SetPosition(0, playerHand.position); rope.SetPosition(1, dog.position);
    }
}
