using System.Collections.Generic;
using UnityEngine;

//****엑셀파일 받기***//

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
    public TextAsset dialogueCSV; // Unity 에디터에서 할당
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
            if (fields.Length > 3) // 간단한 유효성 검사
            {
                DialogueData dialogueData = new DialogueData();

                // NPCID, DialogID, NextID를 안전하게 파싱
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
        // 대화 데이터 목록에서 해당 npcId와 nextId에 일치하는 대화 데이터를 찾아 반환합니다.
        // 여기서는 dialogueDatas가 모든 대화 데이터를 담고 있는 컬렉션이라고 가정합니다.
        foreach (var dialogue in dialogues)
        {
            if (dialogue.NPCID == npcId && dialogue.NextID == nextId)
            {
                return dialogue;
            }
        }
        return null; // 일치하는 대화 데이터가 없는 경우 null 반환
    }
}
