using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class ActionController : MonoBehaviour
{
    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    //private GameObject detectedObject; // 감지된 객체
    [SerializeField]
    //private Text actionText;  // 행동을 보여 줄 텍스트
    //private TMP_Text actionText;

    public GameObject eKeyLayoutG; // VerticalLayoutGroup이 포함된 부모 오브젝트
    public GameObject ekeyNamePrefab; // 아이템 이름을 보여주는 프리팹
    private List<GameObject> detectedItems = new List<GameObject>();

    [SerializeField]
    private Inventory_my _Inventory;// 인벤토리 
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
        // 기즈모 안의 아이템 감지 및 리스트에 추가
        //detectedItems.Clear();
        detectedItems = _ItemRange.FindINPCRange();

        // UI 업데이트
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        // 기존 UI 요소 삭제
        foreach (Transform child in eKeyLayoutG.transform)
        {
            Destroy(child.gameObject);
        }

        // 새로운 UI 요소 생성
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
        // 아이템 픽업 로직
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
                _Inventory.AcquireItem(itemPickup.item); // 인벤토리에 아이템 추가
                detectedItems.Remove(item); // 감지된 아이템 목록에서 제거
                Destroy(item); // 아이템 객체 삭제
                _iteminput.ItemPickupInput(false); // 픽업 상태 초기화
                UpdateItemUI(); // UI 업데이트
            }
        }
    }


}
