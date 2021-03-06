﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

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

    public GameObject FindBottomLeftCell()
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

    public GameObject FindTopRightCell()
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

	public static BaseCell GetCell(Vector2Int gridPosition)
	{
		if (gridPosition.x < 0 || gridPosition.x >= Instance.baseCell.GetLength(0)
		 || gridPosition.y < 0 || gridPosition.y >= Instance.baseCell.GetLength(1))
			return null;

		return Instance.baseCell[gridPosition.x, gridPosition.y];
	}

    public static Vector3 ToWorldPosition(Vector2Int gridPosition)
    {
		return Instance.baseCell[gridPosition.x, gridPosition.y].WorldPosition;
    }

    public static Vector2Int ToGridPosition(Vector3 worldPosition)
    {
        Vector3 bottomLeftPosition = Instance.FindBottomLeftCell().transform.position;
        int xGridPosition = (int)Mathf.Abs((worldPosition.x - bottomLeftPosition.x + cellSize * 0.5f) / cellSize);
        int yGridPosition = (int)Mathf.Abs((worldPosition.z - bottomLeftPosition.z + cellSize * 0.5f) / cellSize);

//		Debug.DrawLine(worldPosition, GetCell(new Vector2Int(xGridPosition, yGridPosition)).WorldPosition);

        return new Vector2Int(xGridPosition,yGridPosition);
    }

    ////////////////////////
    /// 以下为添加内容
    /// ////////////////////
    public static GameObject getLeftBottomCell()
    {
        return Instance.FindBottomLeftCell();
    }
    public static GameObject getRightTopCell()
    {
        return Instance.FindTopRightCell();
    }
}
