using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum 着法状态
{
    到红方走,
    到黑方走,
}

public delegate void ChessReciprocalStateEventHandler();

public class GameController : MonoBehaviour
{
    public static 着法状态 whoWalk;
    public static event ChessReciprocalStateEventHandler ResetChessReciprocalStateEvent;
    /// <summary>
    /// 记录每走一步的所有棋局信息
    /// </summary>
    public static List<Dictionary<Vector2, GameObject>> maps;

    private CreateManager createManager;

    private static GameController instance = null;
    public static GameController Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        createManager = GetComponentInChildren<CreateManager>();
        maps = new List<Dictionary<Vector2, GameObject>>();
    }

    void Start ()
    {
        createManager.InitChessBoard();
        //CreateManager.Instance.InitChessBoard();
        whoWalk = 着法状态.到红方走;
        UpdateChessGame();
    }
	
	void Update () {
		
	}
    /// <summary>
    /// 更新棋局
    /// </summary>
    public static void UpdateChessGame()
    {
        CalculateUtil.UpdateChessGame();
        maps.Add(CalculateUtil.vector2Chesse);
    }
    /// <summary>
    /// 回合制 轮流走棋 ，itween 运动完成后调用
    /// </summary>
    public void TBS()
    {
        if(whoWalk == 着法状态.到红方走)
        {
            whoWalk = 着法状态.到黑方走;
        }
        else//到黑方走
        {
            whoWalk = 着法状态.到红方走;
        }

        UpdateChessGame();
        ResetChessReciprocalStateEvent();
        Chess_Boss.TipsBeAttacking();
    }
}
