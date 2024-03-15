using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Range : MonoBehaviour
{
    public static M_Range instance;

    [Header("�÷��̾� ���� ���� ����")]
    public Vector3 rangeSize = new Vector3(1, 2, 2);
    public Vector3 rangeOffset;
    public Color rangeColor = Color.white;

    [Header("�÷��̾� ���̾�")]
    public LayerMask playerLayer; // �÷��̾� ���̾ ������ LayerMask

    [Header("����� ǥ�� ����")]
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
        // CheckBox�� ����Ͽ� Ư�� ���� ���� �÷��̾ �ִ��� Ȯ��
        Collider[] hitColliders = Physics.OverlapBox(transform.position + rangeOffset, rangeSize / 2, Quaternion.identity, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                return true; // �÷��̾ �����Ǹ� true ��ȯ
            }
        }

        return false; // �÷��̾ �������� ������ false ��ȯ
    }
}

