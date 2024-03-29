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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerCharacter>();
    }

    private void Start()
    {
        mood = MOOD.WANDER;
    }

    private void FixedUpdate()
    {
        if (!fell)
        {
            switch (mood)
            {
                case MOOD.WANDER:
                    if (moving && (transform.position - targetPos).magnitude < 0.05f)
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
