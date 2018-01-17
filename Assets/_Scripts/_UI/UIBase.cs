using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有UI都是从预设克隆或是现成的游戏对象，也就是类型全都已经确定的，并不是用UIBase实例出的，所以这里指定继承UnitySigleton<UIBase>对实例不产生影响.
//但是这里会出现一个问题，克隆出来的实例虽然是子类型，可以使用子类型属性和方法，但是获取Instance却是通过UnitySigleton<UIBase>这个类型获取的基类Instance.
//所以没有办法区分具体子类型，也就是UnitySigleton<UIBase>只能拥有一个单例。界面切换时必须销毁前一界面，常驻界面也不能继承UIBase
public class UIBase : UnitySigleton<UIBase>
{
	public delegate void ConfirmDelegate ();

	void Start ()
	{
	}

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
