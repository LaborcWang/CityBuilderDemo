using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{

    public Material m1;
    public RaycastHit HitInfo;
<<<<<<< HEAD
    public Transform target;
    public bool mv = true;
=======
    private bool enterSquareSign = true;
    private string hittedObjectNameTemp = "";
    private GameObject hittedObject = null;
    private Color hittedObjectColor;
>>>>>>> b857a22ef282f60b3bc5dedf4a7e0266905b4a61
    // Use this for initialization
    void Start()
    {
       // target = HitInfo.collider.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBarrier();
       // OnMouseUp();
    }
    void CheckBarrier()
    {

        //创建一个射线 关键字Ray
        //第一个参数放的是发射射线的物体的位置，第二个参数放的是发射射线的方向
<<<<<<< HEAD
        if (mv == true)
        {
            Ray ray1 = new Ray(this.transform.position, -this.transform.up);

            //发射射线



            //射线默认长度为无穷大，想要设置射线长度加一个参数即可，例如设置射线长度为五（ray，out HitInfo，5）

            bool result = Physics.Raycast(ray1, out HitInfo);

            Debug.DrawRay(ray1.origin, ray1.direction, Color.red);

            //判断射线是否碰到物体，碰到物体打印碰撞到的物体的名字

            if (result)
            {

                //Debug.Log(HitInfo.collider.name);
                HitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                target = HitInfo.collider.gameObject.transform;
            }
        }
       
        
        

    }

    public void OnMouseUp()
    {
        if (mv == true)
        {
            this.transform.position
            = new Vector3(HitInfo.collider.gameObject.transform.position.x,
            HitInfo.collider.gameObject.transform.position.y + 0.5f,
            HitInfo.collider.gameObject.transform.position.z);
           // this.transform.position = HitInfo.collider.gameObject.transform.position;
            if (HitInfo.collider.gameObject.tag == "road" || HitInfo.collider.gameObject.tag == "cube")
            {
                Destroy(this.gameObject);
=======
        Ray ray1 = new Ray(this.transform.position, -this.transform.up);
        Debug.DrawRay(ray1.origin, ray1.direction,Color.red);
        //判断射线是否碰到物体，碰到物体打印碰撞到的物体的名字
        if (Physics.Raycast(ray1, out HitInfo))
        {
            if(enterSquareSign == true)
            {
                hittedObjectColor = HitInfo.collider.gameObject.GetComponent<Renderer>().material.color;
                HitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                hittedObjectNameTemp = HitInfo.collider.name;
                hittedObject = HitInfo.collider.gameObject;
                enterSquareSign = false;
            }
        }
        if (enterSquareSign == false && Physics.Raycast(ray1, out HitInfo))
        {
            if (hittedObjectNameTemp != HitInfo.collider.name)
            {
                hittedObject.GetComponent<Renderer>().material.color = hittedObjectColor;
                enterSquareSign = true;
                hittedObjectNameTemp = HitInfo.collider.name;
>>>>>>> b857a22ef282f60b3bc5dedf4a7e0266905b4a61
            }
        }
        
        mv = false; 


    }
}
