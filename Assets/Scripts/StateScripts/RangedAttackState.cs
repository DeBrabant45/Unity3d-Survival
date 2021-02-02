﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : BaseState
{
    private ItemSO equippedWeapon;
    private GunAmmo itemSlotGun;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        itemSlotGun = controllerReference.ItemSlot.GetComponentInChildren<GunAmmo>();
        if(itemSlotGun != null)
        {
            if(itemSlotGun.IsAmmoEmpty() == false)
            {
                controllerReference.Movement.StopMovement();
                equippedWeapon = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
                controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBack;
                controllerReference.AgentAnimations.TriggerShootAnimation();
                controllerReference.DetectionSystem.OnRangeAttackSuccessful += PreformShoot;
                RemoveAmmoWhenShooting();
            }
            else
            {
                controllerReference.TransitionToState(controllerReference.reloadRangedWeaponState);
            }
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.PreviousState);
        }
    }

    private void RemoveAmmoWhenShooting()
    {
        itemSlotGun.RemoveFromCurrentAmmoCount();
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBack;
        controllerReference.DetectionSystem.OnRangeAttackSuccessful -= PreformShoot;
        controllerReference.TransitionToState(controllerReference.movementState);
    }

    public void PreformShoot(Collider hitObject, Vector3 hitPosition, RaycastHit hit)
    {
        var target = hitObject.transform.GetComponent<Target>();
        AddDamageToTarget(target, equippedWeapon);
        AddWeaponImpactForce(hit, equippedWeapon);
        CreateWeaponImpactEffect(hit);
    }

    private static void AddDamageToTarget(Target target, ItemSO equippedItem)
    {
        if (target != null)
        {
            Debug.Log(target.transform.name);
            target.TakeDamage(((WeaponItemSO)equippedItem).MaximumDamage);
        }
    }

    private void AddWeaponImpactForce(RaycastHit hit, ItemSO equippedItem)
    {
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * ((WeaponItemSO)equippedItem).WeaponImpactForce);
        }
    }

    private void CreateWeaponImpactEffect(RaycastHit hit)
    {
        GameObject impactEffect = GameObject.Instantiate(controllerReference.DetectionSystem.ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        GameObject.Destroy(impactEffect, 2f);
    }

}