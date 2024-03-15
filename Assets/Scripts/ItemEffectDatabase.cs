using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private SlotToolTip theSlotToolTip;

    // SlotToolTip Slot Â¡°Ë´Ù¸®
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        theSlotToolTip.ShowToolTip(_item, _pos);
    }

    // SlotToolTip Slot Â¡°Ë´Ù¸®
    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }
}
