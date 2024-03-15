using Cinemachine;
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

    // ī�޶� ��ȯ
    public NPC_Camera _npcamera; // ī�޶� ��Ʈ�ѷ� ���� �߰�
    void Start()
    {
        _npcInput = GetComponent<NPCInputscript>();
    }

    void Update()
    {
        // FŰ�� �����ٸ� ��ȭ ����
        if (_npcInput.NPCLook)
        {
            // ���� Ȱ��ȭ�� ��ȭâ �ν��Ͻ��� ���� ��쿡�� ���ο� ��ȭâ�� ����
            if (currentDialogueInstance == null)
            {
                currentDialogueInstance = Instantiate(dialoguePrefab, dialogueParent);
                // ��ȭâ Ȱ��ȭ
                currentDialogueInstance.SetActive(true);
                // NPC�� ī�޶�� ��ȯ
                int npcId = dsController.GetCurrentNpcId();
                if (npcId != -1) // ��ȿ�� NPC ID�� ������ ���
                {
                    GameObject npcGameObject = FindNpcGameObjectById(npcId);
                    if (npcGameObject != null)
                    {
                        CinemachineVirtualCamera npcCamera = npcGameObject.GetComponentInChildren<CinemachineVirtualCamera>();
                        if (npcCamera != null)
                        {
                            _npcamera.SwitchToNpcCamera(npcCamera); // CinemachineVirtualCamera ��ü�� ����
                        }
                        else
                        {
                            Debug.LogError("npc���� �ó׸ӽ��� �����ϴ�");
                        }
                    }
                    
                }
            }
            // ��ȭ ���� ��, NPCLook ������ �ʱ�ȭ
            _npcInput.NPCLook = false;
        }
    }

    void StartDialogue(int npcID)//���� ���� ����
    {
        GameObject npcGameObject = FindNpcGameObjectById(npcID);
        if (npcGameObject != null)
        {
            CinemachineVirtualCamera npcVcam = npcGameObject.GetComponentInChildren<CinemachineVirtualCamera>();
            if (npcVcam != null)
            {
                _npcamera.SwitchToNpcCamera(npcVcam);
            }
        }
    }

    // ���� ������ NPC�� GameObject�� ã��
    GameObject FindNpcGameObjectById(int npcId)
    {
        // NPCRange�� ���� ���� ���� ���� ��� NPC�� ������
        var npcs = _npcRange.FindNPCRange();
        foreach (var npc in npcs)
        {
            NPC_ID npcIdComponent = npc.GetComponent<NPC_ID>();
            if (npcIdComponent != null && npcIdComponent.ID == npcId)
            {
                return npc; // ��ġ�ϴ� NPC�� GameObject�� ��ȯ
            }
        }
        return null; // ��ġ�ϴ� NPC�� ���� ��� null�� ��ȯ
    }
}
