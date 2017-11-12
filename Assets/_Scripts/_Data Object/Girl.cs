using UnityEngine;
using System.Collections;

public class Girl:GameData
{
	//------------ 数据结构 -----------------------------------

	//序号,0开始
	public int Index{ set; get; }
	//名称
	public string Name{ set; get; }
	//职业,没用的属性，只是用来显示
	public string Job{ set; get; }
	//典型
	public string Type{ set; get; }
	//性格,不明
	public string Character{ set; get; }
	//是否使用自定义内容
	public int UseCustom{ set; get; }

	//爱情度
	public int Love{ set; get; }
	//薪水，每一轮给玩家的钱
	public int Salary{ set; get; }
	//忍耐度
	public int Patient{ set; get; }
	//压力值
	public int Pressure{ set; get; }
	//稀有度，参考绿蓝紫金
	public int Grade{ set; get; }

	//所属的玩家序号,无主为-1
	public int Owner{ set; get; }
	//所属的前一个玩家，女友离开后不会连续找同一玩家
	public int LastOwner{ set; get; }
	//标记女友曾经交往的玩家，区别见面时的对白,0表示和该玩家没有交往过，1表示交往过。(低优先级)
	public int[] HistoryOwner = new int[4];


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

	public override	void DataSave ()
	{
		
	}

	protected override void NotifyDataChanged (bool hasChanged)
	{
		base.NotifyDataChanged (hasChanged);
	}
}
