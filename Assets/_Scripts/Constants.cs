using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

	public const int DEFAULT_MONEY = 10000;
	public const int DEFAULT_HEALTH = 100;


	//------------- PlayerPrefabs Key ----------------------

	//TODO
	public const string PLAYER_SAVE_NAME = "player_save_name_";
	//TODO
	public const string GIRL_SAVE_NAME = "girl_save_name_";
	//记录游戏回合数,全部玩家行动结束后为一回合结束,TODO,游戏开始或结束要重置
	public const string GAME_ROUND_NUMBER = "game_round_number";
	//记录当前行动的玩家
	public const string CURRENT_PALYER_INDEX = "current_player_index";



	//------------- Action Key ----------------------
	public const string ACTION_START_TURN_IN = "action_start_turn_in";
	public const string ACTION_START_TURN_OUT = "action_start_turn_out";

	public const string ACTION_ARRIVE_GROUND = "action_arrive_ground";

	public const string ACTION_MEET_GIRL = "action_meet_girl";
	public const string ACTION_MEET_GIRL_YES = "action_meet_girl_yes";
	public const string ACTION_MEET_GIRL_NO = "action_meet_girl_no";

	public const string ACTION_BUY_GROUND = "action_buy_ground";
	public const string ACTION_BUY_GROUND_YES = "action_buy_ground_yes";
	public const string ACTION_BUY_GROUND_NO = "action_buy_ground_no";
	public const string ACTION_BUY_GROUND_NO_MONEY = "action_buy_ground_no_money";
	public const string ACTION_BUY_GROUND_NO_MONEY_CONFIRM = "action_buy_ground_no_money_confirm";
	public const string ACTION_PAY_TOLL = "action_pay_toll";
	public const string ACTION_PAY_TOLL_CONFIRM = "action_pay_toll_confirm";
	public const string ACTION_BUY_DRUG = "action_buy_drug";
	public const string ACTION_BUY_DRUG_CONFIRM = "action_buy_drug_confirm";
	//	public const string ACTION_BREAKDOWN = "action_breakdown";
	//	public const string ACTION_BREAKDOWN_CONFIRM = "action_breakdown_confirm";
	public const string ACTION_END_TURN = "action_end_turn";
	public const string ACTION_END_TURN_CONFIRM = "action_end_turn_confirm";
	public const string ACTION_GAME_OVER = "action_game_over";
}
