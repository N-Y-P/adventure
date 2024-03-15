using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class TestCamera : MonoBehaviour
{
    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    //
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;

    //
    [SerializeField] CharacterController _controller;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] StarterAssetsInputs _input;
    [SerializeField] TestPlayerMove _testplayermove;
    GameObject _mainCamera;

    private const float _threshold = 0.01f;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        CameraRotation();
    }

    [Header("ī�޶�ȸ���ӵ�")]
    [SerializeField] float mouse_cam_rospeed = 150f;

    private void CameraRotation()
    {
        // ���� input �� camera position �� �����Ǿ� ���� �ʴٸ�
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            float deltaTimeMultiplier = mouse_cam_rospeed * Time.deltaTime;

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }

        // ȸ������ ������Ű�� value�� 360���� ����
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // �ó׸ӽ��� Ÿ���� ����ٴϰ�
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
