using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAttackStanceState : AttackStanceState
{
    public override void HandleSecondaryHeldDownInput()
    {
        controllerReference.TransitionToState(controllerReference.rangedWeaponAimState);
    }

    public override void HandleReloadInput()
    {
        controllerReference.TransitionToState(controllerReference.reloadRangedWeaponState);
    }
}