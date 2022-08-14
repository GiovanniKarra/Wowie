using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestPoint : MonoBehaviour
{
    public TYPE type;
    public int Type { get { return (int)type; } }
    public float value;
    public float valueLoss;
    public float radius;
    [HideInInspector] public bool available;

    CircleCollider2D collider_;

    private void Awake()
    {
        available = true;
        collider_ = GetComponent<CircleCollider2D>();
        collider_.radius = 0.75f * radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

public enum TYPE
{
    PISS,
    POOP,
    AGGRO,
    HORNINESS,
    MAX
}
