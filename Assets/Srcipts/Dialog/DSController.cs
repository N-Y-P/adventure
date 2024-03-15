using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class DSController : MonoBehaviour
{
    //****범위 안에 npc가 감지되면 [F (NPC name)] UI 나오게 하는 스크립트****

    private int currentNpcId = -1;
    private NPCInputscript _npcInput; // New Input System 스크립트

    [SerializeField]
    NPCRange _npcRange; // NPC 감지를 담당하는 NPCRange 스크립트
    public GameObject FkeyNamePrefab; // NPC 이름을 보여주는 프리팹
    public Transform UIContainer; // [F키]가 나오게 할 ui 위치

    void Start()
    {
        _npcInput = GetComponent<NPCInputscript>();
    }

    void Update()
    {
        UpdateNPCUI();
    }

    private void UpdateNPCUI()
    {
        // 기존 UI 요소 삭제
        foreach (Transform child in UIContainer)
        {
            Destroy(child.gameObject);
        }

        // 새로운 UI 요소 생성
        var npcs = _npcRange.FindNPCRange();
        if (npcs.Count > 0)
        {
            foreach (var npc in npcs)
            {
                NPC_ID _npcid = npc.GetComponent<NPC_ID>();
                if (_npcid != null)
                {
                    var npcUI = Instantiate(FkeyNamePrefab, UIContainer);
                    var npcText = npcUI.GetComponentInChildren<TMP_Text>();

                    if (npcText != null)
                    {
                        // NPC 이름을 설정하고, [F] 접두사 추가
                        npcText.text = $"<color=yellow>[F]</color> {_npcid.Name}";
                        currentNpcId = _npcid.ID;
                    }
                }
            }
        }
    }

    public int GetCurrentNpcId()
    {
        return currentNpcId;
    }

}
