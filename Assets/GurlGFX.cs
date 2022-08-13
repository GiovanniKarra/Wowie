using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GurlGFX : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rb;

    private void Awake()
    {
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = rb.velocity;
        anim.Play(direction == Vector2.zero ? "Idle" : "Run");
        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
    }
}
