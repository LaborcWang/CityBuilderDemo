using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    public RaycastHit HitInfo;
    public bool mv = true;

    private Camera _currentCamera;
    private Transform _target;
    private bool enterSquareSign = true;
    private string hittedObjectNameTemp = "";
    private GameObject hittedObject = null;
    private Color hittedObjectColor;
   

    private void Awake()
    {
        _currentCamera = Camera.main;
        _target = transform;
    }

    void Update()
    {
        CheckBarrier();
        DragCube();
        if(Input.GetMouseButtonUp(0))
        {
            PutDownCube();
        }
    }

    private void CheckBarrier()
    {

        Ray ray1 = new Ray(this.transform.position, -this.transform.up);

        if (Physics.Raycast(ray1, out HitInfo))
        {
            if (enterSquareSign == true)
            {
                hittedObjectColor = HitInfo.collider.gameObject.GetComponent<Renderer>().material.color;
                if (HitInfo.collider.gameObject.tag == "road")
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

    public void PutDownCube()
    {
        this.enabled = false;

        if ( HitInfo.collider == null)
        {
            Destroy(this.gameObject);
            hittedObject.GetComponent<Renderer>().material.color = hittedObjectColor;
            return;
        }

        if (mv == true)
        {
            this.transform.position = new Vector3(HitInfo.collider.gameObject.transform.position.x,
            HitInfo.collider.gameObject.transform.position.y + 0.5f,
            HitInfo.collider.gameObject.transform.position.z);
            // this.transform.position = HitInfo.collider.gameObject.transform.position;
            mv = false;
            hittedObject.GetComponent<Renderer>().material.color = hittedObjectColor;
            if (HitInfo.collider.gameObject.tag == "road" )
            {
                Destroy(this.gameObject);
            }

            //this.transform.position
            //= new Vector3(HitInfo.collider.gameObject.transform.position.x,
            //HitInfo.collider.gameObject.transform.position.y + 0.7f,
            //HitInfo.collider.gameObject.transform.position.z);
            CalculateScore.Calculate(GetComponent<Building>());
        }
    }

    private void DragCube()
    {
        if (mv == true)
        {
            Vector3 CO_Direction = _target.position - _currentCamera.transform.position;
            float cPlane = Vector3.Dot(CO_Direction, _currentCamera.transform.forward);
            Ray cameraRay = _currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit, float.PositiveInfinity, ~LayerMask.GetMask("Cube")))
            {
                _target.position = hit.point + Vector3.up * 0.5f;
            }
            else
            {
                _target.position = _currentCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            }
            return;
        }
     }

   
}
