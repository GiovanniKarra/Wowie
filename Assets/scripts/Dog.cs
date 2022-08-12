using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float speed;
    public float perceptionRange;

    GameObject interest;
    MODE mode;

    PlayerCharacter player;
    Rigidbody2D rb;
    DistanceJoint2D dj;
    float ropeRange;

    private void Awake()
    {
        player = FindObjectOfType<PlayerCharacter>();
        rb = GetComponent<Rigidbody2D>();
        mode = MODE.NORMAL;
        dj = GetComponent<DistanceJoint2D>();
        ropeRange = dj.distance;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Detect();
        RopeDetect();
    }

    void RopeDetect()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, player.transform.position - transform.position, ropeRange, LayerMask.GetMask("Walker"));

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Pedestrian>().Fall();
        }
    }

    void GoTowards(GameObject target, float radius, float mod=1)
    {
        Vector2 direction = target.transform.position - transform.position;
        if (direction.magnitude >= radius)
        {
            rb.velocity = direction.normalized * speed * mod;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Detect()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, perceptionRange, Vector3.forward, 1, LayerMask.GetMask("Interest"));

        if (hits.Length != 0)
        {
            interest = hits[0].collider.gameObject;
            mode = MODE.INTEREST;
        }
    }

    void Move()
    {
        switch (mode)
        {
            case MODE.NORMAL:
                GoTowards(player.gameObject, 2);
                break;
            case MODE.INTEREST:
                GoTowards(interest, 1.5f, 2);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, perceptionRange);
    }

    enum MODE
    {
        NORMAL,
        INTEREST
    }
}
