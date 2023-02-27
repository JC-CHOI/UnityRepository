using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //ĳ���� �̵� ����
    
    public float fMoveSpeed = 3f;

    // �̵� ���� ����
    public Vector2 vMoveDir = Vector2.zero;

    // ��� ������ �����ִ��� �Ǵ��� ����
    public bool isLookLeft = false;

    // ���ϸ����� ����
    Animator animator;

    // ĳ���Ͱ� �̵��ϴ��� �Ǵ��� ����
    bool isPlayerRun = false;

    // Rigidbody 2D ����
    Rigidbody2D rigidbody2d;

    [SerializeField]
    float fJumpPower = 5f;

    // ���� ����
    bool isJump = false;
    bool isFall = false;

    /*
    // �����ؼ� �ö� ��, ������ �� ��������Ʈ
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
            //Rigidbody2d �� ������ Forcemode �� 2d�� �����
            rigidbody2d.AddForce(Vector2.up * fJumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
        spriteRenderer.flipX = isLookLeft;
        //isLookLeft = true ? isLookLeft = true : isLookLeft = false;

        /*
        if( isJump == false)
        {
            // player_run ���ϸ��̼� ���
            animator.SetBool("isRun", isPlayerRun);
        }
        else
        {
            if( rigidbody2d.velocity.y >= 0)
            {
                // �ö󰡰� �ִ� �� - ���� �������� �ۿ�
                spriteRenderer.sprite = spritePlayerJump;

            }
            else
            {
                // �������� �ִ� �� - ���� �Ʒ������� �ۿ�
                spriteRenderer.sprite = spritePlayerFall;
            }
        }
        */
        if (isJump == true)
        {
            if (rigidbody2d.velocity.y > 0)
            {
                // ���������̰� �ö󰡴� ��
                animator.SetBool("isJump", true);
            }
            else
            {
                // ���� �����̰� �������� ��
                animator.SetBool("isFall", true);

            }
        }
        else 
        {
            animator.SetBool("isRun", isPlayerRun);
            // ���� ���� �ƴ�
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
