using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHighLightObjs : MonoBehaviour
{
    private int rangeBuildingSet;
    private Vector2Int position; //Dragging buidling position
    [SerializeField]
    private GameObject highLightObj;
    const float cellSize = 2.5f;

    private void Start()
    {
        rangeBuildingSet = this.GetComponent<Building>().Range;

    }

    private void Update()
    {
        position = BaseCellPlane.ToGridPosition(GetComponent<Building>().transform.position);
        InstanceHighLightObjs(0, rangeBuildingSet, position, highLightObj);
    }

    private void InstanceHighLightObjs(int range, int rangeBuidlingSet,Vector2Int position, GameObject highLightObj)
    {

        Vector3 gridToWorldpos = BaseCellPlane.ToWorldPosition(position);
        Instantiate(highLightObj, gridToWorldpos + Vector3.up * 0.001f, this.transform.rotation, this.transform);


        range++;
        if (range > rangeBuidlingSet)
            return;

        Vector2Int[] nextPosition =
        {
            position + Vector2Int.right,
            position + Vector2Int.up,
            position + Vector2Int.left,
            position + Vector2Int.down

        };

        foreach(var pos in nextPosition)
        {
            InstanceHighLightObjs(range, rangeBuidlingSet, pos, highLightObj);
        }
    }

    bool outOfRange(Vector2Int position)
    {
        Vector3 leftBottomObjPos = BaseCellPlane.getLeftBottomCell().transform.position;
        Vector3 rightTopObjPos = BaseCellPlane.getRightTopCell().transform.position;
        Vector3 gridSize = (rightTopObjPos - leftBottomObjPos) / cellSize;
        Vector2Int gridSizeInt = new Vector2Int(
            Mathf.RoundToInt(gridSize.x) ,
            Mathf.RoundToInt(gridSize.z) );
        Vector2Int leftBottomObjGridPos = BaseCellPlane.ToGridPosition(leftBottomObjPos);
        Vector2Int rightTopObjGridPos = BaseCellPlane.ToGridPosition(rightTopObjPos);

        if ((position.x - leftBottomObjGridPos.x) > gridSizeInt.x || (position.y - leftBottomObjGridPos.y) > gridSizeInt.y
            || Mathf.Abs((position.x - rightTopObjGridPos.x)) > gridSizeInt.x || Mathf.Abs((position.y - rightTopObjGridPos.y)) > gridSizeInt.y)
            return true;

        return false;
    }
}
