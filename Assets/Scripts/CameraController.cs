using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private GameObject targetObj; // 注視点のオブジェクト
    private Vector3 targetPos; // 注視点の座標
    public float rotateSpeed = 2.0f;   // 回転の速さ

    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("RPGHeroHP"); // オブジェクト情報取得
        targetPos = targetObj.transform.position; // 座標取得
    }

    // Update is called once per frame
    void Update()
    {
        // カメラの移動
        MoveCamera();

        // カメラの回転
        RotateCamera(); 
    }

    // カメラ移動用の関数
    private void MoveCamera()
    {
        // targetの移動量だけカメラも移動
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
    }

    // カメラ回転用の関数
    private void RotateCamera()
    {
        // マウスのX、Y移動量分で回転度合いを決める
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed, 0);
        transform.RotateAround(targetPos, Vector3.up, angle.x);
        transform.RotateAround(targetPos, transform.right, -angle.y);
    }

    // カメラ位置リセット
    public void OnReset(InputAction.CallbackContext context)
    {
        transform.position = new Vector3(targetPos.x, targetPos.y + 2.5f, targetPos.z);
        transform.rotation = Quaternion.identity;
    }
}
