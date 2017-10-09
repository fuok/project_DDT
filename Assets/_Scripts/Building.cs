using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	public GameObject[] mPlayerBuildingList;

	public GameObject[] mNeutralBuildingList;

	public GameObject mCurrent;

	//	void Start ()
	//	{
	//
	//	}

	//	void Update ()
	//	{
	//
	//	}

	public void SetPlayerBuilding (int owner)//除了所有者，还要扩展建筑等级
	{
		if (mCurrent) {
			GameObject.Destroy (mCurrent);
		}
		GameObject newBuilding = GameObject.Instantiate (mPlayerBuildingList [owner], transform, false);
		mCurrent = newBuilding;
	}

	/// <summary>
	/// 摆放中立建筑，测试代码
	/// </summary>
	/// <param name="type">Type.</param>
	public void SetNeutralBuilding (int type)
	{
		if (mCurrent) {
			GameObject.Destroy (mCurrent);
		}

		switch (type) {
		case 1://临时处理,TODO
			GameObject newBuilding = GameObject.Instantiate (mNeutralBuildingList [type], transform, false);
			mCurrent = newBuilding;
			break;
		default:
			break;
		}

	}
}
