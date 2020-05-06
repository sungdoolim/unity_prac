using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject capsule;
    public float speed = 3.0f;
    // Start is called before the first frame update
    public Animator animator;
    private float h;
    private float v;
    private float moveX;
    private float moveZ;
    private float speedH = 50f;// 옆으로 이동
    private float speedZ = 80f;// 앞으로 이동
    public Rigidbody rigidbody;
    private bool got = false;
    void Start()
    {
        Debug.Log("초기화가 이루어집니다 - Start부분입니다");
        capsule = GameObject.Find("Capsule");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //capsule.GetComponent<Transform>().Translate(Vector3.left*speed*Time.deltaTime);//z축 앞으로 전진!!!!z축이 default인가봐
        if (Input.GetKeyDown(KeyCode.Space))// 한번만 스페이스 눌렀을때!
        {
            animator.Play("JUMP00", -1, 0);
              }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");// 사용자가 버튼 누를때마다 위치 정보를 받아오는 거래~

        animator.SetFloat("h", h);
        animator.SetFloat("v", v);

        moveX = h * speedH * Time.deltaTime;
        moveZ = v * speedZ * Time.deltaTime;
        if (moveZ <= 0)
        {
            moveX = 0;
        }
        rigidbody.velocity = new Vector3(moveX, 0, moveZ);

        //this.transform.Translate(~~~) 로 했어도 됨 : 이 스크립트 사용하는 객체 모두 적용
        // deltatime : 기계평준화
        if (Input.GetKey(KeyCode.S))// s누르면 child로 품어서 같이 다님
        {
            GameObject child = GameObject.Find("Capsule") as GameObject;
            if (!got)
            {
                child.transform.parent = this.transform;
                got = true;
            }
            else
            {
                child.transform.parent = null;
                got = false;
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Cube")
        {
            Debug.Log("충돌감지");
            animator.Play("DAMAGED01", -1, 0);
            this.transform.Translate(Vector3.back * speedZ * Time.deltaTime);
        }
    }
    private void onCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Cube")
        {
            Debug.Log("충동 유지");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Cube")
        {
            Debug.Log("충돌 종료");
        }
    }
}
