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

public class Player
{
	//------------ 数据结构 -----------------------------------

	private bool hasChanged;

	public int Index{ set; get; }

	public string Name{ set; get; }

	public int Money { set; get; }

	//------------- basic logic ----------------------------------

	public PlayerStatus mStatus = PlayerStatus.GameOver;

	public delegate void ActionHandler (string action);

	public event ActionHandler PlayerActionEvent;

	private delegate void DataHandler (bool param);

	private event DataHandler DataEvent;

	private Player ()
	{
		DataEvent += SetDataStatus;
	}

	public Player (int index, string name, int money) : this ()
	{
		this.Index = index;
		this.Name = name;
		this.Money = money;
	}

	private void SetDataStatus (bool hasChanged)
	{
		this.hasChanged = hasChanged;

		if (hasChanged) {
			Debug.Log ("数据变化");
		} else {
			Debug.Log ("数据已存");
		}
	}

	//----交互操作----------------------------------------

	public void SetAction (string action)
	{
		PlayerActionEvent (action);
	}

	public void XXX ()
	{
		
	}

	//----数据操作----------------------------------------

	//改变金钱数，其他操作以此类推
	public void AddMoney (int number)
	{
		Money += number;
		DataEvent (true);
	}

	//回合结束后保存数据
	public void SaveData ()
	{
		//持久化,TODO
		DataEvent (false);
	}

}
