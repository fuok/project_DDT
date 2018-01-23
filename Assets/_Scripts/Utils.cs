using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

	/// <summary>
	/// 获取制定随机事件的发生结果
	/// </summary>
	/// <returns><c>true</c>, if random result was gotten, <c>false</c> otherwise.</returns>
	/// <param name="percent">获取事件的概率(百分比)</param>
	private static bool GetRandomResult (float percent)
	{
		//Random.value方法返回一个0—1的随机数
		float randomPoint = Random.value * 100;
		Debug.Log ("事件概率：" + percent + ",随机得到：" + randomPoint);
		return percent > randomPoint;
	}

	//游戏事件随机函数,不同函数传入的概率不一样

	//遇到新女友
	public static bool RandomMeetGirl ()
	{
		return GetRandomResult (100f);
	}
}
