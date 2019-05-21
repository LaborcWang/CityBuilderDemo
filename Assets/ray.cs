using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{

    public Material m1;
    public RaycastHit HitInfo;

    public Transform target;
    public bool mv = true;

    private bool enterSquareSign = true;
    private string hittedObjectNameTemp = "";
    private GameObject hittedObject = null;
    private Color hittedObjectColor;

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

        Ray ray1 = new Ray(this.transform.position, -this.transform.up);
        Debug.DrawRay(ray1.origin, ray1.direction, Color.red);
        //判断射线是否碰到物体，碰到物体打印碰撞到的物体的名字


        if (Physics.Raycast(ray1, out HitInfo))
        {
            if (enterSquareSign == true)
            {
                hittedObjectColor = HitInfo.collider.gameObject.GetComponent<Renderer>().material.color;
                if(HitInfo.collider.gameObject.tag == "road")
                    HitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                else
                    HitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
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

            }
        }
        if (mv == false)
        {
            hittedObject.GetComponent<Renderer>().material.color = hittedObjectColor;
        }
   



    }

    public void OnMouseUp()
    {
        if (mv == true)
        {
            this.transform.position
            = new Vector3(HitInfo.collider.gameObject.transform.position.x,
            HitInfo.collider.gameObject.transform.position.y + 0.7f,
            HitInfo.collider.gameObject.transform.position.z);
            // this.transform.position = HitInfo.collider.gameObject.transform.position;
            //HitInfo.collider.gameObject.tag = "road";


            mv = false;
           
            if (mv == false)
            {
                hittedObject.GetComponent<Renderer>().material.color = hittedObjectColor;
            }

            
        }
        if (HitInfo.collider.gameObject.tag == "road")
        {
            //mv = false;
            Destroy(this.gameObject);


        }

        CalculateScore.Calculate(GetComponent<Building>());
    }

  

}
