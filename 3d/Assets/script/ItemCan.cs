using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCan : MonoBehaviour
{
    public float rotateSpeed = 40;


    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {//아이템 회전시키기
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime,Space.World);//y축기준 회전
        
    }
 
}
