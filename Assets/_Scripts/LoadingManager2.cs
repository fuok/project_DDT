using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager2 : MonoBehaviour
{
	public Text txtProgressing;
	public Slider sliderProgress;

	void Awake ()
	{
		//以下初始化游戏数据
		if (Constants.FromBeginning) {//根据读取记录情况判断新建游戏或者读取游戏
			//-------------------回合数
			PlayerPrefs.SetInt (Constants.GAME_ROUND_NUMBER, 0);

			//-------------------Player

			//删除旧数据
			PlayerBean.Instance.DeletePlayerListFromDB ();
			//构造新数据
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 0, "刘玄德(红)");
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 1, "雷锋(黄)");
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 2, "奥巴马(蓝)");
			PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 3, "金三胖(绿)");

			List<Player> pList = new List<Player> ();

			for (int i = 0; i < Constants.PlayerNumber; i++) {//控制玩家数量
				Player p = new Player (
					           index: i,
					           name: PlayerPrefs.GetString (Constants.PLAYER_SAVE_NAME + i, "player" + i),
					           money: Constants.DEFAULT_MONEY,
					           health: Constants.DEFAULT_HEALTH,
					           position: 0,
					           item1: 0,
					           item2: 0,
					           item3: 0,
					           item4: 0,
					           item5: 0,
					           item6: 0
				           );
				pList.Add (p);
			}
			PlayerBean.Instance.SavePlayerList2DB (pList);

			//-------------------Girl
			//删除旧数据
			GirlBean.Instance.DeleteGirlListFromDB ();
			//构造新数据
			GirlBean.Instance.SaveGirlList2DB ();

			//-------------------Ground

			//删除旧数据
			GroundBean.Instance.DeleteGroundListFromDB ();
			//构造新数据
			GroundBean.Instance.SaveGroundList2DB ();
		} else {

		}
	}

	void Start ()
	{
		StartCoroutine (StartLoading ());
	}

	//-------------------------

	private AsyncOperation async;
	private float progressTrue = 0;
	private float progressFake = 0;
	private int progressShow = 0;

	void Update ()
	{
		if (async != null) {

			if (progressFake < 1.0f) {
				progressTrue = async.progress;
				print ("async.progress=" + progressTrue);

				if (progressFake < progressTrue || progressFake >= 0.9f) {
					#if UNITY_EDITOR
					progressFake += 0.02f;//速度设得太快导致出现了数据库写入不全的情况，有没有办法避免,TODO
					#else
					progressFake += 0.01f;
					#endif
				}

			} else {
				print ("loading success");
				async.allowSceneActivation = true;
			}

			progressShow = (int)(Mathf.Clamp01 (progressFake) * 100);//防止数字爆表
			txtProgressing.text = progressShow.ToString ();
			sliderProgress.value = progressFake;
		}
	}

	private IEnumerator StartLoading ()
	{
		yield return new WaitForEndOfFrame ();
		async = SceneManager.LoadSceneAsync ("[PlayScene]", LoadSceneMode.Single);
		async.allowSceneActivation = false;
		yield return async;//也就是说这里不写return async也完全没影响
	}

}
