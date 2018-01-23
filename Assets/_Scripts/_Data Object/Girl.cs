using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Girl:GameData
{
	//------------ 数据结构 -----------------------------------

	//序号,0开始
	public int Index{ private set; get; }
	//名称
	public string Name{ private set; get; }
	//职业,没用的属性，只是用来显示
	public string Job{ private set; get; }
	//典型
	public string Type{ private set; get; }
	//性格,不明
	public string Character{ private set; get; }
	//是否使用自定义内容
	public int UseCustom{ private set; get; }

	//爱情度
	public int Love{ private set; get; }
	//薪水，每一轮给玩家的钱
	public int Salary{ private set; get; }
	//忍耐度
	public int Patient{ private set; get; }
	//压力值
	public int Pressure{ private set; get; }
	//稀有度，参考绿蓝紫金
	public int Grade{ private set; get; }

	//所属的玩家序号,无主为-1
	public int Owner{ private set; get; }
	//所属的前一个玩家，女友离开后不会连续找同一玩家
	public int LastOwner{ private set; get; }
	//标记女友曾经交往的玩家，区别见面时的对白,0表示和该玩家没有交往过，1表示交往过。(低优先级)
	public int[] HistoryOwner;


	//------------- basic logic ----------------------------------

	public Girl (int index, string name, string job, string type, string character, int useCustom, int love, int salary, int patient, int pressure, int grade, int owner, int lastOwner, int historyOwner1, int historyOwner2, int historyOwner3, int historyOwner4) : base ()
	{
		this.Index = index;
		this.Name = name;
		this.Job = job;
		this.Type = type;
		this.Character = character;
		this.UseCustom = useCustom;
		this.Love = love;
		this.Salary = salary;
		this.Patient = patient;
		this.Pressure = pressure;
		this.Grade = grade;
		this.Owner = owner;
		this.LastOwner = lastOwner;
		this.HistoryOwner = new int[]{ historyOwner1, historyOwner2, historyOwner3, historyOwner4 };
	}


	//----数据操作----------------------------------------

	public void SetOwner (int owner)
	{
		this.Owner = owner;

		//通知
		NotifyDataChanged (true);
	}

	public void SetPressure (int value)
	{
		this.Pressure = value;

		NotifyDataChanged (true);
	}

	//----父类操作----------------------------------------

	public override	void DataSave ()
	{
		if (hasChanged) {
			//持久化
			GirlBean.Instance.UpdateGirl2DB (this);
			//通知
			NotifyDataChanged (false);
		}
	}

	protected override void NotifyDataChanged (bool hasChanged)
	{
		base.NotifyDataChanged (hasChanged);
	}
}

public class GirlJson
{
	public List<Girl> girlList = new List<Girl> ();
}
