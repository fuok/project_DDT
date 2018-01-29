using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//只有在初次启动游戏的时候，初始化一次数据库
public class LoadingManager1 : MonoBehaviour
{
	public Text txtProgressing;
	public Slider sliderProgress;

	void Awake ()
	{
		//初次游戏，必须先创建表内容（Girl和Ground），否则在Intro没数据可读
		if (PlayerPrefs.GetInt (Constants.FLAG_FIRST_GAME, 0) == 0) {
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

			PlayerPrefs.SetInt (Constants.FLAG_FIRST_GAME, 1);
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
		async = SceneManager.LoadSceneAsync ("[IntroScene]", LoadSceneMode.Single);
		async.allowSceneActivation = false;
		yield return async;//也就是说这里不写return async也完全没影响
	}

}
