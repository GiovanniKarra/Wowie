using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    Transform[] points;
    public float speed;
    Vector3 target;
    Rigidbody2D rb;
    int index = 0;

    private void Awake()
    {
        points = new Transform[transform.childCount-1];
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        for (int i = 1; i <= points.Length; i++) points[i-1] = transform.GetChild(i);
        for (int i = 0; i < points.Length; i++) points[i].parent = null;

        target = points[index].position;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(target, transform.position) < 0.2f)
        {
            index += 1;
            index %= points.Length;
            target = points[index].position;
        }

        Vector2 direction = (target - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerCharacter player)) player.Fall();
        else if (other.TryGetComponent(out Dog dog)) dog.Free();
    }
}
