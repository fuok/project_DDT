using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class PlayerBean : MonoBehaviour
{
	public static PlayerBean Instance{ get; private set; }

	private DbAccess db;
	private SqliteDataReader sqReader;

	private string[] colName = new string[] {
		"[index]", "name", "money", "health", "position", "item1", "item2", "item3", "item4", "item5", "item6"
	};
	private string[] colType = new string[] {
		"integer", "text", "integer", "integer", "integer", "integer", "integer", "integer", "integer", "integer", "integer"
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
	//Player包含：初始化表，读取表，更新表

	public void InitialTable (DbAccess db)
	{
		this.db = db;
		//创建数据库表，与字段
		db.CreateTable (Constants.TableNamePlayer, colName, colType, false);
	}

	public void SavePlayerList2DB (List<Player> playerList)
	{
		for (int i = 0; i < playerList.Count; i++) {
			db.UpdateInto (Constants.TableNamePlayer, new string[] {
				"index",
				"name",
				"money",
				"health",
				"position",
				"item1",
				"item2",
				"item3",
				"item4",
				"item5",
				"item6"
			}, new object[] {
				playerList [i].Index,
				"'" + playerList [i].Name + "'",
				playerList [i].Money,
				playerList [i].Health,
				playerList [i].Position,
				playerList [i].GetItemList () [0],
				playerList [i].GetItemList () [1],
				playerList [i].GetItemList () [2],
				playerList [i].GetItemList () [3],
				playerList [i].GetItemList () [4],
				playerList [i].GetItemList () [5],
			}, "index", playerList [i].Index);
		}
//			db.InsertInto (Constants.tableNameSave, new object[] {
//				save.savId,
//				"'" + save.savParaId + "'",
//				"'" + save.savTime + "'",
//				"'" + save.savText + "'",
//				"'" + save.savImgPath + "'"
//			});
	}

	public List<Player> GetPlayerListFromDB (int id)
	{
		sqReader = db.SelectWhere (Constants.TableNamePlayer, colName, new string[]{ "savId" }, new string[]{ "=" }, new object[]{ id });
		List<Player> playList = new List<Player> ();
//		while (sqReader.Read ()) {
//			save = new GameSave (sqReader.GetInt32 (sqReader.GetOrdinal ("savId")),
//				sqReader.GetString (sqReader.GetOrdinal ("savParaId")),
//				sqReader.GetString (sqReader.GetOrdinal ("savTime")),
//				sqReader.GetString (sqReader.GetOrdinal ("savText")),
//				sqReader.GetString (sqReader.GetOrdinal ("savImgPath")));
//		}
		return playList;
	}
	
	//	public void DeleteGameSave (int id)
	//	{
	//		sqReader = db.Delete (Constants.tableNameSave, new string[]{ "savId" }, new object[]{ id });
	//	}
}
