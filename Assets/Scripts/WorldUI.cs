using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityUtility;

public class WorldUI : MonoSingleton<WorldUI>
{
	public static void AddedUIElement(RectTransform obj)
	{
		obj.SetParent(Instance.transform);
		obj.localScale = Vector3.one;
		obj.localRotation = Quaternion.identity;
	}

	public static void MoveUIByPixelPosition(Transform ui, Vector3 pixelPosition)
	{
		pixelPosition.z = (Instance.transform.position - Camera.main.transform.position).z;
		var position = Camera.main.ScreenToWorldPoint(pixelPosition);

		ui.transform.position = position;
	}

	public static void MoveUIByWorldPosition(Transform ui, Vector3 worldPosition)
	{
		MoveUIByPixelPosition(ui, Camera.main.WorldToScreenPoint(worldPosition));
	}
}
