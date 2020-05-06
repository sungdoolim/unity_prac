using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{

    Rigidbody2D rigid;
    public int nextMove=1;
    Animator anim;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think",3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove , rigid.velocity.y);




        Vector2 frontVec = new Vector2(rigid.position.x+nextMove/Mathf.Abs(nextMove),rigid .position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("makedlayer"));
        // 내 위치에서 아래쪽으로 1만큼 레이저 발싸
        // ㅅㄹ정한 layer만! 감지 하겠다!!
        if (rayHit.collider == null)
        {
            turn();
                           
        }



    }
    void Think()
    {
        nextMove = Random.Range(-3 ,4);
        Invoke("Think", 3);
        anim.SetInteger("walkspeed", nextMove);
        if(nextMove!=0)// 플립은 이미지 반전
        spriteRenderer.flipX = (nextMove/Mathf.Abs(nextMove) == 1);
    }
    void turn()
    {
        nextMove *= -1;//낭떠러지면 반대방향으로 가기
        spriteRenderer.flipX = (nextMove / Mathf.Abs(nextMove) == 1);
        CancelInvoke();
        Invoke("Think", 3);
    }
}
