﻿using Mirror;
using UnityEngine.UI;

public partial class Player
{
    // Removes the equipment item when left clicking.
    [Command]
    public void CmdRemoveEquipmentItem(int index)
    {
#if _iMMOCURSEDEQUIPMENT
        // validate
        if ((state == "IDLE" || state == "MOVING" || state == "CASTING") &&
            0 <= index && index < equipment.Count && equipment[index].amount > 0)
        {
            // check inventory for free slot and pass it to swapinventoryequip()
            ItemSlot item = equipment[index];

            if (InventorySlotsFree() >= item.amount && ((EquipmentItem)item.item.data).CanUnequipClick(this, (EquipmentItem)item.item.data))
            {
                if (item.amount > 0)
                {
                    InventoryAdd(item.item, 1);
                    item.DecreaseAmount(1);
                    equipment[index] = item;
                }
            }
        }

#else
        // validate
        if ((state == "IDLE" || state == "MOVING" || state == "CASTING") &&
0 <= index && index < equipment.Count && equipment[index].amount > 0)
        {
            // check inventory for free slot and pass it to swapinventoryequip()
            ItemSlot item = equipment[index];

            if (InventorySlotsFree() >= item.amount)
            {
                if (item.amount > 0)
                {
                    InventoryAdd(item.item, 1);
                    item.DecreaseAmount(1);
                    equipment[index] = item;
                }
            }
        }

#endif
    }
}

public partial class UIEquipment
{
    // Adds a listener to the equipment item button to allow click removal.
    private void RemoveEquipmentItem(int currentIndex, UIEquipmentSlot slot, Player player)
    {
#if _iMMOITEMRARITY
        Button button = slot.transform.GetChild(1).GetComponent<Button>();

#else
        Button button = slot.transform.GetChild(0).GetComponent<Button>();

#endif
        button.onClick.SetListener(() =>
{
    player.CmdRemoveEquipmentItem(currentIndex); // added to move equipment into open inventory slot
});
    }
}