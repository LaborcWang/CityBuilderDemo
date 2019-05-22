using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCell : MonoBehaviour
{
    [SerializeField] CellType cellType;
    [SerializeField] Vector2Int position;
    [SerializeField] Building building;

    public CellType CellType => CellType;
    public Vector2Int Position
    {
        get => position;
        set => position = value;
    }

    public Building Building => building;

    public void BuildBuilding(Building building)
    {
        this.building = building;
    }
}
