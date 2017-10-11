using UnityEngine;
using System.Collections;

public class Ground:GameData
{
	//------------ 数据结构 -----------------------------------


	public int Index{ set; get; }
	//所属的玩家id(中立：-2，空地：-1，玩家：0~3)
	public int Owner{ set; get; }

	public int Level{ set; get; }

	public int Price{ set; get; }
	//中立地区的种类，各种中立建筑
	public int Type{ set; get; }

	//------------- basic logic ----------------------------------

	public Ground () : base ()
	{

	}


	//----数据操作----------------------------------------

	public void SetGround (int owner, int level = 0)
	{
		this.Owner = owner;
		this.Level = level;
		//通知
		NotifyDataChanged (true);
	}

	protected override void NotifyDataChanged (bool hasChanged)
	{
		base.NotifyDataChanged (hasChanged);
	}
}
