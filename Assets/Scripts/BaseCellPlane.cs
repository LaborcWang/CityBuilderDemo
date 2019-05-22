using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

public enum CellType {Plane, Road, River};

public class BaseCellPlane : MonoSingleton<BaseCellPlane>
{
    private GameObject[] cellPlanes;
    private const float cellSize = 2.5f;
    private BaseCell[,] baseCell;

    private void Start()
    {
        cellPlanes = GameObject.FindGameObjectsWithTag("Cell");
        FillBaseCells();
        print(ToGridPosition( ToWorldPosition(baseCell[0, 1].Position)));
    }

    private GameObject FindBottomLeftCell()
    {
        GameObject tempGameObject = null;
        foreach(GameObject cellPlane in cellPlanes)
        {
            if(tempGameObject == null)
            {
                tempGameObject = cellPlane;
                continue;
            }
            if(tempGameObject.transform.position.x>=cellPlane.transform.position.x 
            && tempGameObject.transform.position.z>=cellPlane.transform.position.z)
            {
                tempGameObject = cellPlane;
            }
        }
        return tempGameObject;
    }

    private GameObject FindTopRightCell()
    {
        GameObject tempGameObject = null;
        foreach (GameObject cellPlane in cellPlanes)
        {
            if (tempGameObject == null)
            {
                tempGameObject = cellPlane;
                continue;
            }
            if (tempGameObject.transform.position.x <= cellPlane.transform.position.x
            && tempGameObject.transform.position.z <= cellPlane.transform.position.z)
            {
                tempGameObject = cellPlane;
            }
        }
        return tempGameObject;
    }

    private void FillBaseCells()
    {
        Vector3 bottomLeftPosition = FindBottomLeftCell().transform.position;
        Vector3 topRightPosition = FindTopRightCell().transform.position;
        Vector3 gridSize = (topRightPosition - bottomLeftPosition) / cellSize;
        Vector2Int gridSizeInt = new Vector2Int(
            Mathf.RoundToInt(gridSize.x) + 1, 
            Mathf.RoundToInt(gridSize.z) + 1);

        print(bottomLeftPosition);
        print(topRightPosition);
        print(gridSize);
        baseCell = new BaseCell[gridSizeInt.x, gridSizeInt.y];
        Vector3 currentPosition = bottomLeftPosition + Vector3.up;
        RaycastHit hit;

        for (int x = 0; x < gridSizeInt.x; x++)
        {
            for (int y = 0; y < gridSizeInt.y; y++)
            {
                Physics.Raycast(currentPosition + Vector3.forward * cellSize * y, Vector3.down, out hit);
                
                baseCell[x, y] = hit.collider.GetComponent<BaseCell>();
                baseCell[x, y].Position = new Vector2Int(x, y);
            }
            currentPosition += Vector3.right * cellSize;
        }
    }

    public Vector3 ToWorldPosition(Vector2Int gridPosition)
    {
        Vector3 bottomLeftPosition = FindBottomLeftCell().transform.position;
        Vector3 currentCellPosition = new Vector3(gridPosition.x * cellSize + bottomLeftPosition.x,
                                        bottomLeftPosition.y, gridPosition.y * cellSize + bottomLeftPosition.z);


        return currentCellPosition;
    }

    public Vector2Int ToGridPosition(Vector3 worldPosition)
    {
        Vector3 bottomLeftPosition = FindBottomLeftCell().transform.position;
        int xGridPosition = (int)Mathf.Abs((worldPosition.x - bottomLeftPosition.x) / cellSize);
        int yGridPosition = (int)Mathf.Abs((worldPosition.z - bottomLeftPosition.z) / cellSize);
        return new Vector2Int(xGridPosition,yGridPosition);
    }
}
