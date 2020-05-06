using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart : MonoBehaviour
{
    public float speed = 5.0f;
    public float Rspeed = 100.0f;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.P))
        {// 위 방향키누르면또는p키를 눌러도 z축으로 이동
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {//x축으로 이동
         //   this.transform.Translate(speed * Time.deltaTime, 0, 0);
            this.transform.Rotate(0, Rspeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {//  -z축으로 이동
            this.transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {// -x축으로 이동
         //   this.transform.Translate(-speed * Time.deltaTime, 0, 0);
            this.transform.Rotate(0, -Rspeed * Time.deltaTime, 0);
        }
    }
}
