using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuilding : MonoBehaviour
{
	[SerializeField] GameObject[] _buildings;
	[SerializeField] CubeCreateUI buildingButtonPrefab;
	[SerializeField] int numberOfBuilding;

	private void Start()
	{
		RandomlyFillBuildingList();
	}

	private void RandomlyFillBuildingList()
	{
		for (int i = 0; i < numberOfBuilding; i++)
		{
			var instance = Instantiate(buildingButtonPrefab.gameObject).GetComponent<CubeCreateUI>();
			instance.transform.SetParent(this.transform);
			instance.Initialize(_buildings[Random.Range(0, _buildings.Length)]);
		}
	}
}
