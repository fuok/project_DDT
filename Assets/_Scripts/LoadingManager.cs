using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{

	void Start ()
	{
		//根据读取记录情况判断新建游戏或者读取游戏
		if (Constants.FromBeginning) {

			//测试数据的构建+持久化先放到这里,TODO

			//删除旧数据
			PlayerBean.Instance.DeletePlayerListFromDB ();

			//构造测试数据

			//Player
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 0, "刘玄德(红)");
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 1, "雷锋(黄)");
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 2, "奥巴马(蓝)");
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 3, "金三胖(绿)");

			List<Player> pList = new List<Player> ();

			for (int i = 0; i < 4; i++) {
				Player p = new Player (i, PlayerPrefs.GetString (Constants.PLAYER_SAVE_NAME + i, "player" + i)) {
					Money = Constants.DEFAULT_MONEY,
					Health = Constants.DEFAULT_HEALTH,
					Position = 0
				};
				pList.Add (p);
			}
			PlayerBean.Instance.SavePlayerList2DB (pList);
		} else {
			
		}



		SceneManager.LoadScene ("[PlayScene]");
	}
	
	// Update is called once per frame
	//	void Update ()
	//	{
	//
	//	}
}
