using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    PlayerCharacter player;
    Rigidbody2D rb;

    public float speed;
    bool moving = false;
    Vector3 targetPos;

    bool fell = false;

    MOOD mood;
    [HideInInspector] public Vector2 direction;
    Queue<Vector2> posQueue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerCharacter>();
        posQueue = new Queue<Vector2>();
    }

    private void Start()
    {
        mood = MOOD.WANDER;

        for (int i = 0; i < 5; i++)
        {
            posQueue.Enqueue(Vector2.zero);
        }
    }

    private void FixedUpdate()
    {
        Vector2 oldPos = posQueue.Dequeue();
        posQueue.Enqueue(transform.position);

        direction = ((Vector2)transform.position - oldPos).normalized;

        if (!fell)
        {
            switch (mood)
            {
                case MOOD.WANDER:
                    if (moving && (transform.position - targetPos).magnitude < 0.5f)
                    {
                        rb.velocity = Vector2.zero;
                        moving = false;
                    }
                    Wander(20);
                    break;
                case MOOD.ANGRY:
                    GoTowards(player.transform.position);
                    if ((transform.position - targetPos).magnitude < 1f)
                    {
                        rb.velocity = Vector2.zero;
                        moving = false;
                        player.Fall();
                        mood = MOOD.WANDER;
                    }
                    break;
            }
        }
    }

    void GoTowards(Vector3 target, float mod = 1)
    {
        Vector2 direction = target - transform.position;
        rb.velocity = direction.normalized * speed * mod;

        moving = true;
        targetPos = target;
    }

    void Wander(float radius)
    {
        fell = false;

        if (moving) return;
        if (Random.Range(0, 100f) > 2f) return;

        Vector2 center = transform.position;
        float randX = Random.Range(center.x - radius, center.x + radius);
        float randY = Random.Range(
            -Mathf.Sin(Mathf.Acos((randX - center.x) / radius)) * radius + center.y,
            Mathf.Sin(Mathf.Acos((randX - center.x) / radius)) * radius + center.y);
        Vector2 RandPos = new Vector2(randX, randY);

        Vector2 direction = RandPos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, LayerMask.GetMask("Obstacles"));

        if (hit) { RandPos = hit.point - direction.normalized * 1.5f; print(hit.point);  }

        GoTowards(RandPos, 0.5f);
    }

    void Stop()
    {
        targetPos = transform.position;
        rb.velocity = Vector2.zero;
        moving = false;
    }

    public void Fall()
    {
        ObjectiveMan.inst.Trigger(OBJECTIVE.TREBUCHER);

        fell = true;

        Vector2 direction = (targetPos - transform.position).normalized; // si jamais je veux le propulser en avant
        Stop();

        Invoke("Unfall", 4);
    }

    void Unfall()
    {
        fell = false;
        mood = MOOD.ANGRY;
    }

    enum MOOD
    {
        WANDER,
        ANGRY
    }
}
