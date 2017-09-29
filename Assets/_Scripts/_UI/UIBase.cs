using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	/// <summary>
	/// 传递可变参数。用于当前界面只做数据显示的情况
	/// </summary>
	/// <param name="args">Arguments.</param>
	public virtual void SetParams (params object[] args)
	{
	}

	/// <summary>
	/// 重载，传递引用参数。用于当前界面直接修改主缓存数据的情况
	/// </summary>
	/// <param name="arg">Argument.</param>
	public virtual void SetParams (ref object arg)
	{
	}

}
