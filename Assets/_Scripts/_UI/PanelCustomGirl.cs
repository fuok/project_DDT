using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCustomGirl : UIBase
{
	public static PanelCustomGirl Instance{ private set; get; }

	public Button btnBack;

	private List<Girl> mGirlList = new List<Girl> ();

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		//读取Girl
		mGirlList = GirlBean.Instance.GetGirlListFromDB ();
		print ("testtest:" + mGirlList.Count);
		for (int i = 0; i < mGirlList.Count; i++) {
			print (mGirlList [i].Name);
		}
	}

	void Start ()
	{
		btnBack.onClick.AddListener (() => {
			UIManager.Instance.Close (this.GetType ());
		});
	}

	void Update ()
	{
		
	}
}
