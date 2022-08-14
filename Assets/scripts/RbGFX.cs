using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbGFX : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rb;
    Pedestrian pd;

    private void Awake()
    {
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        pd= GetComponentInParent<Pedestrian>();
    }

    protected virtual void Update()
    {
        if (pd != null && pd.fell)
        {
            anim.Play("Down");
            transform.rotation = Quaternion.Euler(0, transform.rotation.y + 180, 0);
            return;
        }

        Vector2 direction = rb.velocity;
        anim.Play(direction == Vector2.zero ? "Idle" : "Run");
        if (direction.x > 0.3f)
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        if (direction.x < -0.3f)
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
    }
}
