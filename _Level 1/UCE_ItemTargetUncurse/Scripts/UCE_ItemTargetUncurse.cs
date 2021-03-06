﻿// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using UnityEngine;

#if _iMMOTOOLS && _FHIZCURSEDEQUIPMENT

// =======================================================================================
// ITEM TARGET UNCURSE
// =======================================================================================
[CreateAssetMenu(menuName = "UCE Item/UCE Item Target Uncurse", order = 999)]
public class UCE_ItemTargetUncurse : UsableItem
{
    [Header("-=-=-=- UCE Target Uncurse Item -=-=-=-")]
    [Tooltip("Remove cursed items with a curse level less or equal to")]
    public int maxCurseLevel;
    public float range;
    public string successMessage = "You uncursed: ";

    [Header("-=-=-=- Buff on Target -=-=-=-")]
    public BuffSkill applyBuff;
    public int buffLevel;
    [Range(0, 1)] public float buffChance;

    [Tooltip("Decrease amount by how many each use (can be 0)?")]
    public int decreaseAmount = 1;

    // -----------------------------------------------------------------------------------
    // CheckTarget
    // -----------------------------------------------------------------------------------
    public bool CheckTarget(Entity caster)
    {
        return caster.target != null && caster.CanAttack(caster.target);
    }

    // -----------------------------------------------------------------------------------
    // CheckDistance
    // -----------------------------------------------------------------------------------
    public bool CheckDistance(Entity caster, int skillLevel, out Vector2 destination)
    {
        if (caster.target != null)
        {
            destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position);
            return Utils.ClosestDistance(caster.collider, caster.target.collider) <= range;
        }
        destination = caster.transform.position;
        return false;
    }

    // -----------------------------------------------------------------------------------
    // Use
    // -----------------------------------------------------------------------------------
    public override void Use(Player player, int inventoryIndex)
    {
        ItemSlot slot = player.inventory[inventoryIndex];

        // -- Only activate if enough charges left
        if (decreaseAmount == 0 || slot.amount >= decreaseAmount)
        {
            if (player.target != null && player.target is Player && player.target.isAlive)
            {
                // always call base function too
                base.Use(player, inventoryIndex);

                ((Player)player.target).UCE_UnequipCursedEquipment(maxCurseLevel);
                player.target.UCE_ApplyBuff(applyBuff, buffLevel, buffChance);
                player.UCE_TargetAddMessage(successMessage + player.target.name);

                // decrease amount
                slot.DecreaseAmount(decreaseAmount);
                player.inventory[inventoryIndex] = slot;
            }
        }
    }

    // -----------------------------------------------------------------------------------
}

#endif

// =======================================================================================