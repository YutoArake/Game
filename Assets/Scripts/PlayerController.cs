using System.Collections;
using System.Collections.Generic;
using Unity.Rendering.HybridV2;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{	
	[SerializeField] float moveSpeed = 5.0f; // �ړ����x
	[SerializeField] float jumpForce = 5.0f; // �W�����v��
	[SerializeField] float maxRollingSpeed = 50.0f; // ���[�����O������x
	private float rollingSpeed; // ���[�����O���x

	private Vector3 rollingMoveVector = Vector3.zero; // ���[�����O���̃x�N�g��
	private bool isRolling; // ���[�����O���Ă��邩�ǂ���

    private float horizontal;	// ������
	private float vertical;		// �c����

	private Rigidbody rb; // �v���C���[��rigidbody
	private CapsuleCollider col; // �v���C���[��CapsuleCollider
	private Collider attackCollider; // �U������collider
	private Animator animator; // �v���C���[��animator

    // �n�ʂɂ��邩�ǂ�������p
    [SerializeField] LayerMask groundLayers; // �n��
    [SerializeField] float groundCheckRadius = 0.1f; // �n�ʊ��m�p�̔��a

    // �X�e�[�^�X
    [SerializeField] PlayerStatusSO playerStatus;
    [SerializeField] Text hpText;
    private int currentHP;

	InputAction move;

	// Start is called before the first frame update
	void Start()
	{
        // �R���|�[�l���g���擾
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
		// inputSystem������͏����擾
		var inputMoveAxis = move.ReadValue<Vector2>();

		horizontal = inputMoveAxis.x;	// �������̓��͂��擾
		vertical = inputMoveAxis.y;		// �c�����̓��͂��擾
    }

    private void FixedUpdate()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * vertical + Camera.main.transform.right * horizontal;

        // ��𒆂Ȃ�
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
            // �ړ������ɃX�s�[�h���|����
            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            animator.SetFloat("Walk", rb.velocity.magnitude);   //�����A�j���[�V�����ɐ؂�ւ���
        }

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    // �ړ�
    public void OnMove(InputAction.CallbackContext context)
    {

    }

    // �W�����v
    public void OnJump(InputAction.CallbackContext context)
    {
        // �ڒn���Ă��ĉ�𒆂���Ȃ���΃W�����v����
        if (IsGrounded() && !isRolling)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ������ɗ͂�������
        }
    }

    // ���
    public void OnRolling(InputAction.CallbackContext context)
    {
        // �ڒn���Ă��ĉ�𒆂���Ȃ�������������
        if (IsGrounded() && !isRolling)
        {
            isRolling = true; // ���[�����O����
        }
    }

    // �U��
    public void OnAttack(InputAction.CallbackContext context)
    {
        // ���[�����O������Ȃ���΍U������
        if (!isRolling)
        {
            animator.SetTrigger("Attack"); // �U�����[�V����
        }
    }

    // �n�ʂɂ��邩�ǂ���
    private bool IsGrounded()
	{
		Vector3 groundCheckPosition = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
		return Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundLayers);
	}

	// ����̓����蔻����I�t��
	public void OffColliderAttack()
	{
		attackCollider.enabled = false;
	}
    // ����̓����蔻����I����
    public void OnColliderAttack()
	{
		attackCollider.enabled = true;
	}

    // ��_���[�W���̃A�j���[�V����
    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
		if (damager != null)
		{
			// �G�̍U���ɓ���������A�j���[�V��������
			animator.SetTrigger("Gethit");
		}
    }
}
