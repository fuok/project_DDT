using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏中购买的道具.
/// </summary>
public class Item
{
	//目前看来，action和id都可以做唯一标识，id为0的空道具可以认为是不存在,TODO
	public int id;
	public int price;
	public string name;
	public string desc;
	public string action;

	private Item ()
	{
		
	}

	public Item (int id, int price, string name, string desc, string action)
	{
		this.id = id;
		this.price = price;
		this.name = name;
		this.desc = desc;
		this.action = action;
	}

}

public class Drug:Item
{
	public Drug (int id, int price, string name, string desc, string action) : base (id, price, name, desc, action)
	{
	}

	public static string GetName (int type)
	{
		string name = "";
		switch (type) {
		case 0:
			name = "空";
			break;
		case 1:
			name = "营养快线";
			break;
		case 2:
			name = "汇仁肾宝";
			break;
		default:
			break;
		}
		return name;
	}
}
