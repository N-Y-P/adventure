using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Inventory_my : MonoBehaviour
{
    private InvenInputScript _inveninput;//newinputsystem
   // private PlayerInput _playerInput;

    public static bool invectoryActivated = false;  // �κ��丮 Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base �̹���
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot���� �θ��� Grid Setting 
    [Header("������ �ʰ� �� UI��")]
    [SerializeField]//���߿� ���� �ʿ�-�ش� ui���� �� �Ⱥ��̰Բ� ������ ��!!!! ������ �ӽ���****
    private GameObject bag_icon; //���� ������
    [SerializeField]
    private GameObject quest_icon; //����Ʈ ������
    [Space (10)]
    [SerializeField]
    private TestPlayerMove _testplayermove;
    //private PlayerMove moveScr;

    private Slot[] slots;  // ���Ե� �迭

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
                if (slots[i].item != null)  // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ���� ����
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