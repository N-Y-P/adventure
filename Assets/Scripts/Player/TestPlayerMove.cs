using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class TestPlayerMove : MonoBehaviour
{
    public static TestPlayerMove instance;//다른 곳에서 참조할 수 있게
    [SerializeField] bool canMove = true;//캐릭터 이동가능 여부 제어 변수

    [SerializeField] StarterAssetsInputs _input;
    public GameObject _mainCamera;
    [SerializeField] CharacterController _controller;
    
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] TestCamera _testCamera;
    [SerializeField] GravityManager _gravityManager;

    [Header("Range관련 스크립트")]
    [SerializeField] ItemRange _itemrange;
    [SerializeField] FlyRange _flyrange;
    [SerializeField] ClimbRange _climbrange;
    [SerializeField] ClimbTopRange _climbtoprange;
    public float RotationSmoothTime = 0.12f;
    public Vector2 Rotation_dir;
    // 애니메이션
    [SerializeField] Animator _animator;
    public float jumpHeight = 8.0f; // 점프 높이
    
    [SerializeField] private float rotationSpeed = 700.0f; // 회전 속도

    //캐릭터 상태
    [SerializeField] private CharacterState currentState = CharacterState.Normal;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // 시작할 때 기본 속도를 설정
        currentSpeed = 0f;

        //캐릭터 초기 회전값 전방
        targetRotation = Quaternion.LookRotation(Vector3.zero, Vector3.up);
    }
    private void Update()
    {
        ParameterReset();

        if (!canMove)
            return; // canMove가 false이면 아래 로직 실행하지 않음

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
                // 활강 상태에서의 로직
                break;
            case CharacterState.Climbing:
                ClimbingMove();
                // 등반 상태에서의 로직
                break;
        }
    }
    public void EnableMovement()//CPosition에서 사용하는 움직임제어 메소드
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
        //현재 우리가 목표로 하는 속도
        float target_speed = TargetSpeed();

        currentSpeed = Mathf.Lerp(currentSpeed, target_speed, SpeedChangeRate);
        _animator.SetFloat("MoveSpeed", currentSpeed);

        _controller.Move((transform.forward * currentSpeed + _gravityManager.Gravity()) * Time.deltaTime);

        // 부드럽게 목표 방향으로 회전
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
        
        //현재 우리가 목표로 하는 속도
        float target_speed = TargetSpeed();

        currentSpeed = Mathf.Lerp(currentSpeed, target_speed, SpeedChangeRate);
        _animator.SetFloat("MoveSpeed", currentSpeed);

        // 활강 중 속도 감소
        Vector3 GlidingGravity = _gravityManager.Gravity(); //추락 속도 감소
        _controller.Move((moveDirection * currentSpeed + GlidingGravity) * Time.deltaTime);
        float reducedRotationSpeed = rotationSpeed * 0.08f; // 회전 속도 감소

        bool isMoving = moveDirection.magnitude > 0.1f;
        if (isMoving) // 일정 이상의 움직임이 있을 때만 회전
        {
            //두번째버젼
            // 캐릭터가 바라볼 목표 방향 설정
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            // 부드럽게 목표 방향으로 회전
            _controller.transform.rotation = Quaternion.RotateTowards(
                _controller.transform.rotation,targetRotation,
                reducedRotationSpeed * Time.deltaTime);

        }

    }
    [Header("올라가기(끝)")]
    [SerializeField] Vector3 up_dir;
    [SerializeField] private float climbSpeed = 3.0f; // 등반 속도
    
    public void ClimbingStart()
    {
        Set_PlayerState(CharacterState.Climbing);

        _animator.SetTrigger("IsClimbing"); 
    }

    private void ClimbingMove()
    {
        // 사용자의 수직 (Y축) 및 수평 (X축) 입력 받기
        float verticalInput = _input.move.y;
        float horizontalInput = _input.move.x;
        
        Vector3 hor_dir = horizontalInput * _controller.transform.right;

        // 수직 및 수평 이동 벡터 계산
        //Vector3 climbMovement = new Vector3(0, verticalInput, horizontalInput).normalized * currentSpeed * Time.deltaTime;

        Vector3 climbMovement = new Vector3(hor_dir.x, verticalInput, hor_dir.z).normalized * currentSpeed * Time.deltaTime;

        // 입력이 없으면 현재 속도를 서서히 0으로 설정
        if (verticalInput == 0 && horizontalInput == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 5f);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, climbSpeed, Time.deltaTime * 5f);
        }
        // 이동 처리
        _controller.Move(climbMovement);

        // 등반 중 애니메이션 재생
        _animator.SetFloat("MoveSpeed", currentSpeed);

       // 벽 감지 체크
        if (_climbtoprange.IsClimbTop() && _input.move.y > 0.5f)//만약 벽이 더이상 감지되지 않는다면(벽 끝까지 도달함)
        {
            currentState = CharacterState.Unactive;
            Vector3 moveVector = transform.forward * up_dir.z + Vector3.up * up_dir.y;

            // CharacterController를 사용하여 캐릭터 이동
            _controller.Move(moveVector);
            _animator.SetTrigger("ClimbingEnd");

            
        }
        else if(_climbtoprange.IsClimbTop())
        {
            currentState = CharacterState.Normal;
        }


    }

    [Header("점프")]
    public bool now_jumping;   //현재 추락이 아닌 점프상태임을 확인
    [SerializeField] public float jump_pow = 30f;
    [SerializeField] float wait_jumptime;
    public GameObject wings;
    public bool _isJumping = false; // 점프를 GravityManager에게 시키는 트리거
    public void Normal_JumpInput()//일반상태 - 점프
    {
        Debug.Log("일반점프 _ TPM");

        now_jumping = true;

        StopFalling();

        _isJumping = true;

        wait_jumptime = 0.15f;

        _animator.SetTrigger("IsJumping"); // 트리거 설정
        
    }
    public void Gliding_JumpInput()// 실행 시 활강상태 됨
    {
        _animator.SetBool("falling", false);
        wings.SetActive(true);
        _animator.SetTrigger("IsFlying"); // 트리거 설정

        /*
        if (_flyrange.IsGroundInFlyRange())//중복입니다!
        {
            wings.SetActive(true);
            _animator.SetTrigger("IsFlying"); // 트리거 설정
        }
        */
        StopFalling();
    }
    public void Gliding_JumpInput2()// 공중에서 활강상태 해제(추락상태로 만들기)
    {
        // wings 게임 오브젝트 비활성화
        wings.SetActive(false);
        _gravityManager.GlidingOff();
        Start_Falling();

    }
    public bool isFalling = false; // 추락 중인지 여부
    public void StartFalling()
    {
        isFalling = true; // 추락 시작 시 호출
        
    }

    public void StopFalling()
    {
        isFalling = false; // 땅에 닿았을 때 호출
        
    }
    public void Start_Falling()//추락상태 관리
    {
        StartFalling();
        _animator.SetTrigger("falling");
        // 상태를 일반 상태로 변경
        Set_PlayerState(CharacterState.Normal);
    }
    public void Climbing_JumpInput()
    {
        if (_climbrange.IsClimbRange())
        {
            _animator.SetTrigger("IsClimbing"); // 트리거 설정
        }
    }

    [Header("스피드")]
    public float sprintSpeed = 8.0f;//대시속도
    public float moveSpeed = 5.0f;//보통 속도
    [SerializeField] float SpeedChangeRate = 1f;
    [SerializeField] float accel_rate = 0.1f;//0~1 의 값을 가짐
    [SerializeField] float deccel_rate = 0.8f;//마찬가지

    [Header("스피드 - 확인용")]
    [SerializeField] float currentSpeed; //현재 속도//밖에서 값 변화시키지 말기
    private bool _Startdash = false;
    float TargetSpeed()
    {
        //현재 내가 움직임 입력값이 없다면
        if (_input.move == Vector2.zero)
        {
            SpeedChangeRate = deccel_rate;
            _Startdash = false;
            return 0f;
        }
        SpeedChangeRate = accel_rate;

        // 스프린트 키가 눌려 있는 경우
        if (_input.sprint)
        {
            // 달리기 시작 애니메이션 트리거
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
            _Startdash = false; // 달리기 상태 리셋
            return moveSpeed;
        }
    }

    #region 중력

    [Header("추락")]
    [SerializeField] bool now_falling;

    void GroundChecker()
    {
        //땅에 착지했을때 날고있는 애니메이션 false
        if(_controller.isGrounded)
        {
            now_falling = false;
            now_jumping = false;

            if (currentState == CharacterState.Gliding)//현재 활강중인데 땅에 닿으면
            {
                wings.SetActive(false);
                Set_PlayerState(CharacterState.Normal);//활강해제, 일반상태가 됨
            }
            _animator.SetBool("IsFlying", false);
        }
        else if(!now_jumping && !_controller.isGrounded && currentState == CharacterState.Normal)
        {
            if (now_falling)
                return;

            now_falling = true;
            //땅에 착지x(즉, 공중)
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

    #region 변수값 정리

    void ParameterReset()
    {
        if(Get_PlayerState() == CharacterState.Unactive)
        {
            currentSpeed = 0f;
        }
    }

    #endregion

    #region 호출용

    //외부에서 현재 상태를 확인하기위한 장치
    //이렇게 해야 점프매니저에서 private인 switch 문을 가져올 수 있음
    public CharacterState Get_PlayerState()
    {
        return currentState;
    }
    
    //외부에서 현재 상태를 변화시키기위한 장치
    public void Set_PlayerState(CharacterState _state)
    {
        currentState = _state;
    }

    //외부에서 속도 조절
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
