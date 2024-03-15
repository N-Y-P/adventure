using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Btn_event : MonoBehaviour
{
    //�����Ϸ��� ������ �ν��Ͻ���
    Quest_my q_instance;
    Inventory_my i_instance;
    StarterAssetsInputs s_instance;
    //
    [Header("�κ��丮 ����")]
    [SerializeField] private Inventory_my inventoryMy; // �ν����Ϳ��� ����
    private void Start()
    {
        q_instance = Quest_my.instance;
        s_instance = StarterAssetsInputs.instance;
    }
    [Header("���� ������")]
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

    [Header("����Ʈ ������")]
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

    // ���콺 Ŀ�� ���� ������Ʈ �޼���
    private void UpdateCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
