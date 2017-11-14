using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回合循环计数机
public class TurnManager : MonoBehaviour
{
	public static TurnManager Instance{ get; private set; }

	//储存Player的Queue
	private Queue<Player> queue = new Queue<Player> ();

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
			
	}

	//	void Start ()
	//	{
	//
	//	}

	public void SetPlayerQueue (ref List<Player> playerList)
	{
		for (int i = 0; i < playerList.Count; i++) {
			queue.Enqueue (playerList [i]);
		}
		queue.TrimExcess ();
	}

	public Player GetCurrentPlayer ()
	{
		return queue.Peek ();
	}

	/// <summary>
	/// 结束当前Player的行动，并且标记一个变量,
	/// </summary>
	public void StopPlayerMoving ()
	{
		NextTurn ();
	}

	//回合开始
	void NextTurn ()
	{
		//queue操作
		Player p = queue.Dequeue ();
		p.mStatus = PlayerStatus.Waiting;
		queue.Enqueue (p);
		queue.Peek ().mStatus = PlayerStatus.Moving;
		print ("取出了：" + queue.Peek ().Index);
	}
}
