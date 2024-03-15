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


    public GameObject dialoguePrefab; // 대화창 프리팹, Unity 에디터에서 할당
    public Transform dialogueParent; // 대화창이 생성될 부모 객체, Unity 에디터에서 할당
    private GameObject currentDialogueInstance; // 현재 활성화된 대화창 인스턴스
    void Start()
    {
        // NPCInputscript 컴포넌트를 찾아 할당합니다.
        _npcInput = GetComponent<NPCInputscript>();
    }

    void Update()
    {
        // F키를 눌렀다면 대화 시작
        if (_npcInput.NPCLook)
        {
            currentDialogueInstance = Instantiate(dialoguePrefab, dialogueParent);
            // 대화창 활성화
            currentDialogueInstance.SetActive(true);
            //int npcId = dsController.GetCurrentNpcId();
            //if (npcId != -1) // 유효한 NPC ID가 감지된 경우
            //{
            //    StartDialogue(npcId);
            //    _npcInput.NPCLook = false; // 대화 시작 후, NPCLook 변수를 초기화합니다.
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
                // 수정된 GetDialogueByNpcId 메서드를 호출하며, npcId와 nextId를 인자로 전달합니다.
                DialogueData dialogueData = dialogueManager.GetDialogueByNpcId(npcIdComponent.ID, npcIdComponent.NextID);
                if (dialogueData != null)
                {
                    dialogueUIManager.ShowDialogue(npcIdComponent.Name, dialogueData.Dialog);
                    // 대화창에 NPC 이름과 대화 내용을 표시합니다.
                    // 필요에 따라 npcIdComponent.NextID를 업데이트합니다.
                }
            }
        }
    }

    // 현재 감지된 NPC의 GameObject를 찾는 메서드입니다.
    GameObject FindNpcGameObjectById(int npcId)
    {
        // NPCRange를 통해 현재 범위 내의 모든 NPC를 가져옵니다.
        var npcs = _npcRange.FindNPCRange();
        foreach (var npc in npcs)
        {
            NPC_ID npcIdComponent = npc.GetComponent<NPC_ID>();
            if (npcIdComponent != null && npcIdComponent.ID == npcId)
            {
                return npc; // 일치하는 NPC의 GameObject를 반환합니다.
            }
        }
        return null; // 일치하는 NPC가 없을 경우 null을 반환합니다.
    }
}
