using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newscene;

public class Input_Char : MonoBehaviour
{
    InputManager im;
    Rigidbody m_rigid;

    // Start is called before the first frame update
    void Start()
    {
        im = InputManager.instance;

        m_rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
    }

    [Header("¿òÁ÷ÀÓ")]
    [SerializeField] float move_speed;
    

    void Moving()
    {
        m_rigid.velocity = im.Get_Direction() * move_speed * Time.deltaTime;
    }


}
