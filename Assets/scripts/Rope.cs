using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer rope;
    Transform dog;
    Transform player;

    private void Awake()
    {
        rope = GetComponent<LineRenderer>();
        dog = FindObjectOfType<Dog>().transform;
        player = FindObjectOfType<PlayerCharacter>().transform;
    }

    private void Update()
    {
        print(dog); print(player);
        rope.SetPosition(0, player.position); rope.SetPosition(1, dog.position);
    }
}
