using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInputscript : MonoBehaviour
{
    //[Header("�κ��丮 ����")]
    //[SerializeField] private Inventory_my inventoryMy; // �ν����Ϳ��� ����

    [Header("������ input")]
    public bool itempickup;//E������ ������ �Ⱦ�

    public void OnItemPickup(InputValue value)
    {
        ItemPickupInput(value.isPressed);
    }
    public void ItemPickupInput(bool newItemPickupState)
    {
        itempickup = newItemPickupState;

    }
    

}
