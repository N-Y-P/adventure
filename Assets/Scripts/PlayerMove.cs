using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    private bool isGround = true;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Moving();
        Jump();
        CameraRotation();
        CharacterRotation();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;//���콺 ����� �ٵ� ȭ�� �ѹ� Ŭ���ؾ���
    }

    private void Moving()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    void Jump()
    {
        // �����̽��ٸ� ������(�Ǵ� ������ ������), �׸��� ĳ���Ͱ� ���� �ִٸ�
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // body�� ���� ���Ѵ�(AddForce)
            // AddForce(����, ���� ��� ���� ���ΰ�)
            myRigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

            // ������ ���������Ƿ� isGround�� false�� �ٲ�
            isGround = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� �±װ� "Ground"���
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void CameraRotation()//ī�޶� ȸ��
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation() //ĳ���� ȸ��
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

}

