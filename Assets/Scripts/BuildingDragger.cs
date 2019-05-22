using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDragger : MonoBehaviour
{
	[SerializeField] Renderer _highLightObj;
	[SerializeField] Color _buildableHighLightColor = Color.green;
	[SerializeField] Color _unbuildableHighLightColor = Color.red;

	private Camera _currentCamera;
	private BaseCell _currentOnCell;

	private void Awake()
	{
		_currentCamera = Camera.main;
	}

	void Update()
    {
		Dragging();
	}

	void Dragging()
	{
		Ray cameraRay = _currentCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(cameraRay, out hit, float.PositiveInfinity, ~LayerMask.GetMask("Cube")))
		{
			transform.position = hit.point + Vector3.up * 0.5f;
			_currentOnCell = BaseCellPlane.GetCell(BaseCellPlane.ToGridPosition(hit.point));
			if (!_currentOnCell)
				return;

			_highLightObj.gameObject.SetActive(true);
			_highLightObj.transform.position = _currentOnCell.WorldPosition + Vector3.up * 0.001f;

			_highLightObj.material.color = 
				_currentOnCell.CanBuildBuilding && _currentOnCell.Building == null ?
				_buildableHighLightColor :
				_unbuildableHighLightColor;
		}
		else
		{
			transform.position = _currentCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward);
			_highLightObj.gameObject.SetActive(false);
			_currentOnCell = null;
		}
	}

	public bool PutDownCube()
	{
		this.enabled = false;
		_highLightObj.gameObject.SetActive(false);

		if (_currentOnCell == null
		 || _currentOnCell.Building != null
		 || !_currentOnCell.CanBuildBuilding)
		{
			return false;
		}

		_currentOnCell.BuildBuilding(GetComponent<Building>());
		CalculateScore.Calculate(GetComponent<Building>());

		return true;
	}
}
