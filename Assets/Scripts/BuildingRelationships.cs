using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRelationships : MonoBehaviour
{
	static float[] scoreMap =
	{
		//Resident, Industrial, commercial
		 1, -1,  1,
		-1,  0,  1,
		 1,  1,  0
	};

	static float[] planeMap =
	{
		//Resident, Industrial, commercial
		 0,  0,  0, // Normal plane
		 0,  1,  1, // Road plane
		 1,  1,  0  // River plane
	};

    public static float GetScoreBetweenBuildings(BuildingType type1, BuildingType type2)
	{
		int index = (int)type1 * 3 + (int)type2;
		return scoreMap[index];
	}

	public static float GetScoreBetweenBuildingAndPlane(BuildingType type1, CellType type2)
	{
		int index = (int)type2 * 3 + (int)type1;
		return planeMap[index];
	}
}
