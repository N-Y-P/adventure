using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class ActionController : MonoBehaviour
{
    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 
    //private GameObject detectedObject; // ������ ��ü
    [SerializeField]
    //private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ
    //private TMP_Text actionText;

    public GameObject eKeyLayoutG; // VerticalLayoutGroup�� ���Ե� �θ� ������Ʈ
    public GameObject ekeyNamePrefab; // ������ �̸��� �����ִ� ������
    private List<GameObject> detectedItems = new List<GameObject>();

    [SerializeField]
    private Inventory_my _Inventory;// �κ��丮 
    [SerializeField] 
    ItemRange _ItemRange;
    private ItemInputscript _iteminput;//newinputsystem

    void Start()
    {
        _iteminput = GetComponent<ItemInputscript>();
    }
    void Update()
    {
        ItemPickup();
        UpdateDetectedItems();
    }
    private void UpdateDetectedItems()
    {
        // ����� ���� ������ ���� �� ����Ʈ�� �߰�
        //detectedItems.Clear();
        detectedItems = _ItemRange.FindINPCRange();

        // UI ������Ʈ
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        // ���� UI ��� ����
        foreach (Transform child in eKeyLayoutG.transform)
        {
            Destroy(child.gameObject);
        }

        // ���ο� UI ��� ����
        foreach (var item in detectedItems)
        {
            var itemUI = Instantiate(ekeyNamePrefab, eKeyLayoutG.transform);
            var itemText = itemUI.GetComponentInChildren<TMP_Text>();
            var itemPickup = item.GetComponent<ItemPickup>();

            if (itemText != null && itemPickup != null)
            {
                itemText.text = "<color=yellow>" + "[E]" + "</color>" + itemPickup.item.itemName;
            }
        }
    }
    private void ItemPickup()
    {
        // ������ �Ⱦ� ����
        if (detectedItems.Count > 0 && _iteminput.itempickup)
        {
            CanPickUp(detectedItems[0]);
        }
    }

    private void CanPickUp(GameObject item)
    {
        if (item != null)
        {
            ItemPickup itemPickup = item.GetComponent<ItemPickup>();
            if (itemPickup != null && itemPickup.item != null)
            {
                _Inventory.AcquireItem(itemPickup.item); // �κ��丮�� ������ �߰�
                detectedItems.Remove(item); // ������ ������ ��Ͽ��� ����
                Destroy(item); // ������ ��ü ����
                _iteminput.ItemPickupInput(false); // �Ⱦ� ���� �ʱ�ȭ
                UpdateItemUI(); // UI ������Ʈ
            }
        }
    }


}
