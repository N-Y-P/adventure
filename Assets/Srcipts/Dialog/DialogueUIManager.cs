using TMPro; // TextMeshPro를 사용하기 위해 필요
using UnityEngine;

public class DialogueUIManager : MonoBehaviour
{
    public GameObject dialoguePrefab; // 대화창 프리팹, Unity 에디터에서 할당
    public Transform dialogueParent; // 대화창이 생성될 부모 객체, Unity 에디터에서 할당

    private GameObject currentDialogueInstance; // 현재 활성화된 대화창 인스턴스
    private TMP_Text nameText; // NPC의 이름을 표시할 TextMeshPro 컴포넌트
    private TMP_Text dialogueText; // 대화 내용을 표시할 TextMeshPro 컴포넌트

    // 대화창을 표시하는 메서드, npcName과 dialogue는 표시할 NPC 이름과 대화 내용입니다.
    public void ShowDialogue(string npcName, string dialogue)
    {

        // 이전에 생성된 대화창 인스턴스가 있다면 제거
        if (currentDialogueInstance != null)
        {
            Destroy(currentDialogueInstance);
        }

        // 대화창 프리팹을 동적으로 생성하고, dialogueParent를 부모로 설정
        currentDialogueInstance = Instantiate(dialoguePrefab, dialogueParent);

        // 생성된 대화창에서 이름과 내용을 표시할 TextMeshPro 컴포넌트 찾기
        nameText = currentDialogueInstance.transform.Find("이름").GetComponent<TMP_Text>();
        dialogueText = currentDialogueInstance.transform.Find("내용").GetComponent<TMP_Text>();

        // NPC 이름과 대화 내용 설정
        if (nameText != null) nameText.text = npcName;
        if (dialogueText != null) dialogueText.text = dialogue;

        // 대화창 활성화
        currentDialogueInstance.SetActive(true);
    }

    // 대화창을 숨기는 메서드
    public void HideDialogue()
    {
        if (currentDialogueInstance != null)
        {
            currentDialogueInstance.SetActive(false);
        }
    }
}
