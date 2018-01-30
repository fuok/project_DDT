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
	public Button btnPickImage;
	public Text txtIndex;
	public Text txtName;
	public Button btnEditName;
	public Button btnSetDefault;

	private List<Girl> mGirlList = new List<Girl> ();
	//每次选择时，标记要自定义的照片位置
	private int selectedIndex;

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

		btnPickImage.onClick.AddListener (() => {
			if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1) {
				selectedIndex = mGirlList [0].Index;
				GetComponent<ImagePicker> ().OpenImage (OnImageLoaded);
			}
		});

//		btnEditName.onClick.AddListener (new UnityEngine.Events.UnityAction ());

		btnSetDefault.onClick.AddListener (() => {
			SetItemDefault (mGirlList [0].Index);
		});

		toggleCustom.isOn = PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1 ? true : false;
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
		txtIndex.text = (mGirlList [0].Index + 1).ToString ();
		txtName.text = mGirlList [0].Name;
	}

	void OnImageLoaded (Texture2D tex)
	{
		string localPath = Constants.SAVE_PATH + string.Format ("girl_{0}_portrait_custom.png", selectedIndex.ToString ());
		Utils.WriteTexture2D2File (tex, localPath);
		Refresh ();
	}

	void SetItemDefault (int index)
	{
		string localPath = Constants.SAVE_PATH + string.Format ("girl_{0}_portrait_custom.png", index.ToString ());
		Utils.DeleteFile (localPath);
		Refresh ();
	}
}
