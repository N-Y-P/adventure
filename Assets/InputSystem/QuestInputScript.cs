using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestInputScript : MonoBehaviour
{
    StarterAssetsInputs s_instance;//가져오기

    [Header("퀘스트 참조")]
    [SerializeField] private Quest_my questmy; // 인스펙터에서 설정

    [Header("퀘스트 input")]
    public bool quest;//I누르면 인벤토리 열기

    private void Start()
    {
        s_instance = StarterAssetsInputs.instance;
    }

    public void OnQuest(InputValue value)
    {
        QuestInput(value.isPressed);
    }
    
    public void QuestInput(bool newQuestState)
    {
        if (questmy != null)
        {
            questmy.TryOpenQuest();
            s_instance.UpdateCursorAndLookState(Quest_my.questActivated);
        }
    }
    

}
