using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer rope;
    Transform dog;
    Transform playerHand;
    SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        rope = GetComponent<LineRenderer>();
        dog = FindObjectOfType<Dog>().transform;
        playerHand = GameObject.Find("Hand").transform;
        playerSpriteRenderer = playerHand.GetComponentInParent<SpriteRenderer>();
    }

    private void Update()
    {
        if (dog.position.y < playerHand.position.y)
        {
            rope.sortingLayerName = "Default";
            rope.sortingOrder = playerSpriteRenderer.sortingOrder + 1;
        }
        else
        {
            rope.sortingLayerName = "Under";
        }
        rope.SetPosition(0, playerHand.position); rope.SetPosition(1, dog.position);
        RopeDetect();
    }

    void RopeDetect()
    {
        Vector2 direction = playerHand.transform.position - dog.transform.position;
        float distance = direction.magnitude;

        RaycastHit2D hit =
            Physics2D.Raycast((Vector2)dog.transform.position + direction * 0.2f,
            direction.normalized, distance * 0.6f, LayerMask.GetMask("Walker"));

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Pedestrian>().Fall();
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector2 direction = playerHand.transform.position - dog.transform.position;
    //    float distance = direction.magnitude;
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine((Vector2)dog.transform.position + direction * 0.2f,
    //        (Vector2)dog.transform.position + direction * 0.2f + direction.normalized * distance * 0.6f);
    //}
}
