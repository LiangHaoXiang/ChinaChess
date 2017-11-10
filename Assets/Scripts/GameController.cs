using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum 着法状态
{
    到红方走,
    到黑方走,
}

public delegate void ChessStateEventHandler();

public class GameController : MonoBehaviour
{
    #region 棋子预制体
    public GameObject red_Ju;
    public GameObject red_Ma;
    public GameObject red_Pao;
    public GameObject red_Shi;
    public GameObject red_Xiang;
    public GameObject red_Bing;
    public GameObject red_Shuai;

    public GameObject black_Ju;
    public GameObject black_Ma;
    public GameObject black_Pao;
    public GameObject black_Shi;
    public GameObject black_Xiang;
    public GameObject black_Zu;
    public GameObject black_Jiang;
    #endregion
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
    public static event ChessStateEventHandler ResetChessStateEvent;

    public static Dictionary<GameObject, Vector2> chesse2Vector;    //棋子与他现在二维坐标的映射
    public static Dictionary<Vector2, GameObject> vector2Chesse;    //棋子二维坐标与自身的映射

    void Awake()
    {
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
                vector2Grids.Add(new Vector2(j, i), GameObject.Find("Grids").transform.GetChild((9 - i) * 9 + j).gameObject);
            }
        }

        chesse2Vector = new Dictionary<GameObject, Vector2>();//记得要定时清空
        vector2Chesse = new Dictionary<Vector2, GameObject>();//记得要定时清空
    }

    void Start ()
    {
        InitChessBoard();
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
    /// 初始化棋盘所有棋子
    /// </summary>
    public void InitChessBoard()
    {
        Create(red_Ju, 0, 0);                                   Create(red_Bing, 0, 3);

        Create(red_Ma, 1, 0);         Create(red_Pao, 1, 2);

        Create(red_Xiang, 2, 0);                                Create(red_Bing, 2, 3);

        Create(red_Shi, 3, 0);

        Create(red_Shuai, 4, 0);                                Create(red_Bing, 4, 3);

        Create(red_Shi, 5, 0);

        Create(red_Xiang, 6, 0);                                Create(red_Bing, 6, 3);

        Create(red_Ma, 7, 0);         Create(red_Pao, 7, 2);

        Create(red_Ju, 8, 0);                                   Create(red_Bing, 8, 3);




        Create(black_Ju, 0, 9);                                 Create(black_Zu, 0, 6);

        Create(black_Ma, 1, 9);       Create(black_Pao, 1, 7);

        Create(black_Xiang, 2, 9);                              Create(black_Zu, 2, 6);

        Create(black_Shi, 3, 9);

        Create(black_Jiang, 4, 9);                              Create(black_Zu, 4, 6);

        Create(black_Shi, 5, 9);

        Create(black_Xiang, 6, 9);                              Create(black_Zu, 6, 6);

        Create(black_Ma, 7, 9);       Create(black_Pao, 7, 7);

        Create(black_Ju, 8, 9);                                 Create(black_Zu, 8, 6);
    }
    /// <summary>
    /// 生成棋子
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="point_X"></param>
    /// <param name="point_Y"></param>
    private void Create(GameObject prefab, int point_X, int point_Y)
    {
        Vector2 point = new Vector2(point_X, point_Y);
        GameObject go = Instantiate(prefab);
        go.transform.parent = GameObject.Find("Chesses").transform;
        go.transform.position = vector2Grids[point].transform.position;
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
        ResetChessStateEvent();
    }
}
