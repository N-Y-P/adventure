using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Inventory_my : MonoBehaviour
{
    private InvenInputScript _inveninput;//newinputsystem
   // private PlayerInput _playerInput;

    public static bool invectoryActivated = false;  // 인벤토리 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 
    [Header("보이지 않게 할 UI들")]
    [SerializeField]//나중에 수정 필요-해당 ui빼고 다 안보이게끔 만들어야 함!!!! 지금은 임시임****
    private GameObject bag_icon; //가방 아이콘
    [SerializeField]
    private GameObject quest_icon; //퀘스트 아이콘
    [Space (10)]
    [SerializeField]
    private TestPlayerMove _testplayermove;
    //private PlayerMove moveScr;

    private Slot[] slots;  // 슬롯들 배열

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        _inveninput = GetComponent<InvenInputScript>();
        //_playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if(_inveninput.inven)
        {
            TryOpenInventory();
        }
        
    }

    public void TryOpenInventory()
    {
        invectoryActivated = !invectoryActivated;

        if (invectoryActivated)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        bag_icon.SetActive(false);
        quest_icon.SetActive(false);
        _testplayermove.enabled = false;
        
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        bag_icon.SetActive(true);
        quest_icon.SetActive(true);
        _testplayermove.enabled = true;
        
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}