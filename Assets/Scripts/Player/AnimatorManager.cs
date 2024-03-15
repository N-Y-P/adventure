using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField]  TestPlayerMove _tpm;
    [SerializeField] float DownRate = 0.6f;

    //land 애니메이터 호출
    void Ani_LandSpeedDown()
    {
        _tpm.Set_CurrentSpeedRate(DownRate);
    }

}
