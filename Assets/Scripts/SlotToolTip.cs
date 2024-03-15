using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private TMP_Text txt_ItemName;
    [SerializeField]
    private TMP_Text txt_ItemDesc;
    // [SerializeField]
    //private Text txt_ItemHowtoUsed;


    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }

}
