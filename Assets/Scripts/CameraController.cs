using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private GameObject targetObj; // �����_�̃I�u�W�F�N�g
    private Vector3 targetPos; // �����_�̍��W
    public float rotateSpeed = 2.0f;   // ��]�̑���

    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("RPGHeroHP"); // �I�u�W�F�N�g���擾
        targetPos = targetObj.transform.position; // ���W�擾
    }

    // Update is called once per frame
    void Update()
    {
        // �J�����̈ړ�
        MoveCamera();

        // �J�����̉�]
        RotateCamera(); 
    }

    // �J�����ړ��p�̊֐�
    private void MoveCamera()
    {
        // target�̈ړ��ʂ����J�������ړ�
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
    }

    // �J������]�p�̊֐�
    private void RotateCamera()
    {
        // �}�E�X��X�AY�ړ��ʕ��ŉ�]�x���������߂�
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed, 0);
        transform.RotateAround(targetPos, Vector3.up, angle.x);
        transform.RotateAround(targetPos, transform.right, -angle.y);
    }

    // �J�����ʒu���Z�b�g
    public void OnReset(InputAction.CallbackContext context)
    {
        transform.position = new Vector3(targetPos.x, targetPos.y + 2.5f, targetPos.z);
        transform.rotation = Quaternion.identity;
    }
}
