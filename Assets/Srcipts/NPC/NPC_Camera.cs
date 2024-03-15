using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NPC_Camera : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera; // �÷��̾� ī�޶�
    private CinemachineVirtualCamera currentNPCCamera; // NPC�� ī�޶�

    public void SwitchToNpcCamera(CinemachineVirtualCamera npcCamera)
    {
        if (currentNPCCamera != null)
        {
            currentNPCCamera.Priority = 0; // ���� ī�޶��� �켱������ ����
        }

        npcCamera.Priority = 10; // NPC ī�޶��� �켱������ ���� Ȱ��ȭ
        currentNPCCamera = npcCamera; // ���� ��� ī�޶� ������Ʈ
    }

    public void SwitchToPlayerCamera()
    {
        if (currentNPCCamera != null)
        {
            currentNPCCamera.Priority = 0; // NPC ī�޶��� �켱������ ����
        }
        playerCamera.Priority = 10; // �÷��̾� ī�޶��� �켱������ ���� Ȱ��ȭ
    }
}
