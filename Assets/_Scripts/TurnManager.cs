using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回合循环计数机
public class TurnManager : MonoBehaviour
{
	public static TurnManager Instance{ get; private set; }

	//构建Player，内部结构目前还不全,TODO
	private Queue<Player> queue = new Queue<Player> ();

	public Player CurrentPlayer{ get; private set; }

	//	bool isPlayerMoving;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
			
	}

	void Start ()
	{
		//Test
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 0, "刘玄德");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 1, "林志玲");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 2, "奥巴马");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 3, "苍井空");

		//角色初始化
		for (int i = 0; i < 4; i++) {
			Player p = new Player (i, PlayerPrefs.GetString (Constants.PLAYER_SAVE_NAME + i, "player" + i), Constants.DEFAULT_MONEY);
			//注册玩家事件
			p.PlayerActionEvent += GameManager.Instance.OnPlayerActionChanged;
			queue.Enqueue (p);
		}
		queue.TrimExcess ();
	}

	//	void Update ()
	//	{
	//
	//		if (CurrentPlayer != null) {
	////			print (mCurrentPlayer.mStatus);
	//			switch (CurrentPlayer.mStatus) {
	//			case PlayerStatus.Moving:
	////				txtCurrentPlayer.text = mCurrentPlayer.Name + ",正在行动";
	//				//玩家自由行动
	//				if (!isPlayerMoving) {//满足回合终止条件
	//					CurrentPlayer.mStatus = PlayerStatus.Waiting;
	//				}
	//				break;
	//			case PlayerStatus.Waiting:
	//				print (CurrentPlayer.Name + "结束了");
	//				NextTurn ();
	//				break;
	//			case PlayerStatus.Stop:
	//				
	//				break;
	//			case PlayerStatus.GameOver:
	//				
	//				break;
	//			}
	//		}
	//
	//	}

	/// <summary>
	/// 结束当前Player的行动，并且标记一个变量,
	/// </summary>
	public void StopPlayerMoving ()//目前逻辑有问题，后面改,TODO
	{
//		isPlayerMoving = false;

		NextTurn ();
	}

	//回合开始
	void NextTurn ()
	{
		//queue操作
		Player p = queue.Dequeue ();
		p.mStatus = PlayerStatus.Waiting;
		queue.Enqueue (p);
		CurrentPlayer = queue.Peek ();
		CurrentPlayer.mStatus = PlayerStatus.Moving;
//		isPlayerMoving = true;
	}
}
