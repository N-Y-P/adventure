using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TestRange : MonoBehaviour
{
    public static TestRange instance;//�ٸ���ũ��Ʈ���� ������ �� �ְ�
    TestPlayerMove tpm;//��ũ��Ʈ ��������

    [Header("�����۰� NPC")]
    public Vector3 INpcSize = new Vector3(3, 2, 2); // �����۰� NPC ���� ����
    public Vector3 INpcRangeOffset; //  �߽� ��ġ ����
    public Color INpcRangeColor = Color.red; //  �������� : ������

    [Header("�� ����")]
    public Vector3 FlySize = new Vector3(3, 3, 3); // �� ���� �ȿ� ���� ������ Ȱ�� ���·� ���� ����
    public Vector3 FlyRangeOffset; // �߽� ��ġ ����
    public Color FlyRangeColor = Color.blue; // ���� ���� : �Ķ���

    [Header("�� ����")]
    public Vector3 ClimbSize = new Vector3(3, 3, 3); //�� ���� ����
    public Vector3 ClimbRangeOffset; //�߽� ��ġ ����
    public Color ClimbRangeColor = Color.green;//���� ���� : �ʷϻ�

    [Header("�� ��� �Ϸ�")]
    public float rayLength = 1.0f; // Raycast ����
    public Vector3 ClimbTopOffset;
    public Color ClimbTopColor = Color.red; // Raycast ����

    [Header("����� ǥ�� ����")]
    public bool showItemNpcGizmos = true; // �����۰� NPC ���� ���� �����
    public bool showGroundGizmos = true; // �� ���� ���� �����
    public bool showClimbGizmos = true; // �� ���� ���� �����
    public bool showClimbTopGizmos = true;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        tpm = TestPlayerMove.instance;
       
    }
    private void Update()
    {
            if (IsClimbRange()) // �� ����
            {
                tpm.Set_PlayerState(CharacterState.Climbing);
            }
    }
     void OnDrawGizmos()//����� ���̰��ϱ�
    {
        // �÷��̾��� ������ ���ϴ� ����� �������� �����Ͽ� ���� ��ġ ���
        Vector3 npcRangePosition = CalculateOffsetPosition(INpcRangeOffset);
        Vector3 flyRangePosition = CalculateOffsetPosition(FlyRangeOffset);
        Vector3 climbRangePosition = CalculateOffsetPosition(ClimbRangeOffset);
        Vector3 climbTopPosition = transform.position + ClimbTopOffset;
        // Vector3 climbTopPosition = CalculateOffsetPosition(ClimbTopOffset);


        // �����۰� NPC ���� ���� �����
        if (showItemNpcGizmos)
        {
            Gizmos.color = INpcRangeColor;
            Gizmos.DrawCube(CalculateOffsetPosition(INpcRangeOffset), INpcSize);
        }

        // �� ���� ���� �����
        if (showGroundGizmos)
        {
            Gizmos.color = FlyRangeColor;
            Gizmos.DrawCube(CalculateOffsetPosition(FlyRangeOffset), FlySize);
        }

        // �� ���� ���� �����
        if (showClimbGizmos)
        {
            Gizmos.color = ClimbRangeColor;
            Gizmos.DrawCube(CalculateOffsetPosition(ClimbRangeOffset), ClimbSize);
        }
        if (showClimbTopGizmos)
        {
            Gizmos.color = ClimbTopColor;
            Gizmos.DrawRay(climbTopPosition, transform.forward * rayLength); // rayLength�� Ray�� ����
        }
    }
    [Header("�����۰� NPC ����")]
    [SerializeField] LayerMask ItemLayer;
    //[SerializeField] LayerMask NPCLayer;
    /*
    public GameObject FindINPCRange() // ������, npc ����
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + INpcRangeOffset, INpcSize / 2, Quaternion.identity, ItemLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NPC") || hitCollider.CompareTag("Item"))
            {
                return hitCollider.gameObject; // ������ ��ü ��ȯ
            }
        }
        return null; // ��ü�� ������ null ��ȯ
    }*/
    public List<GameObject> FindINPCRange()
    {
        List<GameObject> items = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapBox(CalculateOffsetPosition(INpcRangeOffset), INpcSize / 2, Quaternion.identity, ItemLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NPC") || hitCollider.CompareTag("Item"))
            {
                items.Add(hitCollider.gameObject); // ������ ��ü �߰�
            }
        }
        return items; // ��ü ����Ʈ ��ȯ
    }
    [Header("�������� �� ����")]
    [SerializeField] LayerMask GroundLayer;
    public bool IsGroundInFlyRange()//�� ����
    {
            if (Physics.CheckBox(CalculateOffsetPosition(FlyRangeOffset), FlySize / 2, Quaternion.identity, GroundLayer))
            //
            {
                return false;
            }
            else
            {
                 return true;
            }
       
    }
    [Header("����� ���� �� ����")]
    [SerializeField] LayerMask WallLayer;
    [SerializeField] Collider[] hitColliders;
    public bool IsClimbRange()//�� ����
    {
        hitColliders = Physics.OverlapBox(CalculateOffsetPosition(ClimbRangeOffset), ClimbSize / 2, Quaternion.identity, WallLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Wall")) // 'Wall' �±׸� ���� ������Ʈ�� ����
            {
                Debug.Log("�� ������");
                return true;
            }
        }
        Debug.Log("�� ����");
        return false;
        /*
        Vector3 worldPosition = transform.position + ClimbRangeOffset;
        bool isWallDetected = Physics.CheckBox(worldPosition, ClimbSize / 2, Quaternion.identity, WallLayer);

        // ����� ���� ���
        Debug.Log("�� ���� ���: " + isWallDetected);

        return isWallDetected;
        */

    }

    public bool IsClimbTop()
    {
        RaycastHit hit;
        // Raycast ������ ���� (�Ϲ������� ĳ������ ��ġ)
        Vector3 start = transform.position;
        // Raycast ���� ���� (�Ϲ������� ĳ������ ����)
        Vector3 direction = transform.forward;

        // Scene �信�� Raycast �ð�ȭ
        Debug.DrawRay(start, direction * rayLength, ClimbTopColor);

        // Raycast ����
        if (Physics.Raycast(start, direction, out hit, rayLength))
        {
            // Raycast�� ���� �浹�ߴ��� Ȯ��
            if (hit.collider.CompareTag("Wall"))
            {
                // ���� �浹������ true ��ȯ
                return true;
            }
        }

        // ���� �浹���� �ʾ����� false ��ȯ
        return false;
    }
    private Vector3 CalculateOffsetPosition(Vector3 offset)//ĳ���Ͱ� ���� �������� ����� ȸ��
    {
        return transform.position + transform.forward * offset.z +
                                   transform.right * offset.x +
                                   transform.up * offset.y;
    }
}
