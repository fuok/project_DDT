using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//本类型只负责建筑外观的表现，不关联其他逻辑
public class Building : MonoBehaviour
{

	//	public GameObject[] mPlayerBuildingList;

	//	public GameObject[] mNeutralBuildingList;

	public GameObject mCurrent;

	//	void Start ()
	//	{
	//
	//	}

	//	void Update ()
	//	{
	//
	//	}

	/// <summary>
	/// 摆放玩家建筑
	/// </summary>
	/// <param name="owner">Owner.</param>
	public void SetPlayerBuilding (int owner, int level)//除了所有者，还要扩展建筑等级
	{
		if (owner >= 0 && owner <= 3) {
			if (mCurrent) {
				GameObject.Destroy (mCurrent);
			}

			string path = string.Format ("prefabs/buildings/building_player_{0}_level_{1}", owner, level);
			print (path);

			GameObject newBuilding = GameObject.Instantiate (Resources.Load<GameObject> (path), transform.position, transform.rotation);
			mCurrent = newBuilding;
		}
	}

	/// <summary>
	/// 摆放中立建筑
	/// </summary>
	/// <param name="type">Type.</param>
	public void SetNeutralBuilding (int type)
	{
		if (type != 0) {
			if (mCurrent) {
				GameObject.Destroy (mCurrent);
			}
			
			string path = string.Format ("prefabs/buildings/building_neutral_type_{0}", type);
//			print (path);
			
			GameObject newBuilding = GameObject.Instantiate (Resources.Load<GameObject> (path), transform.position, transform.rotation);
			mCurrent = newBuilding;
		} else {
			print ("中立建筑不存在");
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawSphere (transform.position, 2f);
	}
}
