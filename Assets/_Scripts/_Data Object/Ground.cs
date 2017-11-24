using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground:GameData
{
	//------------ 数据结构 -----------------------------------


	public int Index{ private set; get; }
	//所属的玩家id(中立：-2，空地：-1，玩家：0~3)
	public int Owner{ private set; get; }
	//地产等级
	public int Level{ private set; get; }
	//土地售价
	public int Price{ private set; get; }
	//中立地区的种类，各种中立建筑
	public int Type{ private set; get; }

	//------------- basic logic ----------------------------------

	public Ground (int index, int owner, int level, int price, int type) : base ()
	{
		this.Index = index;
		this.Owner = owner;
		this.Level = level;
		this.Price = price;
		this.Type = type;
	}


	//----数据操作----------------------------------------

	public void SetGround (int owner, int level = 0)
	{
		this.Owner = owner;
		this.Level = level;
		//通知
		NotifyDataChanged (true);
	}

	//----父类操作----------------------------------------

	public override	void DataSave ()
	{
		if (hasChanged) {
			//持久化
			GroundBean.Instance.UpdateGround2DB (this);
			//通知
			NotifyDataChanged (false);
		}
	}

	protected override void NotifyDataChanged (bool hasChanged)
	{
		base.NotifyDataChanged (hasChanged);
	}
}

public class GroundJson
{
	public List<Ground> groundList = new List<Ground> ();
}
