using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeCreateUI : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public GameObject cloneCube;
    private bool cloneSign = false;
    private GameObject clone;

    private void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clone = Instantiate(cloneCube, cloneCube.transform.position, Quaternion.identity);
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 0);
        clone.transform.position = mousepos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clone.GetComponent<ray>().PutDownCube();
    }
}
