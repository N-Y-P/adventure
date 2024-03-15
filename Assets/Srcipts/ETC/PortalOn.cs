using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOn : MonoBehaviour
{
    [SerializeField]
    ItemRange _itemrange;
    [SerializeField]
    GameObject CharacterModel;

    [Header("포탈 스폰 위치")]
    [SerializeField]
    public Transform GreenPosition;
    [SerializeField]
    public Transform YellowPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Portalmove()
    {
        //만약 범위 안에 포탈이 들어오면 [F]아래로 이동 / [F]위로 이동 ui생성
        //누르면 워프할 수 있음 1.그린포탈 2. 옐로포탈
    } 
}
