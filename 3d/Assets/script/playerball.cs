using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerball : MonoBehaviour
{
    Rigidbody rigid;
    public float jump_power=20;
    bool isJump;
    public int itemCount;
    AudioSource audios;
    public GameManagerLogic manager;// 이렇게하면 find 필요없이 클래스 찾는거...뭐여,.,


    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(manager.TotalItemCount);
        isJump = false;
        rigid = GetComponent<Rigidbody>();//가져오기    

        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);




    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Jump")&&!isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jump_power, 0), ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")// 부딫히면
        {
            isJump = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            other.gameObject.SetActive(false); //지금 내가 없어짐
            playerball player = other.GetComponent<playerball>();
            itemCount++;
            audios.Play();
            manager.GetItem(itemCount);
        }
        else if (other.tag == "point")
        {
            Debug.Log(manager.TotalItemCount);
            Debug.Log(itemCount);
            audios.Play();
      
            //    playerTransform = GameObject.FindGameObjectWithTag("finish").transform;
            if (itemCount == manager.TotalItemCount)
            {
                //clear next level

                SceneManager.LoadScene(manager.stage+1);
            }
            else
            {
                SceneManager.LoadScene(manager.stage);
                //장면 전환/ 다시하기
            }



        }
    }
}
