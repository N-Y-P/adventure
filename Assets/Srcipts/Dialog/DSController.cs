using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class DSController : MonoBehaviour
{
    //****���� �ȿ� npc�� �����Ǹ� [F (NPC name)] UI ������ �ϴ� ��ũ��Ʈ****

    private int currentNpcId = -1;
    private NPCInputscript _npcInput; // New Input System ��ũ��Ʈ

    [SerializeField]
    NPCRange _npcRange; // NPC ������ ����ϴ� NPCRange ��ũ��Ʈ
    public GameObject FkeyNamePrefab; // NPC �̸��� �����ִ� ������
    public Transform UIContainer; // [FŰ]�� ������ �� ui ��ġ

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
        // ���� UI ��� ����
        foreach (Transform child in UIContainer)
        {
            Destroy(child.gameObject);
        }

        // ���ο� UI ��� ����
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
                        // NPC �̸��� �����ϰ�, [F] ���λ� �߰�
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
