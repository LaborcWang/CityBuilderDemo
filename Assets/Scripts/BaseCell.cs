using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType { Plane, Road, River };

public class BaseCell : MonoBehaviour
{
	[SerializeField] bool canBuildBuilding;
	[SerializeField] Vector3 buildingOffset = Vector3.up * 0.5f;
	[SerializeField] CellType cellType;
    [SerializeField] Vector2Int position;
    [SerializeField] Building building;

    public CellType CellType => cellType;
	public Building Building => building;
	public bool CanBuildBuilding => canBuildBuilding;

	public Vector2Int Position
    {
        get => position;
        set => position = value;
    }

    public Vector3 WorldPosition
    {
        get => transform.position;
        set => transform.position = value;
    }

    public void BuildBuilding(Building building)
    {
        this.building = building;
        building.transform.position = this.transform.position + buildingOffset;
    }
}
