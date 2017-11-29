using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
	public delegate void ConfirmDelegate ();

	void Start ()
	{
		
	}

	//	void Update ()
	//	{
	//
	//	}

	/// <summary>
	/// Sets the parameters.
	/// </summary>
	/// <param name="arg">引用参数。用于当前界面直接修改主缓存数据的情况.因为引用参数必须是指定类型不能用object装箱，所以这里使用泛型.</param>
	/// <param name="args">可变参数。用于只修改界面显示的情况.</param>
	public virtual void SetParams<T> (ref T arg, params object[] args)
	{
	}

	public virtual void SetParams (ConfirmDelegate func, params object[] args)
	{
	}

	public virtual void SetParams (params object[] args)
	{
	}

}
