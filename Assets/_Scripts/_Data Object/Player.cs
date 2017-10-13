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

public class Player:GameData
{
	//------------ 数据结构 -----------------------------------

	//前面两项作为必填项，后面用初始化器
	public int Index{ private set; get; }

	public string Name{ private set; get; }

	public int Money { set; get; }

	public int Health{ set; get; }

	//当游戏中断，记录玩家的位置
	public int Position { set; get; }

	private int[] DrugList = new int[6];

	//------------- basic logic ----------------------------------

	public PlayerStatus mStatus = PlayerStatus.GameOver;

	public delegate void ActionHandler (string action);

	public event ActionHandler PlayerActionEvent;

	private Player () : base ()
	{
		
	}

	public Player (int index, string name) : this ()
	{
		this.Index = index;
		this.Name = name;
	}

	//----交互操作----------------------------------------

	public void SetAction (string action)
	{
		PlayerActionEvent (action);
	}

	//----数据操作----------------------------------------

	//改变金钱数，其他操作以此类推
	public int AddMoney (int number)
	{
		Money += number;
		//通知
		NotifyDataChanged (true);

		return Money;
	}

	//体力
	public int AddHealth (int number)
	{
		Health += number;
		//通知
		NotifyDataChanged (true);

		return Health;
	}

	public int[] GetItemList ()
	{
		return DrugList;
	}

	public bool IsSpaceAvailable ()
	{
		for (int i = 0; i < DrugList.Length; i++) {
			if (DrugList [i] == 0) {
				return true;
			}
		}
		return false;
	}

	public int[] AddItem (int type)
	{
		for (int i = 0; i < DrugList.Length; i++) {
			if (DrugList [i] == 0) {
				DrugList [i] = type;
				return DrugList;
			}
		}
		return DrugList;
	}

	//TODO
	public bool BuyItem (Item item)
	{

		return false;
	}

	public int[] UseItem (int type)
	{
		for (int i = 0; i < DrugList.Length; i++) {
			if (DrugList [i] == type) {
				DrugList [i] = 0;
				//TODO
				return DrugList;
			}
		}
		return DrugList;
	}

	protected override void NotifyDataChanged (bool hasChanged)
	{
		base.NotifyDataChanged (hasChanged);
	}

}
