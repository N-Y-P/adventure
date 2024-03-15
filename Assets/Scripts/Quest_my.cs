using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Quest_my : MonoBehaviour
{
    private QuestInputScript _questinput;
    public static Quest_my instance;//������ ���� �����ϰ�
    public static bool questActivated = false; //����Ʈâ Ȱ��ȭ ����

    [SerializeField]
    private GameObject go_QuestBase; // Quest_Base �̹���
    //[SerializeField]
    //private GameObject go_QuestSlotsParent;  // Slot���� �θ��� Grid Setting 
    [Header("������ �ʰ� �� UI��")]
    [SerializeField]//���߿� ���� �ʿ�-�ش� ui���� �� �Ⱥ��̰Բ� ������ ��!!!! ������ �ӽ���****
    private GameObject bag_icon; //���� ������
    [SerializeField]
    private GameObject quest_icon; //����Ʈ ������
    [Space(10)]
    [SerializeField]
    private TestPlayerMove _testplayermove;

    private Slot[] slots;  // ���Ե� �迭
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
    //����Ʈ ���� �߰��ϴ� �� ���� ����
}
