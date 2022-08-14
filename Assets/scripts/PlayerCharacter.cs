using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Inputs ctrl;
    private Vector2 inputs;
    public bool interacting;

    [HideInInspector]
    public Rigidbody2D rb;

    public float speed;

    Dog dog;
    bool stun;
    float unstunTime;

    private void Awake()
    {
        SetUpControlls();
        rb = GetComponent<Rigidbody2D>();
        dog = FindObjectOfType<Dog>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, dog.transform.position) < 1f)
        {
            dog.Unfree();
        }
    }

    public void Fall()
    {
        dog.Free();
        rb.velocity = Vector2.zero;
        stun = true;
        unstunTime = Time.time + 2;
    }

    void Move()
    {
        if (stun)
        {
            if (unstunTime <= Time.time) stun = false;
            else return;
        }
        rb.velocity = inputs.normalized * speed;
    }

    #region Controlls

    private void SetUpControlls()
    {
        ctrl = new Inputs();

        ctrl.Gameplay.Interact.started += ctx => interacting = true;
        ctrl.Gameplay.Interact.canceled += ctx => interacting = false;


        //Horizontal
        ctrl.Gameplay.Left.started += ctx => inputs.x -= 1;
        ctrl.Gameplay.Right.started += ctx => inputs.x += 1;

        ctrl.Gameplay.Left.canceled += ctx => inputs.x -= -1;
        ctrl.Gameplay.Right.canceled += ctx => inputs.x += -1;

        //Vertical
        ctrl.Gameplay.Down.started += ctx => inputs.y -= 1;
        ctrl.Gameplay.Up.started += ctx => inputs.y += 1;

        ctrl.Gameplay.Down.canceled += ctx => inputs.y -= -1;
        ctrl.Gameplay.Up.canceled += ctx => inputs.y += -1;
    }


    private void OnEnable()
    {
        if (ctrl != null)
            ctrl.Gameplay.Enable();
    }

    private void OnDisable()
    {
        if (ctrl != null)
            ctrl.Gameplay.Disable();
    }
    #endregion
}
