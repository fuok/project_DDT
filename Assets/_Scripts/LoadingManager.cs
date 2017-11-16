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
			//回合数
			PlayerPrefs.SetInt (Constants.GAME_ROUND_NUMBER, 0);

			//Player

			//删除旧数据
			PlayerBean.Instance.DeletePlayerListFromDB ();
			//构造新数据
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
			//删除旧数据
			GirlBean.Instance.DeleteGirlListFromDB ();
			//构造新数据
			GirlBean.Instance.SaveGirlList2DB ();

			//Ground

			//删除旧数据
			GroundBean.Instance.DeleteGroundListFromDB ();
			//构造新数据
			GroundBean.Instance.SaveGroundList2DB ();
//			mGroundList = new List<Ground> {
//				new Ground{ Index = 0, Owner = -2, Type = 0 },//起点
//				new Ground{ Index = 1, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 2, Owner = -2, Type = 1  },//
//				new Ground{ Index = 3, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 4, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 5, Owner = -2, Type = 1 },//
//				new Ground{ Index = 6, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 7, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 8, Owner = -2, Type = 1  },//
//				new Ground{ Index = 9, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 10, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 11, Owner = -2, Type = 1  },//
//				new Ground{ Index = 12, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 13, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 14, Owner = -2, Type = 1 },//
//				new Ground{ Index = 15, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 16, Owner = -2, Type = 1 },//中位
//				new Ground{ Index = 17, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 18, Owner = -2, Type = 1  },//
//				new Ground{ Index = 19, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 20, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 21, Owner = -2, Type = 1 },//
//				new Ground{ Index = 22, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 23, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 24, Owner = -2, Type = 1 },//
//				new Ground{ Index = 25, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 26, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 27, Owner = -2, Type = 1  },//
//				new Ground{ Index = 28, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 29, Owner = -1, Level = 0, Price = 3000 },
//				new Ground{ Index = 30, Owner = -2, Type = 1 },//
//				new Ground{ Index = 31, Owner = -1, Level = 0, Price = 3000 }
//
//			};
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
