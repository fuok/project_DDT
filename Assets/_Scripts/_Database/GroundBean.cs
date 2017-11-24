using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using Newtonsoft.Json;

public class GroundBean : MonoBehaviour
{
	public static GroundBean Instance{ get; private set; }

	private DbAccess db;
	private SqliteDataReader sqReader;
	private string tableName = Constants.TableNameGround;

	private string[] colName = new string[] {
		"[Index]", "Owner", "Level", "Price", "Type"
	};
	private string[] colType = new string[] {
		"integer", "integer", "integer", "integer", "integer"
	};

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	//-------------------------public function-------------

	//Ground包含：初始化表，反序列化，保存表，读取表，逐条更新,清空表

	public void InitialTable (DbAccess db)
	{
		this.db = db;
		//创建数据库表，与字段
		db.CreateTable (tableName, colName, colType, false);
	}

	//新游戏的时候一次性存入表
	public void SaveGroundList2DB ()
	{
		StartCoroutine (DoWriteJob (ReadGroundListFromJson ()));
	}

	IEnumerator DoWriteJob (List<Ground> gList)
	{
		List<Ground>.Enumerator ie = gList.GetEnumerator ();
		while (ie.MoveNext ()) {
			yield return new WaitForEndOfFrame ();//wait
			Ground ground = ie.Current as Ground;
			//			print ("保存:" + girl.Name);
			db.InsertInto (tableName, new object[] {
				ground.Index,
				ground.Owner,
				ground.Level,
				ground.Price,
				ground.Type
			});
		}

	}

	private List<Ground> ReadGroundListFromJson ()
	{
		TextAsset paraAsset = Resources.Load<TextAsset> ("json/ground json");
		print ("json:" + paraAsset.text);
		GroundJson mJson = JsonConvert.DeserializeObject<GroundJson> (paraAsset.text);
		Resources.UnloadUnusedAssets ();
		return mJson.groundList;
	}

	public List<Ground> GetGroundListFromDB ()
	{
		sqReader = db.ReadFullTable (tableName);
		List<Ground> pList = new List<Ground> ();
		while (sqReader.Read ()) {
			Ground g = new Ground (
				           index: sqReader.GetInt32 (sqReader.GetOrdinal ("Index")),//读取不需要加[]括号
				           owner: sqReader.GetInt32 (sqReader.GetOrdinal ("Owner")),
				           level: sqReader.GetInt32 (sqReader.GetOrdinal ("Level")),
				           price: sqReader.GetInt32 (sqReader.GetOrdinal ("Price")),
				           type: sqReader.GetInt32 (sqReader.GetOrdinal ("Type"))
			           );
			pList.Add (g);
			//			print ("读取:" + g.Name);
		}
		return pList;
	}

	//单条更新
	public void UpdateGround2DB (Ground ground)
	{
		db.UpdateInto (tableName, new string[] {
			"[Index]", "Owner", "Level", "Price", "Type"
		}, new object[] {
			ground.Index,
			ground.Owner,
			ground.Level,
			ground.Price,
			ground.Type
		}, "[Index]", ground.Index);
	}

	public void DeleteGroundListFromDB ()
	{
		sqReader = db.DeleteContents (tableName);
	}
}
