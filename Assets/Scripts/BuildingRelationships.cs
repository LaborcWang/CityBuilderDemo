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

    public static float GetScore(BuildingType type1, BuildingType type2)
	{
		int index = (int)type1 * 3 + (int)type2;
		return scoreMap[index];
	}
}
