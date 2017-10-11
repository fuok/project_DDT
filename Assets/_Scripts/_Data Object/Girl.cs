using UnityEngine;
using System.Collections;

public class Girl:GameData
{
	//------------ 数据结构 -----------------------------------

	//序号
	public int Index{ set; get; }
	//名称
	public string Name{ set; get; }
	//薪水，每一轮给玩家的钱
	public int Salary{ set; get; }
	//简介
	public string Desc{ set; get; }
	//所属的玩家序号,无主为-1
	public int Owner{ set; get; }
	//所属的前一个玩家，女友离开后不会连续找同一玩家
	public int LastOwner{ set; get; }
	//标记女友曾经交往的玩家，区别见面时的对白
	public bool[] HistoryOwner = new bool[4];


	//------------- basic logic ----------------------------------

	public Girl () : base ()
	{
		
	}


	//----数据操作----------------------------------------

	public void SetGirl (int owner)
	{
		this.Owner = owner;

		//通知
		NotifyDataChanged (true);
	}

	protected override void NotifyDataChanged (bool hasChanged)
	{
		base.NotifyDataChanged (hasChanged);
	}
}
