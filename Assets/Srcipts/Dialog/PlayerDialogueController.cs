using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerDialogueController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public DialogueUIManager dialogueUIManager;
    private NPCInputscript _npcInput;
    public DSController dsController;
    public NPCRange _npcRange;


    public GameObject dialoguePrefab; // ��ȭâ ������, Unity �����Ϳ��� �Ҵ�
    public Transform dialogueParent; // ��ȭâ�� ������ �θ� ��ü, Unity �����Ϳ��� �Ҵ�
    private GameObject currentDialogueInstance; // ���� Ȱ��ȭ�� ��ȭâ �ν��Ͻ�
    void Start()
    {
        // NPCInputscript ������Ʈ�� ã�� �Ҵ��մϴ�.
        _npcInput = GetComponent<NPCInputscript>();
    }

    void Update()
    {
        // FŰ�� �����ٸ� ��ȭ ����
        if (_npcInput.NPCLook)
        {
            currentDialogueInstance = Instantiate(dialoguePrefab, dialogueParent);
            // ��ȭâ Ȱ��ȭ
            currentDialogueInstance.SetActive(true);
            //int npcId = dsController.GetCurrentNpcId();
            //if (npcId != -1) // ��ȿ�� NPC ID�� ������ ���
            //{
            //    StartDialogue(npcId);
            //    _npcInput.NPCLook = false; // ��ȭ ���� ��, NPCLook ������ �ʱ�ȭ�մϴ�.
            //}
        }
    }

    void StartDialogue(int npcID)
    {
        GameObject npcGameObject = FindNpcGameObjectById(npcID);
        if (npcGameObject != null)
        {
            NPC_ID npcIdComponent = npcGameObject.GetComponent<NPC_ID>();
            if (npcIdComponent != null)
            {
                // ������ GetDialogueByNpcId �޼��带 ȣ���ϸ�, npcId�� nextId�� ���ڷ� �����մϴ�.
                DialogueData dialogueData = dialogueManager.GetDialogueByNpcId(npcIdComponent.ID, npcIdComponent.NextID);
                if (dialogueData != null)
                {
                    dialogueUIManager.ShowDialogue(npcIdComponent.Name, dialogueData.Dialog);
                    // ��ȭâ�� NPC �̸��� ��ȭ ������ ǥ���մϴ�.
                    // �ʿ信 ���� npcIdComponent.NextID�� ������Ʈ�մϴ�.
                }
            }
        }
    }

    // ���� ������ NPC�� GameObject�� ã�� �޼����Դϴ�.
    GameObject FindNpcGameObjectById(int npcId)
    {
        // NPCRange�� ���� ���� ���� ���� ��� NPC�� �����ɴϴ�.
        var npcs = _npcRange.FindNPCRange();
        foreach (var npc in npcs)
        {
            NPC_ID npcIdComponent = npc.GetComponent<NPC_ID>();
            if (npcIdComponent != null && npcIdComponent.ID == npcId)
            {
                return npc; // ��ġ�ϴ� NPC�� GameObject�� ��ȯ�մϴ�.
            }
        }
        return null; // ��ġ�ϴ� NPC�� ���� ��� null�� ��ȯ�մϴ�.
    }
}
