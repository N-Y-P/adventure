using System.Collections.Generic;
using UnityEngine;

//****�������� �ޱ�***//

[System.Serializable]
public class DialogueData
{
    public int NPCID;
    public int DialogID;
    public int NextID;
    public string Dialog;
    public string Condition;
    public string Animation;
    public int EventID;
}

public class DialogueManager : MonoBehaviour
{
    public TextAsset dialogueCSV; // Unity �����Ϳ��� �Ҵ�
    private List<DialogueData> dialogues = new List<DialogueData>();

    void Start()
    {
        ReadCSV();
    }
    void ReadCSV()
    {
        string[] lines = dialogueCSV.text.Split('\n');
        foreach (string line in lines)
        {
            string[] fields = line.Split(',');
            if (fields.Length > 3) // ������ ��ȿ�� �˻�
            {
                DialogueData dialogueData = new DialogueData();

                // NPCID, DialogID, NextID�� �����ϰ� �Ľ�
                int.TryParse(fields[0], out dialogueData.NPCID);
                int.TryParse(fields[1], out dialogueData.DialogID);
                int.TryParse(fields[2], out dialogueData.NextID);

                dialogueData.Dialog = fields[3];

                dialogues.Add(dialogueData);
            }
        }
    }
    public DialogueData GetDialogueByNpcId(int npcId, int nextId)
    {
        // ��ȭ ������ ��Ͽ��� �ش� npcId�� nextId�� ��ġ�ϴ� ��ȭ �����͸� ã�� ��ȯ�մϴ�.
        // ���⼭�� dialogueDatas�� ��� ��ȭ �����͸� ��� �ִ� �÷����̶�� �����մϴ�.
        foreach (var dialogue in dialogues)
        {
            if (dialogue.NPCID == npcId && dialogue.NextID == nextId)
            {
                return dialogue;
            }
        }
        return null; // ��ġ�ϴ� ��ȭ �����Ͱ� ���� ��� null ��ȯ
    }
}
