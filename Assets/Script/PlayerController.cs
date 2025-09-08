using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("이동설정")]
    public float walkspeed = 3f;
    public float runspeed = 6f;
    public float rotationSpeed = 10;

    [Header("공격설정")]
    public float attackDuration = 0.8f;  //공격 지속 시간
    public bool canMoveWhileAttacking = false;  //공격중 이동 가능여부

    [Header("컴포넌트")]
    public Animator animator;

    private CharacterController controller;
    private Camera playerCamera;

    //현재 상태
    private float currentSpeed;
    private bool isAttacking = false;   //공격중인지 체크

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

    void HandleMovement() //이동 함수 제작
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0) //둘중에 하나 라도 입력이 됬을떄
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;  //이동방향 설정

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runspeed;
            }
            else
            {
                currentSpeed = walkspeed;
            }

            controller.Move(moveDirection * currentSpeed * Time.deltaTime); //캐릭터 컨트롤러의 이동 입력

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
        else  //이동이 아닐 경우 스피드 0
        {
            currentSpeed = 0;
        }


    }

    void UpdateAnimator()
    {
        //전체 최대 속도(runSpeed) 기준으로 0 ~~ 1 계산
        float animatorSpeed = Mathf.Clamp01(currentSpeed / runspeed);
        animator.SetFloat("speed", animatorSpeed);
    }



}
