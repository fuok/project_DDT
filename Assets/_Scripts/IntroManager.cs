using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	public static IntroManager Instance{ get; private set; }

	public Button btnNewGame;
	public Button btnContinue;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		//测试数据的构建+持久化先放到这里,TODO

		//构造测试数据

		//Player
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 0, "刘玄德(红)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 1, "雷锋(黄)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 2, "奥巴马(蓝)");
		PlayerPrefs.SetString (Constants.PLAYER_SAVE_NAME + 3, "金三胖(绿)");

		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 0, "林志玲");
		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 1, "波多野结衣");
		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 2, "张雨桐");
		PlayerPrefs.SetString (Constants.GIRL_SAVE_NAME + 3, "王熙凤");

		for (int i = 0; i < 4; i++) {
			Player p = new Player (i, PlayerPrefs.GetString (Constants.PLAYER_SAVE_NAME + i, "player" + i)) {
				Money = Constants.DEFAULT_MONEY,
				Health = Constants.DEFAULT_HEALTH
			};
		}

	}

	// Use this for initialization
	void Start ()
	{
		btnNewGame.onClick.AddListener (() => {
			Constants.fromBeginning = true;
			SceneManager.LoadScene ("[PlayScene]");
		});
		btnContinue.onClick.AddListener (() => {
			Constants.fromBeginning = false;
			SceneManager.LoadScene ("[PlayScene]");
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
