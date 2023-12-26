using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110.0f; // 視野の広さ
    public bool playerInSight; // プレイヤーが視野内にいるかどうか
    public Vector3 lastPlayerSighting; // 最後に視野内にいたプレイヤー座標

    private GameObject player; // プレイヤー
    private Vector3 previousSighting; // 以前の視野情報

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
