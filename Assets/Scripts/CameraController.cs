using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetObj; // 注視点のオブジェクト
    private Vector3 targetPos; // 注視点の座標

    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("RPGHeroHP"); // オブジェクト情報取得
        targetPos = targetObj.transform.position; // 座標取得
    }

    // Update is called once per frame
    void Update()
    {
        // targetの移動量だけカメラも移動
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
    }
}
