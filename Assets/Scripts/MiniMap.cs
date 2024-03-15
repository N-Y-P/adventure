using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private float cameraRotation;

    [SerializeField]
    public Camera MiniCam;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()//카메라 회전
    {
        Vector3 pos;
        pos = Player.gameObject.transform.position;
        MiniCam.transform.position = new Vector3(pos.x, 25, pos.z);
        MiniCam.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
    }
}
