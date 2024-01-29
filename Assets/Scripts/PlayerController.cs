using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{	
	[SerializeField] float moveSpeed = 5.0f; // 移動速度
	[SerializeField] float jumpForce = 5.0f; // ジャンプ力
	[SerializeField] float maxRollingSpeed = 50.0f; // ローリング上限速度
	private float rollingSpeed; // ローリング速度

	private Vector3 rollingMoveVector = Vector3.zero; // ローリング時のベクトル
	private bool isRolling; // ローリングしているかどうか

    private float horizontal;	// 横方向
	private float vertical;		// 縦方向

	private Rigidbody rb; // プレイヤーのrigidbody
	private CapsuleCollider col; // プレイヤーのCapsuleCollider
	private Collider attackCollider; // 攻撃時のcollider
	private Animator animator; // プレイヤーのanimator

    // 地面にいるかどうか判定用
    [SerializeField] LayerMask groundLayers; // 地面
    [SerializeField] float groundCheckRadius = 0.1f; // 地面感知用の半径

    // ステータス
    [SerializeField] PlayerStatusSO playerStatus;
    [SerializeField] Text hpText;
    private int currentHP;

	InputAction move;

	// Start is called before the first frame update
	void Start()
	{
        // コンポーネントを取得
        rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		animator = GetComponent<Animator>();

        currentHP = playerStatus.HP;
        hpText.GetComponent<Text>().text = "HP : " + currentHP;

		var playerInput = GetComponent<PlayerInput>();
		move = playerInput.actions["Move"];

		rollingSpeed = maxRollingSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		// inputSystemから入力情報を取得
		var inputMoveAxis = move.ReadValue<Vector2>();

		horizontal = inputMoveAxis.x;	// 横方向の入力を取得
		vertical = inputMoveAxis.y;		// 縦方向の入力を取得
    }

    private void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vertical + Camera.main.transform.right * horizontal;

        // 回避中なら
        if (isRolling)
        {
            rb.velocity = moveForward * rollingSpeed + new Vector3(0, rb.velocity.y, 0);
            rollingSpeed--;
            if (rollingSpeed <= 0)
            {
                isRolling = false;
                rollingSpeed = maxRollingSpeed;
            }
        }
        else
        {
            // 移動方向にスピードを掛ける
            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            animator.SetFloat("Walk", rb.velocity.magnitude);   //歩くアニメーションに切り替える
        }

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    // 移動
    public void OnMove(InputAction.CallbackContext context)
    {

    }

    // ジャンプ
    public void OnJump(InputAction.CallbackContext context)
    {
        // 接地していて回避中じゃなければジャンプする
        if (IsGrounded() && !isRolling)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 上方向に力を加える
        }
    }

    // 回避
    public void OnRolling(InputAction.CallbackContext context)
    {
        // 接地していて回避中じゃなかったら回避する
        if (IsGrounded() && !isRolling)
        {
            isRolling = true; // ローリング中へ
        }
    }

    // 攻撃
    public void OnAttack(InputAction.CallbackContext context)
    {
        // ローリング中じゃなければ攻撃する
        if (!isRolling)
        {
            animator.SetTrigger("Attack"); // 攻撃モーション
        }
    }

    // 地面にいるかどうか
    private bool IsGrounded()
	{
		Vector3 groundCheckPosition = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
		return Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundLayers);
	}

	// 武器の当たり判定をオフに
	public void OffColliderAttack()
	{
		attackCollider.enabled = false;
	}
    // 武器の当たり判定をオンに
    public void OnColliderAttack()
	{
		attackCollider.enabled = true;
	}

    // 被ダメージ時のアニメーション
    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
		if (damager != null)
		{
			// 敵の攻撃に当たったらアニメーション発生
			animator.SetTrigger("Gethit");
		}
    }
}
