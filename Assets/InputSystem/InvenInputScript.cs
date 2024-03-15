using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvenInputScript : MonoBehaviour
{
    StarterAssetsInputs s_instance; //가져오기

    [Header("인벤토리 참조")]
    [SerializeField] private Inventory_my inventoryMy; // 인스펙터에서 설정

    [Header("인벤토리 input")]
    public bool inven;//I누르면 인벤토리 열기

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
