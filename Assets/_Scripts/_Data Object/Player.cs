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

	//序号，0开始
	public int Index{ private set; get; }
	//名
	public string Name{ private set; get; }
	//钱数
	public int Money { private set; get; }
	//体力
	public int Health{ private set; get; }
	//当游戏中断，记录玩家的位置
	public int Position { private set; get; }
	//玩家是否破产
	public int Breakdown{ private set; get; }
	//玩家是否存在
	//	public int Exist{ private set; get; }
	//持有的道具列表，长度为6的数组，用不同数字代表道具编号，0表示空道具栏
	private int[] mDrugList;

	//------------- basic logic ----------------------------------

	public PlayerStatus mStatus = PlayerStatus.GameOver;

	public delegate void ActionHandler (string action);

	public event ActionHandler PlayerActionEvent;

	public Player (int index, string name, int money, int health, int position, int item1, int item2, int item3, int item4, int item5, int item6) : base ()
	{
		this.Index = index;
		this.Name = name;
		this.Money = money;
		this.Health = health;
		this.Position = position;
		this.mDrugList = new int[]{ item1, item2, item3, item4, item5, item6 };
	}

	//	public Player (int index, string name) : this ()
	//	{
	//		this.Index = index;
	//		this.Name = name;
	//	}

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

	//位置
	public int SetPosition (int positionIndex)
	{
		Position = positionIndex;
		//通知
		NotifyDataChanged (true);
		return Position;
	}

	public int[] GetItemList ()
	{
		return mDrugList;
	}

	public bool IsSpaceAvailable ()
	{
		for (int i = 0; i < mDrugList.Length; i++) {
			if (mDrugList [i] == 0) {
				return true;
			}
		}
		return false;
	}

	//不花钱
	public int[] AddItem (int type)
	{
		for (int i = 0; i < mDrugList.Length; i++) {
			if (mDrugList [i] == 0) {
				mDrugList [i] = type;
				return mDrugList;
			}
		}
		return mDrugList;
	}

	//花钱买,TODO
	public bool BuyItem (Item item)
	{

		return false;
	}

	/// <summary>
	/// 注意index是包内的序号，不是道具的种类
	/// </summary>
	/// <returns>The item.</returns>
	/// <param name="index">Index.</param>
	public int[] DeleteItem (int index)
	{
//		SetAction (Drug.GetDrug (mDrugList [index]).action);
		mDrugList [index] = 0;//将该位置上type改为0

		return mDrugList;
	}

	//----父类操作----------------------------------------

	protected override void NotifyDataChanged (bool hasChanged)
	{
		base.NotifyDataChanged (hasChanged);
	}

	public override	void DataSave ()
	{
		if (hasChanged) {
			//持久化
			PlayerBean.Instance.UpdatePlayer2DB (this);
			//通知
			NotifyDataChanged (false);
		}
	}

}
