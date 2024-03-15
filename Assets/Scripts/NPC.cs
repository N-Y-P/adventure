using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

[CreateAssetMenu(fileName = "New NPC", menuName = "New NPC/npc")]
public class NPC : ScriptableObject
{
    /*
    public enum NPCType  // NPC ����
    {
        white,
        purple,
    }
    */
    public string npcName; // �̸�
    //public NPCType npcType; // ����
    //public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���)
    public GameObject npcPrefab;  // �������� ������ (������ ������ ���������� ��)
    [TextArea] //���� �� ����
    public string npcDesc; //������ ���� 
}
