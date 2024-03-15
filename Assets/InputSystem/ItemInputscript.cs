using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInputscript : MonoBehaviour
{
    //[Header("인벤토리 참조")]
    //[SerializeField] private Inventory_my inventoryMy; // 인스펙터에서 설정

    [Header("아이템 input")]
    public bool itempickup;//E누르면 아이템 픽업

    public void OnItemPickup(InputValue value)
    {
        ItemPickupInput(value.isPressed);
    }
    public void ItemPickupInput(bool newItemPickupState)
    {
        itempickup = newItemPickupState;

    }
    

}
