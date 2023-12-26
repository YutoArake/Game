using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetObj; // �����_�̃I�u�W�F�N�g
    private Vector3 targetPos; // �����_�̍��W

    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("RPGHeroHP"); // �I�u�W�F�N�g���擾
        targetPos = targetObj.transform.position; // ���W�擾
    }

    // Update is called once per frame
    void Update()
    {
        // target�̈ړ��ʂ����J�������ړ�
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
    }
}
