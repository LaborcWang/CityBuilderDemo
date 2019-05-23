using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuilding : MonoBehaviour
{
	[SerializeField] GameObject[] _buildings;
	[SerializeField] CubeCreateUI buildingButtonPrefab;
	[SerializeField] int numberOfBuilding = 5;
	[SerializeField] int refreshAfterUsed = 2;

	int usedBuilding;

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
			instance.OnUsed += OnUsedHandler;
			instance.Initialize(_buildings[Random.Range(0, _buildings.Length)]);
		}
	}

	void OnUsedHandler()
	{
		usedBuilding++;
		if (usedBuilding < refreshAfterUsed)
			return;

		usedBuilding = 0;
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		RandomlyFillBuildingList();
	}
}
