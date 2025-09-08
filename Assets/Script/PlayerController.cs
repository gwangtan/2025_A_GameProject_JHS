using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("�̵�����")]
    public float walkspeed = 3f;
    public float runspeed = 6f;
    public float rotationSpeed = 10;

    [Header("���ݼ���")]
    public float attackDuration = 0.8f;  //���� ���� �ð�
    public bool canMoveWhileAttacking = false;  //������ �̵� ���ɿ���

    [Header("������Ʈ")]
    public Animator animator;

    private CharacterController controller;
    private Camera playerCamera;

    //���� ����
    private float currentSpeed;
    private bool isAttacking = false;   //���������� üũ

     void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        UpdateAnimator();
    }

    void HandleMovement() //�̵� �Լ� ����
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0) //���߿� �ϳ� �� �Է��� ������
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;  //�̵����� ����

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runspeed;
            }
            else
            {
                currentSpeed = walkspeed;
            }

            controller.Move(moveDirection * currentSpeed * Time.deltaTime); //ĳ���� ��Ʈ�ѷ��� �̵� �Է�

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
        else  //�̵��� �ƴ� ��� ���ǵ� 0
        {
            currentSpeed = 0;
        }


    }

    void UpdateAnimator()
    {
        //��ü �ִ� �ӵ�(runSpeed) �������� 0 ~~ 1 ���
        float animatorSpeed = Mathf.Clamp01(currentSpeed / runspeed);
        animator.SetFloat("speed", animatorSpeed);
    }



}
