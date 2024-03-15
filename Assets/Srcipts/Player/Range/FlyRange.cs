using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRange : MonoBehaviour
{
    public static FlyRange instance;//다른스크립트에서 참조할 수 있게

    [Header("땅 감지")]
    public Vector3 FlySize = new Vector3(3, 3, 3); // 이 범위 안에 땅이 없으면 활강 상태로 변경 가능
    public Vector3 FlyRangeOffset; // 중심 위치 조절
    public Color FlyRangeColor = Color.blue; // 범위 색상 : 파란색

    [Header("땅 레이어")]
    [SerializeField] LayerMask GroundLayer;

    [Header("기즈모 표시 설정")]
    public bool showGroundGizmos = true; // 땅 감지 범위 기즈모
    private void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()//기즈모 보이게하기
    {
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, FlyRangeOffset);
        // 플레이어의 전방을 향하는 방향과 오프셋을 결합하여 최종 위치 계산

        // Item 범위 그리기
        // 아이템 감지 범위 기즈모
        if (showGroundGizmos)
        {
            Gizmos.color = FlyRangeColor;
            Gizmos.DrawCube(offsetPosition, FlySize);
        }

    }

    public bool IsGroundInFlyRange()//땅 감지
    {
        if (Physics.CheckBox(transform.position + FlyRangeOffset, FlySize / 2, Quaternion.identity, GroundLayer))
        //
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}
