using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GurlGFX : RbGFX
{
    public PlayerCharacter player;
    public RuntimeAnimatorController leash;
    public RuntimeAnimatorController unleashed;

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
