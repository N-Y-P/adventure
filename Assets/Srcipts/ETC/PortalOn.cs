using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOn : MonoBehaviour
{
    [SerializeField]
    ItemRange _itemrange;
    [SerializeField]
    GameObject CharacterModel;

    [Header("��Ż ���� ��ġ")]
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
        //���� ���� �ȿ� ��Ż�� ������ [F]�Ʒ��� �̵� / [F]���� �̵� ui����
        //������ ������ �� ���� 1.�׸���Ż 2. ������Ż
    } 
}
