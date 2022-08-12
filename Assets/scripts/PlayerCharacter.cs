using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Inputs ctrl;
    private Vector2 inputs;
    public bool interacting;

    private void Awake()
    {
        SetUpControlls();
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
