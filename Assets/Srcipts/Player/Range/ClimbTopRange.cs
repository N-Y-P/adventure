using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTopRange : MonoBehaviour
{
    public static ClimbTopRange instance;
    TestPlayerMove _playermove;

    [Header("�� ��� �Ϸ�")]
    public float rayLength = 1.0f; // Raycast ����
    public Vector3 ClimbTopOffset;
    public Color ClimbTopColor = Color.red; // Raycast ����

    [Header("����� ǥ�� ����")]
    public bool showClimbTopGizmos = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _playermove = TestPlayerMove.instance;

    }
    void OnDrawGizmos()//����� ���̰��ϱ�
    {
        // �÷��̾��� ������ ���ϴ� ����� �������� �����Ͽ� ���� ��ġ ���
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, ClimbTopOffset);

        if (showClimbTopGizmos)
        {
            Gizmos.color = ClimbTopColor;
            Gizmos.DrawRay(offsetPosition, transform.forward * rayLength); // rayLength�� Ray�� ����
        }
    }
    public bool IsClimbTop()
    {
        RaycastHit hit;
        // Raycast ������ ���� (ĳ������ ��ġ)
        Vector3 start = PublicRangePosition.CalculateOffsetPosition(transform, ClimbTopOffset);
        // Raycast ���� ���� (ĳ������ ����)
        Vector3 direction = transform.forward;

        // Scene �信�� Raycast �ð�ȭ
        Debug.DrawRay(start, direction * rayLength, ClimbTopColor);

        // Raycast ����
        if (!Physics.Raycast(start, direction, out hit, rayLength))
        {

            return true;
        }
        // ���� �浹���� �ʾ����� false ��ȯ
        return false;
    }
}
