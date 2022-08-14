using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeGFX : MonoBehaviour
{
    public Animator anim;
    Dog dog;

    private void Awake()
    {
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        dog = GetComponentInParent<Dog>();
    }

    private void Update()
    {
        Vector2 direction = dog.GetDirection();
        anim.Play(direction == Vector2.zero ? "Idle" : "Run");
        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
    }
}
