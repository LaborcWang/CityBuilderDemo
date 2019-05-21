using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{

    public Material m1;
    public RaycastHit HitInfo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckBarrier();
    }
    void CheckBarrier()
    {

        //创建一个射线 关键字Ray

        //第一个参数放的是发射射线的物体的位置，第二个参数放的是发射射线的方向

        Ray ray1 = new Ray(this.transform.position, -this.transform.up);

        //发射射线

        

        //射线默认长度为无穷大，想要设置射线长度加一个参数即可，例如设置射线长度为五（ray，out HitInfo，5）

        bool result = Physics.Raycast(ray1, out HitInfo);

        Debug.DrawRay(ray1.origin, ray1.direction,Color.red);

        //判断射线是否碰到物体，碰到物体打印碰撞到的物体的名字

        if (result)
        {

            //Debug.Log(HitInfo.collider.name);
            HitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
