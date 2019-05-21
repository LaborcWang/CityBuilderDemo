 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour {
    public GameObject redcube;
    public ray ray1_object;
    private Camera _currentCamera;
    private Transform _target;
    public ray rayScript;
    private void Awake()
    {
        _currentCamera = Camera.main;
        _target = transform;
    }

    private void OnMouseDrag()
    {
        if (rayScript.mv == true)
        {
            Vector3 CO_Direction = _target.position - _currentCamera.transform.position;
            float cPlane = Vector3.Dot(CO_Direction, _currentCamera.transform.forward);
            Ray cameraRay = _currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit, float.PositiveInfinity, ~LayerMask.GetMask("Cube")))
            {
                print(hit.collider.name);
                _target.position = hit.point + Vector3.up * 0.5f;
            }
            else
            {
                _target.position = _currentCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            }
            return;
            //得到摄像机到物体的向量

            //Vector3 CO_Direction = _target.position - _currentCamera.transform.position;
            //得到摄像机与物体所在平面的距离
            //float cPlane = Vector3.Dot(CO_Direction, _currentCamera.transform.forward);
            _target.position = _currentCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cPlane));
            if (ray1_object.HitInfo.collider != null)
            {
                _target.position = ray1_object.HitInfo.point + Vector3.up * 0.5f;
            }
            else
            {
                _target.position = _currentCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cPlane));
            }
        }
        

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 0);
        //clone.transform.position = mousepos;
    }
    public void OnMouseDown()
    {
       // GameObject clone = Instantiate(redcube, redcube.transform.position, Quaternion.identity);
        //Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 0);
        //clone.transform.position = mousepos;
    }

	private void OnMouseUp()
	{
		
	}

}
