using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float speed;
    public float perceptionRange;

    InterestPoint interest;
    MODE mode;

    PlayerCharacter player;
    [HideInInspector] public Rigidbody2D rb;
    DistanceJoint2D dj;
    float ropeRange;
    GameObject piss;
    Rope rope;

    Vector3 stopPoint;
    float stopRange;

    float boost = 1;
    bool wandering = false;

    public float[] interestValues = { 50, 50, 50 }; // piss, horniness, aggro

    private void Awake()
    {
        player = FindObjectOfType<PlayerCharacter>();
        rb = GetComponent<Rigidbody2D>();
        dj = GetComponent<DistanceJoint2D>();
        piss = Resources.Load<GameObject>(@"prefabs\Piss");
        rope = FindObjectOfType<Rope>();
    }

    private void Start()
    {
        mode = MODE.NORMAL;
        ropeRange = dj.distance;
        stopPoint = transform.position;
    }

    private void FixedUpdate()
    {
        if (player.rb.velocity != Vector2.zero) wandering = false;

        boost = Mathf.Lerp(boost, 1, Time.deltaTime);
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
        if (mode == MODE.FREE) return;

        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, player.transform.position - transform.position, ropeRange, LayerMask.GetMask("Walker"));

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Pedestrian>().Fall();
        }
    }

    void Stop(bool forced=false)
    {
        if ((stopPoint - transform.position).magnitude <= stopRange || forced)
        {
            rb.velocity = Vector2.zero;
            stopPoint = transform.position;
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
        if (mode == MODE.FREE) return;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, perceptionRange, Vector3.forward, 1, LayerMask.GetMask("Interest"));

        if (hits.Length != 0 && mode != MODE.INTEREST)
        {
            foreach (RaycastHit2D hit in hits)
            {
                InterestPoint interestPoint = hit.collider.GetComponent<InterestPoint>();
                if (!interestPoint.available) continue;
                NewInterest(interestPoint, interestPoint.Type);
            }
        }
    }

    void NewInterest(InterestPoint newInterest, int type)
    {
        interestValues[type] += newInterest.value * Time.deltaTime;

        if (interestValues[type] >= 100)
        {
            interest = newInterest;
            boost = 4f;
            mode = MODE.INTEREST;
        }
    }

    void InterestInteract()
    {
        //  /!\
        // valeurs placeholder
        //  /!\

        if (rb.velocity == Vector2.zero && (interest.transform.position - transform.position).magnitude <= interest.radius)
        {
            interestValues[interest.Type] -= Mathf.Min(10 * Time.deltaTime, interestValues[interest.Type]);
            if (interest.available)
            {
                Instantiate(piss, interest.transform.position, Quaternion.identity);
                interest.available = false;
            }
        }
        if (interestValues[interest.Type] <= 0 || (interestValues[interest.Type] <= 70 &&
            (interest.transform.position - transform.position).magnitude > interest.radius))
        {
            mode = MODE.NORMAL;
        }
    }

    void Wander(Vector2 center, float radius, float mod=1)
    {
        if (!wandering)
        {
            Stop(true);
            wandering = true;
        }
        if (rb.velocity != Vector2.zero) return;
        if (Random.Range(0, 100f) > 1.8f * mod) return;

        float randX = Random.Range(center.x - radius, center.x + radius);
        float randY = Random.Range(
            -Mathf.Sin(Mathf.Acos((randX-center.x)/radius))*radius+center.y,
            Mathf.Sin(Mathf.Acos((randX-center.x)/radius))*radius+center.y);
        Vector2 RandPos = new Vector2(randX, randY);

        GoTowards(RandPos, 0.05f, 0.65f * mod);

        stopPoint = RandPos;
        stopRange = 0.05f;
    }

    void Move()
    {
        switch (mode)
        {
            case MODE.NORMAL:
                if (player.rb.velocity != Vector2.zero)
                {
                    GoTowards(player.transform.position, 2);
                }
                else Wander(player.transform.position, ropeRange);
                break;

            case MODE.INTEREST:
                InterestInteract();
                GoTowards(interest.transform.position, interest.radius, boost);
                break;

            case MODE.FREE:
                if (Vector2.Dot(player.rb.velocity.normalized, (transform.position - player.transform.position).normalized) > 0.7f)
                    GoTowards((Vector2)transform.position + player.rb.velocity * 20, 0.05f);
                else Wander(player.transform.position, ropeRange * 5, 1.5f);
                break;
        }
    }

    public void Free()
    {
        mode = MODE.FREE;
        dj.distance = ropeRange * 10;
        rope.gameObject.SetActive(false);
    }

    public void Unfree()
    {
        mode = MODE.NORMAL;
        dj.distance = ropeRange;
        rope.gameObject.SetActive(true);
    }

    public Vector2 GetDirection()
    {
        return stopPoint - transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, perceptionRange);
    }

    enum MODE
    {
        NORMAL,
        INTEREST,
        FREE
    }
}
