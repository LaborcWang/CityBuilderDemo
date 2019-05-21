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
		Vector3 position = building.transform.position;
		List<Building> checkedList = new List<Building>();
		checkedList.Add(building);

		Instance.GetScores(position, building, 0, checkedList, ref score);
		Instance.totalScore += score;
		Instance.scoreText.text = Instance.totalScore.ToString();

		print(Instance.totalScore);
		return score;
	}

	void GetScores(Vector3 position, Building building, int range, List<Building> checkedList, ref float score)
	{
		if (range > building.Range)
			return;
		
		var colliders = Physics.OverlapBox(position, Vector3.one * gridCellSize * 0.5f);
		foreach (var col in colliders)
		{
			var other = col.GetComponent<Building>();
			if (other == null)
				continue;

			if (checkedList.Contains(other))
				break;

			checkedList.Add(other);
			var value = BuildingRelationships.GetScore(building.Type, other.Type);
			if (value > 0)
			{
				Popup.Pop("+ " + value, other.transform.position, Color.black);
			}
			else if (value < 0)
			{
				Popup.Pop("- " + -value, other.transform.position, Color.red);
			}
			score += value;
			break;
		}

		range++;

		Vector3[] nextPositions =
		{
			position + Vector3.right * gridCellSize,
			position + Vector3.forward * gridCellSize,
			position + Vector3.left * gridCellSize,
			position + Vector3.back * gridCellSize
		};

		foreach (var pos in nextPositions)
		{
			GetScores(pos, building, range, checkedList, ref score);
		}
	}
}
