using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStanceState : MovementState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, true);
        controllerReference.BlockAttack.IsBlocking = true;
        controllerReference.BlockAttack.OnBlockSuccessful += BlockReaction;
    }

    private void BlockReaction()
    {
        controllerReference.BlockAttack.OnBlockSuccessful -= BlockReaction;
        controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, false);
        controllerReference.TransitionToState(controllerReference.blockReactionState);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.BlockAttack.IsBlocking = false;
        controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, false);
    }
}
