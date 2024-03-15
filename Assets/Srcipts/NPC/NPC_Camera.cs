using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NPC_Camera : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera; // 플레이어 카메라
    private CinemachineVirtualCamera currentNPCCamera; // NPC의 카메라

    public void SwitchToNpcCamera(CinemachineVirtualCamera npcCamera)
    {
        if (currentNPCCamera != null)
        {
            currentNPCCamera.Priority = 0; // 이전 카메라의 우선순위를 낮춤
        }

        npcCamera.Priority = 10; // NPC 카메라의 우선순위를 높여 활성화
        currentNPCCamera = npcCamera; // 현재 대상 카메라 업데이트
    }

    public void SwitchToPlayerCamera()
    {
        if (currentNPCCamera != null)
        {
            currentNPCCamera.Priority = 0; // NPC 카메라의 우선순위를 낮춤
        }
        playerCamera.Priority = 10; // 플레이어 카메라의 우선순위를 높여 활성화
    }
}
