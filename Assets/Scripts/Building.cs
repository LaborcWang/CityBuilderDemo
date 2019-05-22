using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
	Resident,
	Industrial,
	Commercial
}


public class Building : MonoBehaviour
{
	[SerializeField] int range;
	[SerializeField] BuildingType type;
	public int Range => range;
	public BuildingType Type => type;

}
