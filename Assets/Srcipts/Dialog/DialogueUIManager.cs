using TMPro; // TextMeshPro�� ����ϱ� ���� �ʿ�
using UnityEngine;

public class DialogueUIManager : MonoBehaviour
{
    public GameObject dialoguePrefab; // ��ȭâ ������, Unity �����Ϳ��� �Ҵ�
    public Transform dialogueParent; // ��ȭâ�� ������ �θ� ��ü, Unity �����Ϳ��� �Ҵ�

    private GameObject currentDialogueInstance; // ���� Ȱ��ȭ�� ��ȭâ �ν��Ͻ�
    private TMP_Text nameText; // NPC�� �̸��� ǥ���� TextMeshPro ������Ʈ
    private TMP_Text dialogueText; // ��ȭ ������ ǥ���� TextMeshPro ������Ʈ

    // ��ȭâ�� ǥ���ϴ� �޼���, npcName�� dialogue�� ǥ���� NPC �̸��� ��ȭ �����Դϴ�.
    public void ShowDialogue(string npcName, string dialogue)
    {

        // ������ ������ ��ȭâ �ν��Ͻ��� �ִٸ� ����
        if (currentDialogueInstance != null)
        {
            Destroy(currentDialogueInstance);
        }

        // ��ȭâ �������� �������� �����ϰ�, dialogueParent�� �θ�� ����
        currentDialogueInstance = Instantiate(dialoguePrefab, dialogueParent);

        // ������ ��ȭâ���� �̸��� ������ ǥ���� TextMeshPro ������Ʈ ã��
        nameText = currentDialogueInstance.transform.Find("�̸�").GetComponent<TMP_Text>();
        dialogueText = currentDialogueInstance.transform.Find("����").GetComponent<TMP_Text>();

        // NPC �̸��� ��ȭ ���� ����
        if (nameText != null) nameText.text = npcName;
        if (dialogueText != null) dialogueText.text = dialogue;

        // ��ȭâ Ȱ��ȭ
        currentDialogueInstance.SetActive(true);
    }

    // ��ȭâ�� ����� �޼���
    public void HideDialogue()
    {
        if (currentDialogueInstance != null)
        {
            currentDialogueInstance.SetActive(false);
        }
    }
}
