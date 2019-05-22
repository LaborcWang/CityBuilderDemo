using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityUtility;

public class CalculateScore : MonoSingleton<CalculateScore>
{
	[SerializeField] Text scoreText;
	[SerializeField] float gridCellSize;

	[SerializeField] float totalScore;

    public static float Calculate(Building building)
	{
		float score = 0;
		Vector2Int position = BaseCellPlane.ToGridPosition(building.transform.position);
		List<Component> checkedList = new List<Component>();

		checkedList.Add(building);
		checkedList.Add(BaseCellPlane.GetCell(position));

		Instance.GetScores(position, building, 0, checkedList, ref score);
		Instance.totalScore += score;
		Instance.scoreText.text = Instance.totalScore.ToString();

		print(Instance.totalScore);
		return score;
	}

	void GetScores(Vector2Int position, Building building, int range, List<Component> checkedList, ref float score)
	{
		var cell = BaseCellPlane.GetCell(position);
		if (cell != null)
		{
			float value = 0;

			if (cell.Building != null &&
				!checkedList.Contains(cell.Building))
			{
				checkedList.Add(cell.Building);
				if (CheckBuilding(building, cell.Building, out value))
				{
					PopMessage(value, cell.Building.transform.position);
					score += value;
				}
			}

			if (!checkedList.Contains(cell))
			{
				checkedList.Add(cell);
				if (CheckCell(building, cell, out value))
				{
					PopMessage(value, cell.transform.position);
					score += value;
				}
			}
		}

		range++;
		if (range > building.Range)
			return;

		Vector2Int[] nextPositions =
		{
			position + Vector2Int.right,
			position + Vector2Int.up,
			position + Vector2Int.left,
			position + Vector2Int.down
		};

		foreach (var pos in nextPositions)
		{
			GetScores(pos, building, range, checkedList, ref score);
		}
	}

	bool CheckBuilding(Building building, Building other, out float value)
	{
		value = 0;
		if (building == null || other == null)
			return false;
		
		value = BuildingRelationships.GetScoreBetweenBuildings(building.Type, other.Type);

		return true;
	}

	bool CheckCell(Building building, BaseCell cell, out float value)
	{
		value = 0;
		if (building == null || cell == null)
			return false;

		value = BuildingRelationships.GetScoreBetweenBuildingAndPlane(building.Type, cell.CellType);

		return true;
	}

	void PopMessage(float value, Vector3 position)
	{
		if (value > 0)
		{
			Popup.Pop("+ " + value, position, Color.green);
		}
		else if (value < 0)
		{
			Popup.Pop("- " + -value, position, Color.red);
		}
	}
}
