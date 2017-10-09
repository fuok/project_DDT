using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主缓存数据，同时也是需要持久化的数据
/// </summary>
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

	protected virtual void DataChange (bool hasChange)
	{
		DataEvent (hasChange);
	}

	//回合结束后调用此方法保存数据
	public void DataSave ()
	{
		if (hasChanged) {
			//持久化,TODO
					
			//通知
			DataChange (false);
		}
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
