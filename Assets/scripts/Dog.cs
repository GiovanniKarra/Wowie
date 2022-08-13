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

    Vector3 stopPoint;
    float stopRange;

    float boost = 1;

    float[] interestValues = { 50, 50, 50 }; // piss, horniness, aggro

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
        Stop();
    }

    private void Update()
    {
        InterestDetect();
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

    void Stop()
    {
        if ((stopPoint - transform.position).magnitude <= stopRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void GoTowards(Vector3 target, float radius, float mod=1)
    {
        Vector2 direction = target - transform.position;
        rb.velocity = direction.normalized * speed * mod;

        stopPoint = target;
        stopRange = radius;
    }

    void InterestDetect()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, perceptionRange, Vector3.forward, 1, LayerMask.GetMask("Interest"));

        if (hits.Length != 0 && mode != MODE.INTEREST)
        {
            foreach (RaycastHit2D hit in hits)
            {
                InterestPoint interestPoint = hit.collider.GetComponent<InterestPoint>();
                int type = (int)interestPoint.type;

                NewInterest(interestPoint, type);
            }
        }
    }

    void NewInterest(InterestPoint newInterest, int type)
    {
        interestValues[type] += newInterest.value * Time.deltaTime;

        if (interestValues[type] >= 100)
        {
            interest = newInterest.gameObject;
            boost = 4;
            mode = MODE.INTEREST;
        }
    }

    void Wander(Vector2 center, float radius)
    {
        if (rb.velocity != Vector2.zero) return;
        if (Random.Range(0, 100f) > 1.8f) return;

        float randX = Random.Range(center.x - radius, center.x + radius);
        float randY = Random.Range(
            -Mathf.Sin(Mathf.Acos((randX-center.x)/radius))*radius+center.y,
            Mathf.Sin(Mathf.Acos((randX-center.x)/radius))*radius+center.y);
        Vector2 RandPos = new Vector2(randX, randY);

        GoTowards(RandPos, 0.05f, 0.65f);

        stopPoint = RandPos;
        stopRange = 0.05f;
    }

    void Move()
    {
        switch (mode)
        {
            case MODE.NORMAL:
                if (player.rb.velocity != Vector2.zero) GoTowards(player.transform.position, 2);
                else Wander(player.transform.position, 2.5f);
                break;
            case MODE.INTEREST:
                GoTowards(interest.transform.position, 1.5f, boost);
                boost = Mathf.Lerp(boost, 1, Time.deltaTime);
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
