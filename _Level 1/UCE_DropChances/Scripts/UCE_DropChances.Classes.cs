﻿// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using System;
using UnityEngine;

// =======================================================================================
// UCE ITEM DROP CHANCE
// =======================================================================================
[Serializable]
public class UCE_ItemDropChance
{
    public ScriptableItem item;
    [Range(0, 1)] public float probability;
    public UCE_ActivationRequirements dropRequirements;
}

// =======================================================================================