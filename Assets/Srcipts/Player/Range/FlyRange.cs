using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRange : MonoBehaviour
{
    public static FlyRange instance;//�ٸ���ũ��Ʈ���� ������ �� �ְ�

    [Header("�� ����")]
    public Vector3 FlySize = new Vector3(3, 3, 3); // �� ���� �ȿ� ���� ������ Ȱ�� ���·� ���� ����
    public Vector3 FlyRangeOffset; // �߽� ��ġ ����
    public Color FlyRangeColor = Color.blue; // ���� ���� : �Ķ���

    [Header("�� ���̾�")]
    [SerializeField] LayerMask GroundLayer;

    [Header("����� ǥ�� ����")]
    public bool showGroundGizmos = true; // �� ���� ���� �����
    private void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()//����� ���̰��ϱ�
    {
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, FlyRangeOffset);
        // �÷��̾��� ������ ���ϴ� ����� �������� �����Ͽ� ���� ��ġ ���

        // Item ���� �׸���
        // ������ ���� ���� �����
        if (showGroundGizmos)
        {
            Gizmos.color = FlyRangeColor;
            Gizmos.DrawCube(offsetPosition, FlySize);
        }

    }

    public bool IsGroundInFlyRange()//�� ����
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
