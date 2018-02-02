using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCustomGirl : UIBase
{
	public static PanelCustomGirl Instance{ private set; get; }

	public Button btnBack;
	public Toggle toggleCustom;

	//列表item容器
	public GameObject[] girlItems;

	//文字输入框,先直接用，后面再考虑放进UIBase里
	public GameObject panelInput;
	public InputField inputCustomName;
	public Button btnCustomNameCancel, btnCustomNameConfirm;
	//输入自定义内容时，保存Playerprefab的key的前缀和序列，同一序列下可能有不同前缀（名字、描述。。。）
	private int selectIndex;
	private string selectKey;

	//数据库缓存
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
		//返回
		btnBack.onClick.AddListener (() => {
			UIManager.Instance.Close (this.GetType ());
		});
		//自定义开关
		toggleCustom.isOn = PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1 ? true : false;
		toggleCustom.onValueChanged.AddListener ((bool select) => {
			if (select) {
//				print ("check");
				PlayerPrefs.SetInt (Constants.FLAG_CUSTOM_TOGGLE, 1);
				RefreshAll ();
			} else {
//				print ("no check");
				PlayerPrefs.SetInt (Constants.FLAG_CUSTOM_TOGGLE, 0);
				RefreshAll ();
			}
		});

		//列表中的按钮
		for (int i = 0; i < girlItems.Length; i++) {
			int tempIndex = i;
			//头像
			girlItems [i].transform.Find ("Raw Portrait/Button Picker").GetComponent<Button> ().onClick.AddListener (() => {
				if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1) {
					GetComponent<ImagePicker> ().OpenImage (OnImageLoaded, tempIndex);
				}
			});
			//名字
			girlItems [i].transform.Find ("Button Edit Name").GetComponent<Button> ().onClick.AddListener (() => {
				if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1) {
					panelInput.SetActive (true);
					selectIndex = tempIndex;
					selectKey = string.Format (Constants.CUSTOM_GIRL_NAME, selectIndex.ToString ());
				}
			});
			//复位
			girlItems [i].transform.Find ("Button Set Default").GetComponent<Button> ().onClick.AddListener (() => {
				if (PlayerPrefs.GetInt (Constants.FLAG_CUSTOM_TOGGLE, 0) == 1) {
					SetItemDefault (tempIndex);
				}
			});
				
		}

		//Input界面
		btnCustomNameCancel.onClick.AddListener (() => {
			inputCustomName.text = "";
			panelInput.SetActive (false);
		});
		btnCustomNameConfirm.onClick.AddListener (() => {
			if (!string.IsNullOrEmpty (inputCustomName.text)) {
				PlayerPrefs.SetString (selectKey, inputCustomName.text);
				Refresh (selectIndex);
			}
			inputCustomName.text = "";//顺序别颠倒
			panelInput.SetActive (false);
		});

		RefreshAll ();
	}

	/// <summary>
	/// 刷新特定item，要把所有可改的内容都放进来
	/// </summary>
	/// <param name="index">Index.</param>
	void Refresh (int index)
	{
//		print ("Refresh:" + index);
		CustomHelper.Instance.SetGirlPortrait (girlItems [index].transform.Find ("Raw Portrait").GetComponent<RawImage> (), mGirlList [index]);
		girlItems [index].transform.Find ("Text Index").GetComponent<Text> ().text = (index + 1).ToString ();
		girlItems [index].transform.Find ("Text Name").GetComponent<Text> ().text = CustomHelper.Instance.GetGirlName (mGirlList [index]);
	}

	void RefreshAll ()
	{
		for (int i = 0; i < girlItems.Length; i++) {
			Refresh (i);
		}
	}

	/// <summary>
	/// 导入图片后保存本地
	/// </summary>
	/// <param name="tex">Tex.</param>
	void OnImageLoaded (Texture2D tex, int index)
	{
		string localPath = Constants.SAVE_PATH + string.Format (Constants.CUSTOM_GIRL_PORTRAIT_PATH, index);
		Utils.WriteTexture2D2File (tex, localPath);
		Refresh (index);
	}

	/// <summary>
	/// 清除本地保存的图片
	/// </summary>
	/// <param name="index">Index.</param>
	void SetItemDefault (int index)
	{
		string localPath = Constants.SAVE_PATH + string.Format (Constants.CUSTOM_GIRL_PORTRAIT_PATH, index.ToString ());
		Utils.DeleteFile (localPath);
		Refresh (index);
	}
}
