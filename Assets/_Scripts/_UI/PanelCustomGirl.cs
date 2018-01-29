using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCustomGirl : UIBase
{
	public static PanelCustomGirl Instance{ private set; get; }

	public Button btnBack;
	public Toggle toggleCustom;
	public RawImage rawPortrait;
	public Text txtIndex;
	public Text txtName;
	public Button btnEditName;
	public Button btnSetDefault;

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

//		btnEditName.onClick.AddListener (new UnityEngine.Events.UnityAction ());
		toggleCustom.onValueChanged.AddListener ((bool select) => {
			if (select) {
				print ("check");
				PlayerPrefs.SetInt (Constants.FLAG_CUSTOM_TOGGLE, 1);
				Refresh ();
			} else {
				print ("no check");
				PlayerPrefs.SetInt (Constants.FLAG_CUSTOM_TOGGLE, 0);
				Refresh ();
			}
		});

		Refresh ();
	}

	void Refresh ()
	{
		print ("Refresh ()");

//		rawPortrait.texture = Resources.Load<Texture> (string.Format ("Girl Image Default/girl_{0}_portrait_default", mGirlList [0].Index.ToString ()));
		CustomHelper.Instance.SetGirlPortrait (rawPortrait, mGirlList [0]);
		txtName.text = mGirlList [0].Name;
	}
}
