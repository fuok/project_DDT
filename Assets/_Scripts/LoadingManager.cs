using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
	public Text txtProgressing;
	public Slider sliderProgress;

	void Awake ()
	{
		//以下初始化游戏数据
		if (Constants.FromBeginning) {//根据读取记录情况判断新建游戏或者读取游戏

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

			//Girl
			GirlBean.Instance.DeleteGirlListFromDB ();
			GirlBean.Instance.SaveGirlList2DB ();
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
					progressFake += 0.01f;
				}

			} else {
				print ("good!!!");
				async.allowSceneActivation = true;
			}

			progressShow = (int)(progressFake * 100);
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
