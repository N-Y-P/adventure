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
    // �ִϸ��̼�
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
        //���� �츮�� ��ǥ�� �ϴ� �ӵ�
        float target_speed = TargetSpeed();

        currentSpeed = Mathf.Lerp(currentSpeed, target_speed, SpeedChangeRate);
        _animator.SetFloat("MoveSpeed", currentSpeed);

        _controller.Move((transform.forward * currentSpeed + Gravity()) * Time.deltaTime);

        // �ε巴�� ��ǥ �������� ȸ��
        _controller.transform.rotation = Quaternion.RotateTowards(
            _controller.transform.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
    }
    */
}
