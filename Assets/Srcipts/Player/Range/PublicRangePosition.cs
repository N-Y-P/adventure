using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PublicRangePosition
{
    public static Vector3 CalculateOffsetPosition(Transform transform, Vector3 offset)//캐릭터가 보는 방향으로 기즈모 회전
    {
        return transform.position + transform.forward * offset.z +
                                   transform.right * offset.x +
                                   transform.up * offset.y;
    }
}
