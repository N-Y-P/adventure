using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField]  TestPlayerMove _tpm;
    [SerializeField] float DownRate = 0.6f;

    //land �ִϸ����� ȣ��
    void Ani_LandSpeedDown()
    {
        _tpm.Set_CurrentSpeedRate(DownRate);
    }

}
