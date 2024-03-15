using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemRange : MonoBehaviour
{
    public static ItemRange instance;//�ٸ���ũ��Ʈ���� ������ �� �ְ�

    [Header("������ ���� ���� ����")]
    public Vector3 ItemSize = new Vector3(1, 2, 2); // �����۰� ���� ���� (���� : 1.5, 1.2, 1.8)
    public Vector3 ItemRangeOffset; //  �߽� ��ġ ���� (���� : 0, 0.6, 0.85)
    public Color ItemRangeColor = Color.red; //  �������� : ������

    [Header("������ ���̾�")]
    [SerializeField] LayerMask ItemLayer;

    [Header("����� ǥ�� ����")]
    public bool showItemNpcGizmos = true; // ������ ���� ���� �����

    private void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()//����� ���̰��ϱ�
    {
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform,ItemRangeOffset);
        // �÷��̾��� ������ ���ϴ� ����� �������� �����Ͽ� ���� ��ġ ���

        // Item ���� �׸���
        // ������ ���� ���� �����
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
                items.Add(hitCollider.gameObject); // ������ ��ü �߰�
            }
        }
        return items; // ��ü ����Ʈ ��ȯ
    }
}
