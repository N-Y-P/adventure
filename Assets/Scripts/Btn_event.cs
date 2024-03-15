using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Btn_event : MonoBehaviour
{
    //참조하려고 가져온 인스턴스들
    Quest_my q_instance;
    Inventory_my i_instance;
    StarterAssetsInputs s_instance;
    //
    [Header("인벤토리 참조")]
    [SerializeField] private Inventory_my inventoryMy; // 인스펙터에서 설정
    private void Start()
    {
        q_instance = Quest_my.instance;
        s_instance = StarterAssetsInputs.instance;
    }
    [Header("가방 아이콘")]
    [SerializeField] public GameObject inven_content;
    public void OnBag_btn()
    {
        inventoryMy.TryOpenInventory();
        UpdateCursorVisibility(true);
    }
    public void OnBag_btn_exit()
    {
        inventoryMy.TryOpenInventory();
        UpdateCursorVisibility(false);
    }

    [Header("퀘스트 아이콘")]
    [SerializeField] public GameObject quest_content;
    public void OnQuest_btn()
    {
        q_instance.TryOpenQuest();
        UpdateCursorVisibility(true);
        
    }
    public void OnQuest_btn_exit()
    {
        q_instance.TryOpenQuest();
        UpdateCursorVisibility(false);
    }

    // 마우스 커서 상태 업데이트 메서드
    private void UpdateCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
