using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float speed;
    MODE mode;

    PlayerCharacter player;
    Rigidbody2D rb;

    private void Awake()
    {
        player = FindObjectOfType<PlayerCharacter>();
        rb = GetComponent<Rigidbody2D>();
        mode = MODE.NORMAL;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GoTowards(GameObject target, float radius)
    {
        Vector2 direction = target.transform.position - transform.position;
        if (direction.magnitude >= radius)
        {
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Move()
    {
        switch (mode)
        {
            case MODE.NORMAL:
                GoTowards(player.gameObject, 2);
                break;
            default:
                break;
        }
    }

    enum MODE
    {
        NORMAL
    }
}
