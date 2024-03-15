using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemRange : MonoBehaviour
{
    public static ItemRange instance;//다른스크립트에서 참조할 수 있게

    [Header("아이템 감지 범위 조절")]
    public Vector3 ItemSize = new Vector3(1, 2, 2); // 아이템과 감지 범위 (실제 : 1.5, 1.2, 1.8)
    public Vector3 ItemRangeOffset; //  중심 위치 조절 (실제 : 0, 0.6, 0.85)
    public Color ItemRangeColor = Color.red; //  범위색상 : 빨간색

    [Header("아이템 레이어")]
    [SerializeField] LayerMask ItemLayer;

    [Header("기즈모 표시 설정")]
    public bool showItemNpcGizmos = true; // 아이템 감지 범위 기즈모

    private void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()//기즈모 보이게하기
    {
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform,ItemRangeOffset);
        // 플레이어의 전방을 향하는 방향과 오프셋을 결합하여 최종 위치 계산

        // Item 범위 그리기
        // 아이템 감지 범위 기즈모
        if (showItemNpcGizmos)
        {
            Gizmos.color = ItemRangeColor;
            Gizmos.DrawCube(offsetPosition,ItemSize);
        }

    }

    public List<GameObject> FindINPCRange()
    {
        List<GameObject> items = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapBox(transform.position + ItemRangeOffset, ItemSize / 2, Quaternion.identity, ItemLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Item"))
            {
                items.Add(hitCollider.gameObject); // 감지된 객체 추가
            }
        }
        return items; // 객체 리스트 반환
    }
}
