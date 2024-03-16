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


        if (false)//���ڿ� ������ true)
            {
            set_info.Next_Sequence();
            //��� ���� �̺�Ʈ �ߵ�
        }
        else
            set_info.Next_Dialogue();
    }
}
