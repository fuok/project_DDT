using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Reflection;
using System;

public class PlayerBean : MonoBehaviour
{
	public static PlayerBean Instance{ get; private set; }

	private DbAccess db;
	private SqliteDataReader sqReader;

	private string[] colName = new string[] {
		"savId", "savParaId", "savTime", "savText", "savImgPath"
	};
	private string[] colType = new string[] {
		"integer", "text", "text", "text", "text"
	};

//	private string hhh = GetTest ();
//
//	private string GetTest ()
//	{
//		return "";
//	}

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	//-------------------------public function-------------

	public void InitPlayerBean (DbAccess db)
	{
//		ParameterInfo[]=new Player().get
		Type t = typeof(Player);
		MemberInfo[] info = t.GetMembers ();


		this.db = db;
		//创建数据库表，与字段
		db.CreateTable (Constants.TableNamePlayer, colName, colType, false);
	}

	public void AddPlayerList2DB (List<Player> playerList)
	{
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
