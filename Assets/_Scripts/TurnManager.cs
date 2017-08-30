using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
	public static TurnManager instance;

	public Text txtCurrentPlayer;
	public Button btnTest;
	//
	Queue<Player> queue = new Queue<Player> ();
	Player mCurrentPlayer;
	bool isPlayerMoving;

	void Start ()
	{

		btnTest.onClick.AddListener (StopPlayerMoving);

		for (int i = 0; i < 4; i++) {
			Player p = new Player ();
			p.Name = "player" + i;
			print ("add player " + i);
			queue.Enqueue (p);
		}
		queue.TrimExcess ();

		NextTurn ();
	}

	void Update ()
	{

		if (mCurrentPlayer != null) {
//			print (mCurrentPlayer.mStatus);
			switch (mCurrentPlayer.mStatus) {
			case PlayerStatus.Moving:
				txtCurrentPlayer.text = mCurrentPlayer.Name + ",正在行动";
				//玩家自由行动
				if (!isPlayerMoving) {//满足回合终止条件
					mCurrentPlayer.mStatus = PlayerStatus.Waiting;
				}
				break;
			case PlayerStatus.Waiting:
				print (mCurrentPlayer.Name + "结束了");
				NextTurn ();
				break;
			case PlayerStatus.Stop:
				
				break;
			case PlayerStatus.GameOver:
				
				break;
			}
		}

	}

	void StopPlayerMoving ()
	{
		isPlayerMoving = false;
	}

	void NextTurn ()
	{
		//queue操作

		Player p = queue.Dequeue ();
		p.mStatus = PlayerStatus.Stop;
		queue.Enqueue (p);
		mCurrentPlayer = queue.Peek ();
		mCurrentPlayer.mStatus = PlayerStatus.Moving;
		isPlayerMoving = true;
	}
}
