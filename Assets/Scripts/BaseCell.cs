using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCell : MonoBehaviour
{
    [SerializeField] Vector3 buildingOffset = Vector3.up * 0.5f;
    [SerializeField] CellType cellType;
    [SerializeField] Vector2Int position;
    [SerializeField] Vector3 worldPosition;
    [SerializeField] Building building;

    public CellType CellType => CellType;
    public Vector2Int Position
    {
        get => position;
        set => position = value;
    }

    public Vector3 WorldPosition
    {
        get => worldPosition;
        set => worldPosition = value;
    }

    public Building Building => building;

    public void BuildBuilding(Building building)
    {
        this.building = building;
        building.transform.position = this.transform.position + buildingOffset;
    }
}
