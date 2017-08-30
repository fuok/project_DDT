using UnityEngine;

//暂未使用
public static class GameEventManager
{
	public delegate void GameEvent ();

	//委托链，两个阶段要做的所有事情，分布在各个脚本中
	public static event GameEvent GameStart, GameOver;

	public static void TriggerGameStart ()
	{
		if (GameStart != null) {
			//			GameStart=TestTest;
			GameStart ();
		}
	}

	public static void TriggerGameOver ()
	{
		if (GameOver != null) {
			GameOver ();
		}
	}

	public static void TestTest ()
	{
		Debug.Log ("GameStart");
	}
}