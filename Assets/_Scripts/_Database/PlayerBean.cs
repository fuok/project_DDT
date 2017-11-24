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
		"[Index]", "Name", "Money", "Health", "Position", "item1", "item2", "item3", "item4", "item5", "item6"
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

	//Player包含：初始化表，保存表，读取表，逐条更新,清空表

	public void InitialTable (DbAccess db)
	{
		this.db = db;
		//创建数据库表，与字段
		db.CreateTable (Constants.TableNamePlayer, colName, colType, false);
	}

	//新游戏的时候一次性存入表
	public void SavePlayerList2DB (List<Player> playerList)
	{
		for (int i = 0; i < playerList.Count; i++) {
			
			db.InsertInto (Constants.TableNamePlayer, new object[] {
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
				playerList [i].GetItemList () [5]
			});
		}
	}

	//单条更新
	public void UpdatePlayer2DB (Player player)
	{
		db.UpdateInto (Constants.TableNamePlayer, new string[] {
			"[Index]",
			"Name",
			"Money",
			"Health",
			"Position",
			"item1",
			"item2",
			"item3",
			"item4",
			"item5",
			"item6"
		}, new object[] {
			player.Index,
			"'" + player.Name + "'",
			player.Money,
			player.Health,
			player.Position,
			player.GetItemList () [0],
			player.GetItemList () [1],
			player.GetItemList () [2],
			player.GetItemList () [3],
			player.GetItemList () [4],
			player.GetItemList () [5]
		}, "[index]", player.Index);
	}

	//	public void UpdatePlayerList2DB (List<Player> playerList)
	//	{
	//		for (int i = 0; i < playerList.Count; i++) {
	//			db.UpdateInto (Constants.TableNamePlayer, new string[] {
	//				"[index]",
	//				"name",
	//				"money",
	//				"health",
	//				"position",
	//				"item1",
	//				"item2",
	//				"item3",
	//				"item4",
	//				"item5",
	//				"item6"
	//			}, new object[] {
	//				playerList [i].Index,
	//				"'" + playerList [i].Name + "'",
	//				playerList [i].Money,
	//				playerList [i].Health,
	//				playerList [i].Position,
	//				playerList [i].GetItemList () [0],
	//				playerList [i].GetItemList () [1],
	//				playerList [i].GetItemList () [2],
	//				playerList [i].GetItemList () [3],
	//				playerList [i].GetItemList () [4],
	//				playerList [i].GetItemList () [5]
	//			}, "[index]", playerList [i].Index);
	//		}
	//	}

	public List<Player> GetPlayerListFromDB ()
	{
		sqReader = db.ReadFullTable (Constants.TableNamePlayer);
		List<Player> pList = new List<Player> ();
		while (sqReader.Read ()) {
			Player p = new Player (
				           index: sqReader.GetInt32 (sqReader.GetOrdinal ("Index")),//读取不需要加[]括号
				           name: sqReader.GetString (sqReader.GetOrdinal ("Name")),
				           money: sqReader.GetInt32 (sqReader.GetOrdinal ("Money")),
				           health: sqReader.GetInt32 (sqReader.GetOrdinal ("Health")),
				           position: sqReader.GetInt32 (sqReader.GetOrdinal ("Position")),
				           item1: sqReader.GetInt32 (sqReader.GetOrdinal ("item1")),
				           item2: sqReader.GetInt32 (sqReader.GetOrdinal ("item2")),
				           item3: sqReader.GetInt32 (sqReader.GetOrdinal ("item3")),
				           item4: sqReader.GetInt32 (sqReader.GetOrdinal ("item4")),
				           item5: sqReader.GetInt32 (sqReader.GetOrdinal ("item5")),
				           item6: sqReader.GetInt32 (sqReader.GetOrdinal ("item6"))
			           );
			pList.Add (p);

//			print (sqReader.GetInt32 (sqReader.GetOrdinal ("index")) + "," + sqReader.GetString (sqReader.GetOrdinal ("name")));
		}
		return pList;
	}

	public void DeletePlayerListFromDB ()
	{
		sqReader = db.DeleteContents (Constants.TableNamePlayer);
	}
}
