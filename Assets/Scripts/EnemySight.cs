using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110.0f; // ����̍L��
    public bool playerInSight; // �v���C���[��������ɂ��邩�ǂ���
    public Vector3 lastPlayerSighting; // �Ō�Ɏ�����ɂ����v���C���[���W

    private GameObject player; // �v���C���[
    private Vector3 previousSighting; // �ȑO�̎�����

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    playerInSight = true;
                    lastPlayerSighting = player.transform.position;
                }
            }
        }
    }
}
