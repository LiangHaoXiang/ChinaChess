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
    public static Vector3[,] grids;
    public static Vector2[,] points;
    /// <summary>
    /// 场景grids或棋子与平面直角坐标(x方向0-8，y方向0-9)之间的映射关系
    /// </summary>
    public static Dictionary<Vector3, Vector2> coords;
    /// <summary>
    /// 二维坐标与场景中的网格点物体的映射
    /// </summary>
    public static Dictionary<Vector2, GameObject> vector2Grids;

    public static GameObject[] chesses;   //获取所有棋子
    public static 着法状态 whoWalk;
    public static event ChessReciprocalStateEventHandler ResetChessReciprocalStateEvent;

    public static Dictionary<GameObject, Vector2> chesse2Vector;    //棋子与他现在二维坐标的映射
    public static Dictionary<Vector2, GameObject> vector2Chesse;    //棋子二维坐标与自身的映射

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
        //初始化场景网格点，获取场景中的每个排列好的网格点
        grids = new Vector3[10, 9];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                grids[i, j] = GameObject.Find("Grids").transform.GetChild((9 - i) * 9 + j).position;
            }
        }
        //初始化平面直角坐标系的二维坐标点
        points = new Vector2[10, 9];
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                points[i, j] = new Vector2(j, i);//坐标和二维数组不同
            }
        }
        //初始化坐标映射字典，将场景中的网格点分别对应于二维坐标点
        coords = new Dictionary<Vector3, Vector2>();
        for (int i = 0; i < 10; i++)  //行
        {
            for(int j = 0; j < 9; j++) //列
            {
                coords.Add(grids[i, j], points[i, j]);
            }
        }
        //初始化二维坐标与场景中的网格点物体的映射，将网格点物体分别对应于以左下角为原点的二维坐标
        vector2Grids = new Dictionary<Vector2, GameObject>();
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                vector2Grids.Add(points[i, j], GameObject.Find("Grids").transform.GetChild((9 - i) * 9 + j).gameObject);
            }
        }

        chesse2Vector = new Dictionary<GameObject, Vector2>();
        vector2Chesse = new Dictionary<Vector2, GameObject>();

        createManager = GetComponentInChildren<CreateManager>();
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
        if (chesse2Vector != null && vector2Chesse != null)
        {
            chesse2Vector.Clear();
            vector2Chesse.Clear();
        }

        int count = GameObject.Find("Chesses").transform.childCount;
        chesses = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            chesses[i] = GameObject.Find("Chesses").transform.GetChild(i).gameObject;//获取所有棋子
        }
        for (int i = 0; i < count; i++)
        {
            chesse2Vector.Add(chesses[i], coords[chesses[i].transform.position]);
            vector2Chesse.Add(coords[chesses[i].transform.position], chesses[i]);
        }
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
