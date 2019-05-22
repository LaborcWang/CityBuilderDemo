using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CubeCreateUI : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
	private GameObject _prefab;
    private GameObject _buidlingInstance;

	public void Initialize(GameObject prefab)
	{
		_prefab = prefab;
		GetComponentInChildren<Text>().text = _prefab.GetComponent<Building>().Type.ToString();
	}

	public void OnPointerDown(PointerEventData eventData)
    {
        _buidlingInstance = Instantiate(_prefab, _prefab.transform.position, Quaternion.identity);
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _buidlingInstance.transform.position = mousepos;
		_buidlingInstance.GetComponent<BuildingDragger>().enabled = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (_buidlingInstance.GetComponent<BuildingDragger>().PutDownCube())
		{
			Destroy(this.gameObject);
		}
    }
}
