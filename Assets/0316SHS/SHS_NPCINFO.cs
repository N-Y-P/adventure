using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHS_NPCINFO : MonoBehaviour
{

    [SerializeField] string npc_id_set = "5020101";

    public int npc_id;
    public int npc_sequence;
    public int npc_dialogue;

    // Start is called before the first frame update
    void Start()
    {
        //npc_id_set을 나누는 작업이 필요 -> 필요에 의해서 502/01/01 이런식으로 key값 형식이 바뀔 수 있다. 

        npc_id = int.Parse( npc_id_set.Substring(0, 3) );
        npc_sequence = int.Parse(npc_id_set.Substring(3, 2)); ;
        npc_dialogue = int.Parse(npc_id_set.Substring(5, 2)); ;
    }

    public string Get_MyID()
    {
        return npc_id_set;
    }

    public void Next_Dialogue()
    {
        npc_dialogue++;

        npc_id_set = (npc_id * 10000 + npc_sequence * 100 + npc_dialogue).ToString();
    }

    public void Next_Sequence()
    {
        npc_dialogue = 1;
        npc_sequence++;

        npc_id_set = (npc_id * 10000 + npc_sequence * 100 + npc_dialogue).ToString();
    }

    // Update is called once per frame
    void Message()
    {
        //5020101 -> 5020102-> 5020103 -> null => 닫아버리는 과정의 판단을 어떻게 할것인가? if(특수한 패턴 감지)
        // sequence++ => 02
        // dialogue = 1;

        // => 5020201
    }
}
