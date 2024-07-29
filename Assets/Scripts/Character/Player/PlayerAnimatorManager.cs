using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    PlayerManager player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }
    private void OnAnimatorMove()
    {
        if (player.applyRootMotion)
        {
            Vector3 veloctiy = player.animator.deltaPosition;
            player.characterController.Move(veloctiy);
            player.transform.rotation *= player.animator.deltaRotation;
        }
    }
}
