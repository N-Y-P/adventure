using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Range : MonoBehaviour
{
    public static M_Range instance;

    [Header("플레이어 감지 범위 조절")]
    public Vector3 rangeSize = new Vector3(1, 2, 2);
    public Vector3 rangeOffset;
    public Color rangeColor = Color.white;

    [Header("플레이어 레이어")]
    public LayerMask playerLayer; // 플레이어 레이어를 지정할 LayerMask

    [Header("기즈모 표시 설정")]
    public bool showrangeGizmos = true;
    private void Awake()
    {
        instance = this;
    }

    private void OnDrawGizmos()
    {
        if(showrangeGizmos)
        {
            Gizmos.color = rangeColor;
            Gizmos.DrawCube(transform.position + rangeOffset, rangeSize);
        }
    }

    public bool FindPlayerRange()
    {
        // CheckBox를 사용하여 특정 범위 내에 플레이어가 있는지 확인
        Collider[] hitColliders = Physics.OverlapBox(transform.position + rangeOffset, rangeSize / 2, Quaternion.identity, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                return true; // 플레이어가 감지되면 true 반환
            }
        }

        return false; // 플레이어가 감지되지 않으면 false 반환
    }
}

