#if UNITY_ANDROID && !UNITY_EDITOR
#define ANDROID
#endif

#if UNITY_IPHONE && !UNITY_EDITOR
#define IPHONE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

	public static bool FromBeginning = true;
	//控制玩家数量，如果是再开的，就不会给赋值，所以游戏过程中不要访问.
	//而且玩家数并不等于游戏中的玩家数，游戏中的玩家可以出局，一旦出局玩家数量减少但index不变，这里index就不等于Number
	public static int PlayerNumber;

	public const int DEFAULT_MONEY = 10000;
	public const int DEFAULT_HEALTH = 100;

	//沙盒目录
	#if ANDROID || IPHONE
	public static string SAVE_PATH = Application.persistentDataPath + "/";
	#else
	public static string SAVE_PATH = Application.dataPath + "/_save/";
	#endif

	//默认图片保存位置
	public const string DEFAULT_GIRL_PORTRAIT_PATH = "Girl Image Default/girl_{0}_portrait_default";
	public const string DEFAULT_GIRL_HSCENE_1_PATH = "Girl Image Default/girl_{0}_hscene1_default";
	public const string DEFAULT_GIRL_HSCENE_2_PATH = "Girl Image Default/girl_{0}_hscene2_default";
	//自定义图片保存位置
	public const string CUSTOM_GIRL_PORTRAIT_PATH = "girl_{0}_portrait_custom.png";
	public const string CUSTOM_GIRL_HSCENE_1_PATH = "girl_{0}_hscene1_custom.png";
	public const string CUSTOM_GIRL_HSCENE_2_PATH = "girl_{0}_hscene2_custom.png";

	//数据库名
	public const string DbName = "ddt.db";
	//表名
	public const string TableNamePlayer = "save1player";
	public const string TableNameGirl = "save1girl";
	public const string TableNameGround = "save1ground";
	//数据库版本号
	public const int dataBaseVersion = 1;
	//数据库地址(StandAlone),//注意不是在根目录下,Application.dataPath会读取./assets/下面的
	public static string dbPath = "data source=" + Application.dataPath + "/" + DbName;
	//数据库地址(Android)
	public static string dbPathAndroid = "URI=file:" + Application.persistentDataPath + "/" + DbName;
	//数据库地址(IOS)
	public static string dbPathIos = @"data source=" + Application.persistentDataPath + "/" + DbName;

	//数据库地址(已经写好数据直接下载，应该也是Android的格式)
	public static string dbPathStreamIn = "URI=file:" + Application.streamingAssetsPath + "/" + DbName;


	//------------- PlayerPrefabs Key ----------------------
	public const string FLAG_FIRST_GAME = "flag_first_game";
	public const string FLAG_CUSTOM_TOGGLE = "flag_custom_toggle";
	//TODO
	public const string DATABASE_VERSION = "database version";
	//TODO
	public const string PLAYER_SAVE_NAME = "player_save_name_";
	//TODO
	public const string GIRL_SAVE_NAME = "girl_save_name_";
	//记录游戏回合数,全部玩家行动结束后为一回合结束,游戏开始或结束要重置
	public const string GAME_ROUND_NUMBER = "game_round_number";
	//记录当前行动的玩家
	public const string CURRENT_PALYER_INDEX = "current_player_index";
	//自定义女生名字的前缀
	public const string CUSTOM_GIRL_NAME = "c_g_n_{0}";

	//------------- Action Key ----------------------
	public const string ACTION_START_TURN_IN = "action_start_turn_in";
	public const string ACTION_START_TURN_OUT = "action_start_turn_out";

	public const string ACTION_SHOW_MESSAGE_IN = "action_show_message_in";
	public const string ACTION_SHOW_MESSAGE_OUT = "action_show_message_out";

	public const string ACTION_START_MOVE = "action_start_move";
	public const string ACTION_STOP_MOVE = "action_stop_move";

	public const string ACTION_ARRIVE_GROUND = "action_arrive_ground";

	public const string ACTION_MEET_GIRL = "action_meet_girl";
	public const string ACTION_MEET_GIRL_YES = "action_meet_girl_yes";
	public const string ACTION_MEET_GIRL_NO = "action_meet_girl_no";

	public const string ACTION_BUY_GROUND = "action_buy_ground";
	public const string ACTION_BUY_GROUND_YES = "action_buy_ground_yes";
	public const string ACTION_BUY_GROUND_NO = "action_buy_ground_no";
	public const string ACTION_BUY_GROUND_FAILED = "action_buy_ground_no_money";
	public const string ACTION_BUY_GROUND_FAILED_CONFIRM = "action_buy_ground_no_money_confirm";
	public const string ACTION_PAY_TOLL = "action_pay_toll";
	public const string ACTION_PAY_TOLL_CONFIRM = "action_pay_toll_confirm";
	public const string ACTION_BUY_DRUG = "action_buy_drug";
	public const string ACTION_BUY_DRUG_CONFIRM = "action_buy_drug_confirm";
	//	public const string ACTION_BREAKDOWN = "action_breakdown";
	//	public const string ACTION_BREAKDOWN_CONFIRM = "action_breakdown_confirm";
	public const string ACTION_END_TURN = "action_end_turn";
	public const string ACTION_END_TURN_CONFIRM = "action_end_turn_confirm";
	public const string ACTION_GAME_OVER = "action_game_over";

	//------------- Action Key 道具----------------------

	public const string ACTIVATE_PACKAGE_0 = "activate_package_0";
	public const string ACTIVATE_PACKAGE_1 = "activate_package_1";
	public const string ACTIVATE_PACKAGE_2 = "activate_package_2";
	public const string ACTIVATE_PACKAGE_3 = "activate_package_3";
	public const string ACTIVATE_PACKAGE_4 = "activate_package_4";
	public const string ACTIVATE_PACKAGE_5 = "activate_package_5";

	public const string USE_ITEM_1 = "item_1";
	public const string USE_ITEM_2 = "item_2";
	public const string USE_ITEM_3 = "item_3";
	public const string USE_ITEM_4 = "item_4";
	public const string USE_ITEM_5 = "item_5";
	public const string USE_ITEM_6 = "item_6";
	public const string USE_ITEM_7 = "item_7";
	public const string USE_ITEM_8 = "item_8";
	public const string USE_ITEM_9 = "item_9";
	public const string USE_ITEM_10 = "item_10";
}
