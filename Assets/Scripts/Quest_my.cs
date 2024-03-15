using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Quest_my : MonoBehaviour
{
    private QuestInputScript _questinput;
    public static Quest_my instance;//남들이 나를 참조하게
    public static bool questActivated = false; //퀘스트창 활성화 여부

    [SerializeField]
    private GameObject go_QuestBase; // Quest_Base 이미지
    //[SerializeField]
    //private GameObject go_QuestSlotsParent;  // Slot들의 부모인 Grid Setting 
    [Header("보이지 않게 할 UI들")]
    [SerializeField]//나중에 수정 필요-해당 ui빼고 다 안보이게끔 만들어야 함!!!! 지금은 임시임****
    private GameObject bag_icon; //가방 아이콘
    [SerializeField]
    private GameObject quest_icon; //퀘스트 아이콘
    [Space(10)]
    [SerializeField]
    private TestPlayerMove _testplayermove;

    private Slot[] slots;  // 슬롯들 배열
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _questinput = GetComponent<QuestInputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_questinput.quest)
        {
            TryOpenQuest();
        }
    }

    public void TryOpenQuest()
    {
        questActivated = !questActivated;
        if(questActivated)
        {
            OpenQuest();
        }
        else
        {
            CloseQuest();
        }
    }

    private void OpenQuest()
    {
        go_QuestBase.SetActive(true);
        bag_icon.SetActive(false);
        quest_icon.SetActive(false);
        _testplayermove.enabled = false;
    }
    private void CloseQuest()
    {
        go_QuestBase?.SetActive(false);
        bag_icon.SetActive(true);
        quest_icon.SetActive(true);
        _testplayermove.enabled = true;
    }
    //퀘스트 슬롯 추가하는 것 추후 구현
}
