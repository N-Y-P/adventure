using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCRange : MonoBehaviour
{
    public static NPCRange instance;//�ٸ���ũ��Ʈ���� ������ �� �ְ�

    [Header("npc ���� ���� ����")]
    public Vector3 NpcSize = new Vector3(1, 2, 2); // �����۰� ���� ���� (���� : 1.5, 1.2, 1.8)
    public Vector3 NpcRangeOffset; //  �߽� ��ġ ���� (���� : 0, 0.6, 0.85)
    public Color NpcRangeColor = Color.red; //  �������� : ������

    [Header("npc ���̾�")]
    [SerializeField] LayerMask NpcLayer;

    [Header("npc ǥ�� ����")]
    public bool showNpcGizmos = true; // ���� ���� �����

    private void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()//����� ���̰��ϱ�
    {
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, NpcRangeOffset);
        // �÷��̾��� ������ ���ϴ� ����� �������� �����Ͽ� ���� ��ġ ���

        // npc ���� �׸���
        // npc ���� ���� �����
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
                NPC.Add(hitCollider.gameObject); // ������ ��ü �߰�
            }
        }
        return NPC; // ��ü ����Ʈ ��ȯ
    }
}
