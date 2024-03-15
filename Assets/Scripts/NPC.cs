using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

[CreateAssetMenu(fileName = "New NPC", menuName = "New NPC/npc")]
public class NPC : ScriptableObject
{
    /*
    public enum NPCType  // NPC 유형
    {
        white,
        purple,
    }
    */
    public string npcName; // 이름
    //public NPCType npcType; // 유형
    //public Sprite itemImage; // 아이템의 이미지(인벤 토리 안에서 띄울)
    public GameObject npcPrefab;  // 아이템의 프리팹 (아이템 생성시 프리팹으로 찍어냄)
    [TextArea] //여러 줄 가능
    public string npcDesc; //아이템 설명 
}
