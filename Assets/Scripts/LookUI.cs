using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUI : MonoBehaviour
{

    private Camera cam;
    //플레이어에게 달린 메인카메라
    public float UIScaleX = 0.05f;//캐릭터마다 보이는 ui크기가 다를 수 있음
    public float UIScaleY = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cam != null)
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, 
                cam.transform.rotation * Vector3.up);
            transform.localScale = new Vector3(UIScaleX, UIScaleY, 0.05f);

        }
    }
}
