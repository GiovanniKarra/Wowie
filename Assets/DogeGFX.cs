using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeGFX : MonoBehaviour
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
        anim.Play(rb.velocity == Vector2.zero ? "Idle" : "Run");
        if (rb.velocity.x > 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        if (rb.velocity.x < 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
    }
}
