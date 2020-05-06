using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed=3;
    public float jumpPower = 30;
    SpriteRenderer spriteRenderer;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Jump")&&!anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);

        }
        if (Input.GetButtonUp("Horizontal"))
        {

            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h*2, ForceMode2D.Impulse);
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }else if (rigid.velocity.x < maxSpeed * -1)
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
        }

        //Landing platform
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 2,LayerMask.GetMask("makedlayer"));
        // 내 위치에서 아래쪽으로 1만큼 레이저 발싸
        // ㅅㄹ정한 layer만! 감지 하겠다!!
        if(rayHit.collider != null)
        {
            Debug.Log(anim.GetBool("isJump"));
            Debug.Log("?");
            //레이저가 어떤 것과 닿으면...
            // 근데 얘는 자신의 가운데서 쏘고 자신의 콜리더와 부딛혀도 인식함
            // 한번 인식하면 그다음 끝남....
            //때문에 LayerMask 사용 = 경계 닿아도 무시
            if (rigid.velocity.y < 0)
            {
                if (rayHit.distance < 2f)// 길이가 감지된 곳부터의 길이인가본대?
                {
                    anim.SetBool("isJump", false);// 바닥에 있을때는 점프 한거 아닌대
                }
            }
        }
    }
}
