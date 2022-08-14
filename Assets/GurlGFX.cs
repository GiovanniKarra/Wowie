using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GurlGFX : RbGFX
{
    public PlayerCharacter player;
    public AnimatorController leash;
    public AnimatorController unleashed;

    protected override void Update()
    {
        anim.runtimeAnimatorController = player.dog.mode == MODE.FREE ? unleashed : leash;

        if (player != null && player.stun)
        {
            anim.Play("Down");
            return;
        }

        base.Update();
    }
}
