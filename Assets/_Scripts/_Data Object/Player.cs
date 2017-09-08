using UnityEngine;
using System.Collections;

public enum PlayerStatus
{
	Moving,
	Waiting,
	Stop,
	GameOver
}

public class Player
{
	//------------ 数据结构部分 -----------------------------------

	private bool hasChanged;

	public int Index{ set; get; }

	public string Name{ set; get; }

	public int Money { set; get; }

	//-----------------------------------------------

	public PlayerStatus mStatus = PlayerStatus.Stop;

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
