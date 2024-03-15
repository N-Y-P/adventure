using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class NormalMove : MonoBehaviour
{
    public static NormalMove instance;
    public GameObject _mainCamera;
    public Vector2 Rotation_dir;
    [SerializeField] StarterAssetsInputs _input;
    // 애니메이션
    [SerializeField] Animator _animator;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
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

        _controller.Move((transform.forward * currentSpeed + Gravity()) * Time.deltaTime);

        // 부드럽게 목표 방향으로 회전
        _controller.transform.rotation = Quaternion.RotateTowards(
            _controller.transform.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
    }
    */
}
