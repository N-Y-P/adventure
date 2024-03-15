using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestInputScript : MonoBehaviour
{
    StarterAssetsInputs s_instance;//��������

    [Header("����Ʈ ����")]
    [SerializeField] private Quest_my questmy; // �ν����Ϳ��� ����

    [Header("����Ʈ input")]
    public bool quest;//I������ �κ��丮 ����

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
