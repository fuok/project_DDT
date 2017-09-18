using UnityEngine;
using System.Collections;

public enum PlayerStatus
{
	//角色行动中
	Moving,
	//角色到达目的地
	Arrived,
	//等待其他角色行动中
	Waiting,
	//角色出局
	GameOver
}

public class Player:SaveData
{
	//------------ 数据结构 -----------------------------------


	public int Index{ set; get; }

	public string Name{ set; get; }

	public int Money { set; get; }

	//------------- basic logic ----------------------------------

	public PlayerStatus mStatus = PlayerStatus.GameOver;

	public delegate void ActionHandler (string action);

	public event ActionHandler PlayerActionEvent;

	private Player () : base ()
	{
		
	}

	public Player (int index, string name, int money) : this ()
	{
		this.Index = index;
		this.Name = name;
		this.Money = money;
	}

	//----交互操作----------------------------------------

	public void SetAction (string action)
	{
		PlayerActionEvent (action);
	}

	//----数据操作----------------------------------------

	//改变金钱数，其他操作以此类推
	public void AddMoney (int number)
	{
		Money += number;
		//通知
		DataChange (true);
	}

	//回合结束后调用此方法保存数据
	public void SaveData ()
	{
		if (hasChanged) {
			//持久化,TODO
			
			//通知
			DataChange (false);
		}
	}

	protected override void DataChange (bool hasChanged)
	{
		base.DataChange (hasChanged);
	}

}
