using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipItemState : BaseState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.AgentAnimations.SetTriggerForAnimation("unequipItem");
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger += UnequipItem;
    }

    public void UnequipItem()
    {
        ItemSpawnManager.Instance.SwapHandItemToPlayersBack();
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= UnequipItem;
        controllerReference.TransitionToState(controllerReference.movementState);
    }
}
