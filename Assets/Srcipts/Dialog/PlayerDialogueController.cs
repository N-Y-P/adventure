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


    public GameObject dialoguePrefab; // 대화창 프리팹, Unity 에디터에서 할당
    public Transform dialogueParent; // 대화창이 생성될 부모 객체, Unity 에디터에서 할당
    private GameObject currentDialogueInstance; // 현재 활성화된 대화창 인스턴스

    // 카메라 전환
    public NPC_Camera _npcamera; // 카메라 컨트롤러 참조 추가
    void Start()
    {
        _npcInput = GetComponent<NPCInputscript>();
    }

    void Update()
    {
        // F키를 눌렀다면 대화 시작
        if (_npcInput.NPCLook)
        {
            // 현재 활성화된 대화창 인스턴스가 없는 경우에만 새로운 대화창을 생성
            if (currentDialogueInstance == null)
            {
                currentDialogueInstance = Instantiate(dialoguePrefab, dialogueParent);
                // 대화창 활성화
                currentDialogueInstance.SetActive(true);
                // NPC의 카메라로 전환
                int npcId = dsController.GetCurrentNpcId();
                if (npcId != -1) // 유효한 NPC ID가 감지된 경우
                {
                    GameObject npcGameObject = FindNpcGameObjectById(npcId);
                    if (npcGameObject != null)
                    {
                        CinemachineVirtualCamera npcCamera = npcGameObject.GetComponentInChildren<CinemachineVirtualCamera>();
                        if (npcCamera != null)
                        {
                            _npcamera.SwitchToNpcCamera(npcCamera); // CinemachineVirtualCamera 객체를 전달
                        }
                        else
                        {
                            Debug.LogError("npc에게 시네머신이 없습니다");
                        }
                    }
                    
                }
            }
            // 대화 시작 후, NPCLook 변수를 초기화
            _npcInput.NPCLook = false;
        }
    }

    void StartDialogue(int npcID)//현재 쓰지 않음
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

    // 현재 감지된 NPC의 GameObject를 찾기
    GameObject FindNpcGameObjectById(int npcId)
    {
        // NPCRange를 통해 현재 범위 내의 모든 NPC를 가져옴
        var npcs = _npcRange.FindNPCRange();
        foreach (var npc in npcs)
        {
            NPC_ID npcIdComponent = npc.GetComponent<NPC_ID>();
            if (npcIdComponent != null && npcIdComponent.ID == npcId)
            {
                return npc; // 일치하는 NPC의 GameObject를 반환
            }
        }
        return null; // 일치하는 NPC가 없을 경우 null을 반환
    }
}
