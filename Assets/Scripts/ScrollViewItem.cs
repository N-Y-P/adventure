using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ScrollViewItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //����

        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //�Ʒ���

        }
    }
}
