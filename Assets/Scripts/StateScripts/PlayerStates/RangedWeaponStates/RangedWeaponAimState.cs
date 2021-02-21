using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAimController.IsAimActive = true;
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAimController.IsAimActive = false;
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackState);
    }

    public override void HandleAimInput()
    {
        base.HandleAimInput();
        controllerReference.AgentAimController.IsAimActive = false;
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void HandleReloadInput()
    {
        base.HandleReloadInput();
        controllerReference.AgentAimController.IsAimActive = false;
        controllerReference.TransitionToState(controllerReference.reloadRangedWeaponState);
    }

    public override void Update()
    {
        base.Update();
        controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
    }
}