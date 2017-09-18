using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
	protected bool hasChanged;

	protected delegate void DataHandler (bool param);

	protected event DataHandler DataEvent;

	protected SaveData ()
	{
//		Debug.Log ("构造");
		DataEvent += OnDataEvent;//注册
	}

	protected virtual void DataChange (bool hasChange)
	{
		DataEvent (hasChange);
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
