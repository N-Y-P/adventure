using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCRange : MonoBehaviour
{
    public static NPCRange instance;//다른스크립트에서 참조할 수 있게

    [Header("npc 감지 범위 조절")]
    public Vector3 NpcSize = new Vector3(1, 2, 2); // 아이템과 감지 범위 (실제 : 1.5, 1.2, 1.8)
    public Vector3 NpcRangeOffset; //  중심 위치 조절 (실제 : 0, 0.6, 0.85)
    public Color NpcRangeColor = Color.red; //  범위색상 : 빨간색

    [Header("npc 레이어")]
    [SerializeField] LayerMask NpcLayer;

    [Header("npc 표시 설정")]
    public bool showNpcGizmos = true; // 감지 범위 기즈모

    private void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()//기즈모 보이게하기
    {
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, NpcRangeOffset);
        // 플레이어의 전방을 향하는 방향과 오프셋을 결합하여 최종 위치 계산

        // npc 범위 그리기
        // npc 감지 범위 기즈모
        if (showNpcGizmos)
        {
            Gizmos.color = NpcRangeColor;
            Gizmos.DrawCube(offsetPosition, NpcSize);
        }

    }

    public List<GameObject> FindNPCRange()
    {
        List<GameObject> NPC = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapBox(transform.position + NpcRangeOffset, NpcSize / 2, Quaternion.identity, NpcLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NPC"))
            {
                NPC.Add(hitCollider.gameObject); // 감지된 객체 추가
            }
        }
        return NPC; // 객체 리스트 반환
    }
}
