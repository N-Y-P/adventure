using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ClimbRange : MonoBehaviour
{
    //�ν��Ͻ�
    StarterAssetsInputs m_input;


    public static ClimbRange instance;
    TestPlayerMove _playermove;//��ũ��Ʈ ��������

    [Header("�� ����")]
    public float Climbray = 1f; //�� ���� ���� 
    public Vector3 ClimbRangeOffset; //�߽� ��ġ ����
    public Color ClimbRangeColor = Color.green;//���� ���� : �ʷϻ�

    [Header("�� ���̾�")]
    [SerializeField] LayerMask WallLayer;

    [Header("����� ǥ�� ����")]
    public bool showClimbGizmos = true; // �� ���� ���� �����

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //�ν��Ͻ�
        m_input = StarterAssetsInputs.instance;

        _playermove = TestPlayerMove.instance;
    }



    private void Update()
    {
        if (_playermove.Get_PlayerState() == CharacterState.Climbing)
        {
            input_time = 0;
            return;
        }

        if (IsClimbRange()) // �� ����
        {
            if (InputForward())
                _playermove.ClimbingStart();

            /*
             * 
            if(InputForward())
                _playermove.Set_PlayerState(CharacterState.Climbing);
            */
        }
        else
        {
            input_time = 0;
        }
    }

    //������ �Է°�

    [Header("������ �Է°� �ֱ�")]

    [SerializeField] float input_time_set = 0.2f;

    [Header("������ �Է°� �ֱ� - ��ġȮ��")]

    [SerializeField] float input_time;


    bool InputForward()
    {
        if (m_input.move.y > 0.5)
        {
            input_time += Time.deltaTime;
        }
        else
        {
            input_time = 0;
        }

        if (input_time > input_time_set)
            return true;
        else
            return false;
    }


    void OnDrawGizmos()//����� ���̰��ϱ�
    {
        // �÷��̾��� ������ ���ϴ� ����� �������� �����Ͽ� ���� ��ġ ���
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, ClimbRangeOffset);

        // �� ���� ���� �����
        if (showClimbGizmos)
        {
            Gizmos.color = ClimbRangeColor;
            Gizmos.DrawRay(offsetPosition, transform.forward * Climbray);
        }
    }

    public bool IsClimbRange()
    {
        RaycastHit hit;
        // Raycast ������ ���� (ĳ������ ��ġ)
        Vector3 start = PublicRangePosition.CalculateOffsetPosition(transform, ClimbRangeOffset);
        // Raycast ���� ���� (ĳ������ ����)
        Vector3 direction = transform.forward;

        // Scene �信�� Raycast �ð�ȭ
        Debug.DrawRay(start, direction * Climbray, ClimbRangeColor);

        // Raycast ����
        if (Physics.Raycast(start, direction, out hit, Climbray))
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
}
