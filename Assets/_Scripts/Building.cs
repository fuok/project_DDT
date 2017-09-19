using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	public GameObject[] mBuildingList;

	public GameObject mCurrent;

	//	void Start ()
	//	{
	//
	//	}

	//	void Update ()
	//	{
	//
	//	}

	public void SetBuilding (int owner)
	{
		if (mCurrent) {
			GameObject.Destroy (mCurrent);
		}
		GameObject newBuilding = GameObject.Instantiate (mBuildingList [owner], transform, false);
		mCurrent = newBuilding;
	}
}
