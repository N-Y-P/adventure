using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    public enum ItemType  // ������ ����
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }

    public string itemName; // �������� �̸�
    public string itemNumber; // ������ �����ѹ�
    public ItemType itemType; // ������ ����
    public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���)
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)
    [TextArea] //���� �� ����
    public string itemDesc; //������ ���� 

    //public string weaponType;  // ���� ����
}
