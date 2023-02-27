using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //캐릭터 이동 변수
    
    public float fMoveSpeed = 3f;

    // 이동 방향 변수
    public Vector2 vMoveDir = Vector2.zero;

    // 어느 방향을 보고있는지 판단할 변수
    public bool isLookLeft = false;

    // 에니메이터 변수
    Animator animator;

    // 캐릭터가 이동하는지 판단할 변수
    bool isPlayerRun = false;

    // Rigidbody 2D 변수
    Rigidbody2D rigidbody2d;

    [SerializeField]
    float fJumpPower = 5f;

    // 점프 상태
    bool isJump = false;
    bool isFall = false;

    /*
    // 점프해서 올라갈 때, 내려갈 때 스프라이트
    [SerializeField]
    Sprite spritePlayerJump;
    [SerializeField]
    Sprite spritePlayerFall;
    */
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey(KeyCode.LeftArrow))
        {
            vMoveDir = Vector2.left;
            isLookLeft = true;
            isPlayerRun = true;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            vMoveDir = Vector2.right;
            isLookLeft = false;
            isPlayerRun = true;
        }
        else
        {
            vMoveDir = Vector2.zero;
            isPlayerRun = false;
        }

        // 
        if( Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            //Rigidbody2d 를 썼으면 Forcemode 도 2d를 써야함
            rigidbody2d.AddForce(Vector2.up * fJumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
        spriteRenderer.flipX = isLookLeft;
        //isLookLeft = true ? isLookLeft = true : isLookLeft = false;

        /*
        if( isJump == false)
        {
            // player_run 에니매이션 재생
            animator.SetBool("isRun", isPlayerRun);
        }
        else
        {
            if( rigidbody2d.velocity.y >= 0)
            {
                // 올라가고 있는 중 - 힘이 위쪽으로 작용
                spriteRenderer.sprite = spritePlayerJump;

            }
            else
            {
                // 내려오고 있는 중 - 힘이 아래쪽으로 작용
                spriteRenderer.sprite = spritePlayerFall;
            }
        }
        */
        if (isJump == true)
        {
            if (rigidbody2d.velocity.y > 0)
            {
                // 점프상태이고 올라가는 중
                animator.SetBool("isJump", true);
            }
            else
            {
                // 점프 상ㄷ태이고 내려가는 중
                animator.SetBool("isFall", true);

            }
        }
        else 
        {
            animator.SetBool("isRun", isPlayerRun);
            // 점프 중이 아님
            animator.SetBool("isJump", false);
            animator.SetBool("isFall", false);
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(vMoveDir * Time.deltaTime * fMoveSpeed);
    }

    public void SetJumpState(bool value)
    {
        isJump = value;
    }
}
