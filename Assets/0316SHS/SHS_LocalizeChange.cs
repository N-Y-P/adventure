using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

public class SHS_LocalizeChange : MonoBehaviour
{
    [SerializeField] StringTable m_st;

    [SerializeField] LocalizeStringEvent m_lse;

    // Start is called before the first frame update
    void Start()
    {
        m_lse = GetComponent<LocalizeStringEvent>();

    }

    [SerializeField] SHS_NPCINFO set_info;

    // Update is called once per frame
    bool nyom;
    public void Btn_qkRnjqjflrh()
    {
        m_lse.StringReference.TableEntryReference = set_info.Get_MyID();


        if (false)//문자열 감지가 true)
            {
            set_info.Next_Sequence();
            //대사 종료 이벤트 발동
        }
        else
            set_info.Next_Dialogue();
    }
}
