using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static List<Dictionary<GameObject, Vector2>> maps;

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
        maps = new List<Dictionary<GameObject, Vector2>>();
    }

    void Start ()
    {
        createManager.InitChessBoard();
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
        CalculateUtil.UpdateChessData();        //更新棋局
        Dictionary<GameObject, Vector2> temp = new Dictionary<GameObject, Vector2>();
        foreach (KeyValuePair<GameObject, Vector2> kvp in CalculateUtil.chess2Vector)
        {
            //一定要遍历赋值的，否则如果直接temp=CalculateUtil.chess2Vector就相当于引用了这个静态字典，没用
            temp.Add(kvp.Key, kvp.Value);
        }
        maps.Add(temp);  //添加棋谱
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
        Chess_Boss.DetectBeAttacked();
    }
    /// <summary>
    /// 悔棋点击事件
    /// </summary>
    public void Undo_Click()
    {
        if (maps.Count >= 3)
        {
            Dictionary<GameObject, Vector2> temp = maps[maps.Count - 1 - 2];
            foreach (KeyValuePair<GameObject, Vector2> kvp in temp)
            {
                CalculateUtil.ResetChessByMaps(kvp.Key, kvp.Value);
            }
            maps.RemoveRange(maps.Count - 2, 2);
            CalculateUtil.UpdateChessData();
            ResetChessReciprocalStateEvent();
            Chess_Boss.DetectBeAttacked();
        }
        else
        {
            //GameObject.Find("UndoButton").GetComponent<Button>().enabled = false;
        }
    }


}
