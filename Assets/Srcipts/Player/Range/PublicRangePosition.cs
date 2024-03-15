using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PublicRangePosition
{
    public static Vector3 CalculateOffsetPosition(Transform transform, Vector3 offset)//ĳ���Ͱ� ���� �������� ����� ȸ��
    {
        return transform.position + transform.forward * offset.z +
                                   transform.right * offset.x +
                                   transform.up * offset.y;
    }
}
