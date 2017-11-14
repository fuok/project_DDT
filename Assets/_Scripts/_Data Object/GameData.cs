using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主缓存数据，同时也是需要持久化的数据
/// </summary>
[System.Serializable]
public class GameData
{
	protected bool hasChanged;

	protected delegate void DataHandler (bool param);

	protected event DataHandler DataEvent;

	protected GameData ()
	{
//		Debug.Log ("构造");
		DataEvent += OnDataEvent;//注册
	}

	protected virtual void NotifyDataChanged (bool hasChange)
	{
		DataEvent (hasChange);
	}

	//回合结束后调用此方法的重写保存数据
	public virtual void DataSave ()
	{

	}

	private void OnDataEvent (bool hasChanged)
	{
		this.hasChanged = hasChanged;

		if (hasChanged) {
			Debug.Log ("数据变化");
		} else {
			Debug.Log ("数据已存");
		}
	}
}
