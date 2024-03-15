using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvenInputScript : MonoBehaviour
{
    StarterAssetsInputs s_instance; //��������

    [Header("�κ��丮 ����")]
    [SerializeField] private Inventory_my inventoryMy; // �ν����Ϳ��� ����

    [Header("�κ��丮 input")]
    public bool inven;//I������ �κ��丮 ����

    private void Start()
    {
        s_instance = StarterAssetsInputs.instance;
    }
    public void OnInven(InputValue value)
    {
        InvenInput(value.isPressed);

    }
    
    public void InvenInput(bool newInvenState)
    {
        if (inventoryMy != null)
        {
            inventoryMy.TryOpenInventory();
            s_instance.UpdateCursorAndLookState(Inventory_my.invectoryActivated);
        }
    }
}
