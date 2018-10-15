using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using MathExt;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class RandomLocation : MonoBehaviour
{
	public RandomLocationData[] RandomLocationDatas;


	[SerializeField] private GameObject _objectToSpawn;
	[SerializeField] private int _amountOfObjects;
	[SerializeField] private GameObject _parent;



	// Use this for initialization
	void Start ()
	{
		SpawnObjects();
	}

	private void SpawnObjects()
	{
		for (int i = 0; i < _amountOfObjects; i++)
		{
			var locations = RandomLocationDatas.GetRandom_Array();
			Vector2 sorted;
			float yValue = locations.One.position.y;
			float xValue = locations.One.position.x;
			float zValue = locations.One.position.z;
			if(locations.OnX)
			{

				sorted = SortLargest(locations.One.position.x, locations.Two.position.x);
				xValue  = Random.Range(sorted.x, sorted.y);
			}
			else
			{
				sorted = SortLargest(locations.One.position.z, locations.Two.position.z);
				zValue = Random.Range(sorted.x, sorted.y);
			}

			Instantiate(_objectToSpawn, new Vector3(xValue, yValue, zValue), Quaternion.identity, _parent.transform);
		}
	}

	private Vector2 SortLargest(float one, float two)
	{
		float x = Mathf.Min(one, two);
		float y = Mathf.Max(one, two);
		return new Vector2(x,y);
	}


}


[Serializable]
public struct RandomLocationData
{
	public bool OnX;
	public Transform One;
	public Transform Two;

}
