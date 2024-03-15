using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class TestPlayerMove : MonoBehaviour
{
    public static TestPlayerMove instance;//�ٸ� ������ ������ �� �ְ�
    [SerializeField] bool canMove = true;//ĳ���� �̵����� ���� ���� ����

    [SerializeField] StarterAssetsInputs _input;
    public GameObject _mainCamera;
    [SerializeField] CharacterController _controller;
    
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] TestCamera _testCamera;
    [SerializeField] GravityManager _gravityManager;

    [Header("Range���� ��ũ��Ʈ")]
    [SerializeField] ItemRange _itemrange;
    [SerializeField] FlyRange _flyrange;
    [SerializeField] ClimbRange _climbrange;
    [SerializeField] ClimbTopRange _climbtoprange;
    public float RotationSmoothTime = 0.12f;
    public Vector2 Rotation_dir;
    // �ִϸ��̼�
    [SerializeField] Animator _animator;
    public float jumpHeight = 8.0f; // ���� ����
    
    [SerializeField] private float rotationSpeed = 700.0f; // ȸ�� �ӵ�

    //ĳ���� ����
    [SerializeField] private CharacterState currentState = CharacterState.Normal;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ������ �� �⺻ �ӵ��� ����
        currentSpeed = 0f;

        //ĳ���� �ʱ� ȸ���� ����
        targetRotation = Quaternion.LookRotation(Vector3.zero, Vector3.up);
    }
    private void Update()
    {
        ParameterReset();

        if (!canMove)
            return; // canMove�� false�̸� �Ʒ� ���� �������� ����

        StateSwitch();
        GroundChecker();
    }
    private void StateSwitch()
    {
        switch (currentState)
        {
            case CharacterState.Unactive:
                break;

            case CharacterState.Normal:
                Move();
                break;
            case CharacterState.Gliding:
                GlidingMove();
                // Ȱ�� ���¿����� ����
                break;
            case CharacterState.Climbing:
                ClimbingMove();
                // ��� ���¿����� ����
                break;
        }
    }
    public void EnableMovement()//CPosition���� ����ϴ� ���������� �޼ҵ�
    {
        Set_PlayerState(CharacterState.Normal);
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
    Quaternion targetRotation;
    private void Move()
    {
        Vector3 cameraForward = _mainCamera.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward.Normalize();

        Vector3 dir = new Vector3(Rotation_dir.x, 0, Rotation_dir.y).normalized;
        Vector3 moveDirection = cameraForward * dir.z + _mainCamera.transform.right * dir.x;


        if (_input.move != Vector2.zero)
        {
            Rotation_dir = _input.move;
            targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        }
        //���� �츮�� ��ǥ�� �ϴ� �ӵ�
        float target_speed = TargetSpeed();

        currentSpeed = Mathf.Lerp(currentSpeed, target_speed, SpeedChangeRate);
        _animator.SetFloat("MoveSpeed", currentSpeed);

        _controller.Move((transform.forward * currentSpeed + _gravityManager.Gravity()) * Time.deltaTime);

        // �ε巴�� ��ǥ �������� ȸ��
        _controller.transform.rotation = Quaternion.RotateTowards(
            _controller.transform.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void GlidingMove()
    {  
        Vector3 cameraForward = _mainCamera.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward.Normalize();

        Vector3 dir = new Vector3(_input.move.x, 0, _input.move.y).normalized;
        Vector3 moveDirection = cameraForward * dir.z + _mainCamera.transform.right * dir.x;
        
        //���� �츮�� ��ǥ�� �ϴ� �ӵ�
        float target_speed = TargetSpeed();

        currentSpeed = Mathf.Lerp(currentSpeed, target_speed, SpeedChangeRate);
        _animator.SetFloat("MoveSpeed", currentSpeed);

        // Ȱ�� �� �ӵ� ����
        Vector3 GlidingGravity = _gravityManager.Gravity(); //�߶� �ӵ� ����
        _controller.Move((moveDirection * currentSpeed + GlidingGravity) * Time.deltaTime);
        float reducedRotationSpeed = rotationSpeed * 0.08f; // ȸ�� �ӵ� ����

        bool isMoving = moveDirection.magnitude > 0.1f;
        if (isMoving) // ���� �̻��� �������� ���� ���� ȸ��
        {
            //�ι�°����
            // ĳ���Ͱ� �ٶ� ��ǥ ���� ����
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            // �ε巴�� ��ǥ �������� ȸ��
            _controller.transform.rotation = Quaternion.RotateTowards(
                _controller.transform.rotation,targetRotation,
                reducedRotationSpeed * Time.deltaTime);

        }

    }
    [Header("�ö󰡱�(��)")]
    [SerializeField] Vector3 up_dir;
    [SerializeField] private float climbSpeed = 3.0f; // ��� �ӵ�
    
    public void ClimbingStart()
    {
        Set_PlayerState(CharacterState.Climbing);

        _animator.SetTrigger("IsClimbing"); 
    }

    private void ClimbingMove()
    {
        // ������� ���� (Y��) �� ���� (X��) �Է� �ޱ�
        float verticalInput = _input.move.y;
        float horizontalInput = _input.move.x;
        
        Vector3 hor_dir = horizontalInput * _controller.transform.right;

        // ���� �� ���� �̵� ���� ���
        //Vector3 climbMovement = new Vector3(0, verticalInput, horizontalInput).normalized * currentSpeed * Time.deltaTime;

        Vector3 climbMovement = new Vector3(hor_dir.x, verticalInput, hor_dir.z).normalized * currentSpeed * Time.deltaTime;

        // �Է��� ������ ���� �ӵ��� ������ 0���� ����
        if (verticalInput == 0 && horizontalInput == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 5f);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, climbSpeed, Time.deltaTime * 5f);
        }
        // �̵� ó��
        _controller.Move(climbMovement);

        // ��� �� �ִϸ��̼� ���
        _animator.SetFloat("MoveSpeed", currentSpeed);

       // �� ���� üũ
        if (_climbtoprange.IsClimbTop() && _input.move.y > 0.5f)//���� ���� ���̻� �������� �ʴ´ٸ�(�� ������ ������)
        {
            currentState = CharacterState.Unactive;
            Vector3 moveVector = transform.forward * up_dir.z + Vector3.up * up_dir.y;

            // CharacterController�� ����Ͽ� ĳ���� �̵�
            _controller.Move(moveVector);
            _animator.SetTrigger("ClimbingEnd");

            
        }
        else if(_climbtoprange.IsClimbTop())
        {
            currentState = CharacterState.Normal;
        }


    }

    [Header("����")]
    public bool now_jumping;   //���� �߶��� �ƴ� ������������ Ȯ��
    [SerializeField] public float jump_pow = 30f;
    [SerializeField] float wait_jumptime;
    public GameObject wings;
    public bool _isJumping = false; // ������ GravityManager���� ��Ű�� Ʈ����
    public void Normal_JumpInput()//�Ϲݻ��� - ����
    {
        Debug.Log("�Ϲ����� _ TPM");

        now_jumping = true;

        StopFalling();

        _isJumping = true;

        wait_jumptime = 0.15f;

        _animator.SetTrigger("IsJumping"); // Ʈ���� ����
        
    }
    public void Gliding_JumpInput()// ���� �� Ȱ������ ��
    {
        _animator.SetBool("falling", false);
        wings.SetActive(true);
        _animator.SetTrigger("IsFlying"); // Ʈ���� ����

        /*
        if (_flyrange.IsGroundInFlyRange())//�ߺ��Դϴ�!
        {
            wings.SetActive(true);
            _animator.SetTrigger("IsFlying"); // Ʈ���� ����
        }
        */
        StopFalling();
    }
    public void Gliding_JumpInput2()// ���߿��� Ȱ������ ����(�߶����·� �����)
    {
        // wings ���� ������Ʈ ��Ȱ��ȭ
        wings.SetActive(false);
        _gravityManager.GlidingOff();
        Start_Falling();

    }
    public bool isFalling = false; // �߶� ������ ����
    public void StartFalling()
    {
        isFalling = true; // �߶� ���� �� ȣ��
        
    }

    public void StopFalling()
    {
        isFalling = false; // ���� ����� �� ȣ��
        
    }
    public void Start_Falling()//�߶����� ����
    {
        StartFalling();
        _animator.SetTrigger("falling");
        // ���¸� �Ϲ� ���·� ����
        Set_PlayerState(CharacterState.Normal);
    }
    public void Climbing_JumpInput()
    {
        if (_climbrange.IsClimbRange())
        {
            _animator.SetTrigger("IsClimbing"); // Ʈ���� ����
        }
    }

    [Header("���ǵ�")]
    public float sprintSpeed = 8.0f;//��üӵ�
    public float moveSpeed = 5.0f;//���� �ӵ�
    [SerializeField] float SpeedChangeRate = 1f;
    [SerializeField] float accel_rate = 0.1f;//0~1 �� ���� ����
    [SerializeField] float deccel_rate = 0.8f;//��������

    [Header("���ǵ� - Ȯ�ο�")]
    [SerializeField] float currentSpeed; //���� �ӵ�//�ۿ��� �� ��ȭ��Ű�� ����
    private bool _Startdash = false;
    float TargetSpeed()
    {
        //���� ���� ������ �Է°��� ���ٸ�
        if (_input.move == Vector2.zero)
        {
            SpeedChangeRate = deccel_rate;
            _Startdash = false;
            return 0f;
        }
        SpeedChangeRate = accel_rate;

        // ������Ʈ Ű�� ���� �ִ� ���
        if (_input.sprint)
        {
            // �޸��� ���� �ִϸ��̼� Ʈ����
            if (!_Startdash)
            {
                _animator.SetTrigger("StartDash");
                _Startdash = true;
                currentSpeed = 20f;
            }
            return sprintSpeed;
        }
        else
        {
            _Startdash = false; // �޸��� ���� ����
            return moveSpeed;
        }
    }

    #region �߷�

    [Header("�߶�")]
    [SerializeField] bool now_falling;

    void GroundChecker()
    {
        //���� ���������� �����ִ� �ִϸ��̼� false
        if(_controller.isGrounded)
        {
            now_falling = false;
            now_jumping = false;

            if (currentState == CharacterState.Gliding)//���� Ȱ�����ε� ���� ������
            {
                wings.SetActive(false);
                Set_PlayerState(CharacterState.Normal);//Ȱ������, �Ϲݻ��°� ��
            }
            _animator.SetBool("IsFlying", false);
        }
        else if(!now_jumping && !_controller.isGrounded && currentState == CharacterState.Normal)
        {
            if (now_falling)
                return;

            now_falling = true;
            //���� ����x(��, ����)
            _animator.SetBool("falling", true);
            StartFalling();
        }
        
        if (wait_jumptime > 0)
        {
            wait_jumptime -= Time.deltaTime;
            _animator.SetBool("IsLanding", false);
            return;
        }

        _animator.SetBool("IsLanding", _controller.isGrounded);
        
    }

    #endregion

    #region ������ ����

    void ParameterReset()
    {
        if(Get_PlayerState() == CharacterState.Unactive)
        {
            currentSpeed = 0f;
        }
    }

    #endregion

    #region ȣ���

    //�ܺο��� ���� ���¸� Ȯ���ϱ����� ��ġ
    //�̷��� �ؾ� �����Ŵ������� private�� switch ���� ������ �� ����
    public CharacterState Get_PlayerState()
    {
        return currentState;
    }
    
    //�ܺο��� ���� ���¸� ��ȭ��Ű������ ��ġ
    public void Set_PlayerState(CharacterState _state)
    {
        currentState = _state;
    }

    //�ܺο��� �ӵ� ����
    public void Set_CurrentSpeedRate(float _rate)
    {
        currentSpeed *= _rate;
    }

    public CharacterController Get_m_CC()
    {
        return _controller;
    }

    #endregion


}
