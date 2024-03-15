using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TestRange : MonoBehaviour
{
    public static TestRange instance;//다른스크립트에서 참조할 수 있게
    TestPlayerMove tpm;//스크립트 가져오기

    [Header("아이템과 NPC")]
    public Vector3 INpcSize = new Vector3(3, 2, 2); // 아이템과 NPC 감지 범위
    public Vector3 INpcRangeOffset; //  중심 위치 조절
    public Color INpcRangeColor = Color.red; //  범위색상 : 빨간색

    [Header("땅 감지")]
    public Vector3 FlySize = new Vector3(3, 3, 3); // 이 범위 안에 땅이 없으면 활강 상태로 변경 가능
    public Vector3 FlyRangeOffset; // 중심 위치 조절
    public Color FlyRangeColor = Color.blue; // 범위 색상 : 파란색

    [Header("벽 감지")]
    public Vector3 ClimbSize = new Vector3(3, 3, 3); //벽 감지 범위
    public Vector3 ClimbRangeOffset; //중심 위치 조절
    public Color ClimbRangeColor = Color.green;//범위 색상 : 초록색

    [Header("벽 등반 완료")]
    public float rayLength = 1.0f; // Raycast 길이
    public Vector3 ClimbTopOffset;
    public Color ClimbTopColor = Color.red; // Raycast 색상

    [Header("기즈모 표시 설정")]
    public bool showItemNpcGizmos = true; // 아이템과 NPC 감지 범위 기즈모
    public bool showGroundGizmos = true; // 땅 감지 범위 기즈모
    public bool showClimbGizmos = true; // 벽 감지 범위 기즈모
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
            if (IsClimbRange()) // 벽 감지
            {
                tpm.Set_PlayerState(CharacterState.Climbing);
            }
    }
     void OnDrawGizmos()//기즈모 보이게하기
    {
        // 플레이어의 전방을 향하는 방향과 오프셋을 결합하여 최종 위치 계산
        Vector3 npcRangePosition = CalculateOffsetPosition(INpcRangeOffset);
        Vector3 flyRangePosition = CalculateOffsetPosition(FlyRangeOffset);
        Vector3 climbRangePosition = CalculateOffsetPosition(ClimbRangeOffset);
        Vector3 climbTopPosition = transform.position + ClimbTopOffset;
        // Vector3 climbTopPosition = CalculateOffsetPosition(ClimbTopOffset);


        // 아이템과 NPC 감지 범위 기즈모
        if (showItemNpcGizmos)
        {
            Gizmos.color = INpcRangeColor;
            Gizmos.DrawCube(CalculateOffsetPosition(INpcRangeOffset), INpcSize);
        }

        // 땅 감지 범위 기즈모
        if (showGroundGizmos)
        {
            Gizmos.color = FlyRangeColor;
            Gizmos.DrawCube(CalculateOffsetPosition(FlyRangeOffset), FlySize);
        }

        // 벽 감지 범위 기즈모
        if (showClimbGizmos)
        {
            Gizmos.color = ClimbRangeColor;
            Gizmos.DrawCube(CalculateOffsetPosition(ClimbRangeOffset), ClimbSize);
        }
        if (showClimbTopGizmos)
        {
            Gizmos.color = ClimbTopColor;
            Gizmos.DrawRay(climbTopPosition, transform.forward * rayLength); // rayLength는 Ray의 길이
        }
    }
    [Header("아이템과 NPC 감지")]
    [SerializeField] LayerMask ItemLayer;
    //[SerializeField] LayerMask NPCLayer;
    /*
    public GameObject FindINPCRange() // 아이템, npc 감지
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + INpcRangeOffset, INpcSize / 2, Quaternion.identity, ItemLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NPC") || hitCollider.CompareTag("Item"))
            {
                return hitCollider.gameObject; // 감지된 객체 반환
            }
        }
        return null; // 객체가 없으면 null 반환
    }*/
    public List<GameObject> FindINPCRange()
    {
        List<GameObject> items = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapBox(CalculateOffsetPosition(INpcRangeOffset), INpcSize / 2, Quaternion.identity, ItemLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NPC") || hitCollider.CompareTag("Item"))
            {
                items.Add(hitCollider.gameObject); // 감지된 객체 추가
            }
        }
        return items; // 객체 리스트 반환
    }
    [Header("날기위한 땅 감지")]
    [SerializeField] LayerMask GroundLayer;
    public bool IsGroundInFlyRange()//땅 감지
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
    [Header("등반을 위한 벽 감지")]
    [SerializeField] LayerMask WallLayer;
    [SerializeField] Collider[] hitColliders;
    public bool IsClimbRange()//벽 감지
    {
        hitColliders = Physics.OverlapBox(CalculateOffsetPosition(ClimbRangeOffset), ClimbSize / 2, Quaternion.identity, WallLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Wall")) // 'Wall' 태그를 가진 오브젝트를 감지
            {
                Debug.Log("벽 감지됨");
                return true;
            }
        }
        Debug.Log("벽 없음");
        return false;
        /*
        Vector3 worldPosition = transform.position + ClimbRangeOffset;
        bool isWallDetected = Physics.CheckBox(worldPosition, ClimbSize / 2, Quaternion.identity, WallLayer);

        // 디버깅 정보 출력
        Debug.Log("벽 감지 결과: " + isWallDetected);

        return isWallDetected;
        */

    }

    public bool IsClimbTop()
    {
        RaycastHit hit;
        // Raycast 시작점 설정 (일반적으로 캐릭터의 위치)
        Vector3 start = transform.position;
        // Raycast 방향 설정 (일반적으로 캐릭터의 전방)
        Vector3 direction = transform.forward;

        // Scene 뷰에서 Raycast 시각화
        Debug.DrawRay(start, direction * rayLength, ClimbTopColor);

        // Raycast 실행
        if (Physics.Raycast(start, direction, out hit, rayLength))
        {
            // Raycast가 벽과 충돌했는지 확인
            if (hit.collider.CompareTag("Wall"))
            {
                // 벽과 충돌했으면 true 반환
                return true;
            }
        }

        // 벽과 충돌하지 않았으면 false 반환
        return false;
    }
    private Vector3 CalculateOffsetPosition(Vector3 offset)//캐릭터가 보는 방향으로 기즈모 회전
    {
        return transform.position + transform.forward * offset.z +
                                   transform.right * offset.x +
                                   transform.up * offset.y;
    }
}
