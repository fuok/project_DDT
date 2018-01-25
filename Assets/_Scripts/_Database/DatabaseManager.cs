#if UNITY_ANDROID && !UNITY_EDITOR
#define ANDROID
#endif

#if UNITY_IPHONE && !UNITY_EDITOR
#define IPHONE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
	public static DatabaseManager Instance{ get; private set; }

	private DbAccess db;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Start ()
	{
		StartDatabase ();
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy ()//认为游戏结束了
	{
		print ("OnDesrtoy");
		//关闭对象
		db.CloseSqlConnection ();
	}

	//-------------------------public function-------------

	public void StartDatabase ()
	{
		//读取/创建数据库,目前只有android和pc后需要加入ios
		#if ANDROID
		db = new DbAccess (Constants.dbPathAndroid);
		#elif IPHONE
		db = new DbAccess (Constants.dbPathIos);
		#else
		db = new DbAccess (Constants.dbPath);//数据库名//("Server=127.0.0.1;UserId=root;Password=;Database=li")
		#endif

		//检查数据库版本
		int version = PlayerPrefs.GetInt (Constants.DATABASE_VERSION, 0);//检查版本号
		print ("version=" + version);
		if (Constants.dataBaseVersion > version) {
			print ("database update");
			//升级para数据库
//			try {
//				db.DeleteTable (Constants.TableNamePlayer);//TODO
//			} catch (System.Exception ex) {
//
//			}
			PlayerPrefs.SetInt (Constants.DATABASE_VERSION, Constants.dataBaseVersion);
			//初始化bean
			PlayerBean.Instance.InitialTable (db);
			GirlBean.Instance.InitialTable (db);
			GroundBean.Instance.InitialTable (db);
		} else {
			print ("no need update");
			//初始化bean
			PlayerBean.Instance.InitialTable (db);
			GirlBean.Instance.InitialTable (db);
			GroundBean.Instance.InitialTable (db);
		}
	}

	/// <summary>
	/// 清除数据库，用于测试
	/// </summary>
	public void CleanDB ()
	{
		PlayerPrefs.DeleteKey (Constants.DATABASE_VERSION);
		db.DeleteTable (Constants.TableNamePlayer);
		db.DeleteTable (Constants.TableNameGirl);
		db.DeleteTable (Constants.TableNameGround);
	}
}
