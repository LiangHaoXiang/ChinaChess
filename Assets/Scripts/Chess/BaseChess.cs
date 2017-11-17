using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ChooseEventHandler();
public delegate void EatEventHandler(GameObject chess);

/// <summary>
/// 棋子交互状态
/// </summary>
public enum ChessReciprocalState
{
    unChoosed,      //未被选择状态
    beChoosed,      //被选择中
    moving,         //移动中
}
/// <summary>
/// 棋子所处的情境状态
/// </summary>
public enum ChessSituationState
{
    Idle,           //安然无恙
    Attacking,      //将军状态
    BeAttacked,     //只有帅/将才拥有的被将军状态
    NoWayOut,       //无路可走状态
}
public abstract class BaseChess : MonoBehaviour
{
    public static event ChooseEventHandler ChooseEvent;//选择本棋子事件，通知其他棋子为取消选择状态
    public static event EatEventHandler EatEvent;
    protected CreateManager createManager;
    protected ChessReciprocalState chessReciprocalState;    //棋子交互状态
    protected ChessSituationState chessSituationState;      //棋子形势状态

    public virtual void Awake()
    {
        createManager = GameObject.Find("CreateManager").GetComponent<CreateManager>();
        chessReciprocalState = ChessReciprocalState.unChoosed;
        chessSituationState = ChessSituationState.Idle;
        ChooseEvent += new ChooseEventHandler(CancelChoose);//订阅事件
        EatEvent += new EatEventHandler(Eat);               //订阅吃事件
        GameController.ResetChessReciprocalStateEvent += CancelChoose; //订阅重置棋子状态事件
        Chess_Boss.BeAttackingEvent += JiangJun;            //订阅将军事件
    }

    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }
    }
    /// <summary>
    /// 移动
    /// </summary>
    public void Move()
    {
        if (chessReciprocalState == ChessReciprocalState.beChoosed)
        {
            Vector2[] canMovePoints = CanMovePoints().ToArray();
            Vector3[] canMoveGrids = new Vector3[canMovePoints.Length];
            for (int i = 0; i < canMoveGrids.Length; i++)   //将所有可移动的二维坐标转化成网格点三维坐标
            {
                canMoveGrids[i] = CalculateUtil.vector2Grids[canMovePoints[i]].transform.position;
            }

            if (canMovePoints.Length > 0)
            {
                for (int i = 0; i < canMovePoints.Length; i++)
                {
                    //鼠标点击在可移动点附近范围内，即可走步
                    if (canMoveGrids[i].x - 30 <= Input.mousePosition.x && Input.mousePosition.x <= canMoveGrids[i].x + 30 &&
                        canMoveGrids[i].y - 27.5 <= Input.mousePosition.y && Input.mousePosition.y <= canMoveGrids[i].y + 27.5)
                    {
                        //若点击位置存在其他棋子 且 是敌方棋子，那就是吃
                        if (CalculateUtil.vector2Chesse.ContainsKey(canMovePoints[i]))
                        {
                            GameObject otherChess = CalculateUtil.vector2Chesse[canMovePoints[i]];
                            if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                            {
                                EatEvent(otherChess);   //吃
                            }
                        }

                        chessReciprocalState = ChessReciprocalState.moving;//这里在移动的时候时间是0.1秒，这个时间段的状态改成moving状态，还需要改进，否则会有bug
                        iTween.MoveTo(gameObject, iTween.Hash("time", 0.1f, "position", canMoveGrids[i],
                            "easetype", iTween.EaseType.linear, "oncomplete", "TBS", "oncompletetarget", GameObject.Find("GameController")));
                    }
                    else if (chessReciprocalState != ChessReciprocalState.moving)
                    {
                        CancelChoose();
                    }
                }
            }
            else
            {
                CancelChoose();
            }
        }
    }
    /// <summary>
    /// 吃
    /// </summary>
    public void Eat(GameObject chess)
    {
        if (chess == gameObject)
        {
            EatEvent -= Eat;    //需要取消订阅事件，否则销毁物体后会空引用
            ChooseEvent -= CancelChoose;
            GameController.ResetChessReciprocalStateEvent -= CancelChoose;
            Chess_Boss.BeAttackingEvent -= JiangJun;            
            Killed();
        }
    }
    /// <summary>
    /// 该棋子能移动的所有位置,返回的是平面二维坐标，如(0,0)、(3,5)、(6,6)等
    /// </summary>
    public abstract List<Vector2> CanMovePoints();
    /// <summary>
    /// 被杀
    /// </summary>
    public void Killed()
    {
        //播放音效

        //自行销毁
        DestroyImmediate(gameObject);
    }
    /// <summary>
    /// 将军
    /// </summary>
    public bool JiangJun()
    {
        //就是判断当前可移动的点包含将军的位置
        Vector2[] canMovePoints = CanMovePoints().ToArray();
        if (GetComponent<ChessCamp>().camp == Camp.Red)
        {
            for (int i = 0; i < canMovePoints.Length; i++)
            {
                if (canMovePoints[i] == CalculateUtil.chesse2Vector[createManager.GetBlackBoss()])
                {
                    Debug.Log("将军，黑方注意");
                    return true;
                }
            }
        }
        if (GetComponent<ChessCamp>().camp == Camp.Black)
        {
            for (int i = 0; i < canMovePoints.Length; i++)
            {
                if (canMovePoints[i] == CalculateUtil.chesse2Vector[createManager.GetRedBoss()])
                {
                    Debug.Log("将军，红方注意");
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 棋子点击事件
    /// </summary>
    public void ChesseClicked()
    {
        if ((GameController.whoWalk == 着法状态.到红方走 && GetComponent<ChessCamp>().camp == Camp.Red) ||
            (GameController.whoWalk == 着法状态.到黑方走 && GetComponent<ChessCamp>().camp == Camp.Black))
        {
            if (chessReciprocalState == ChessReciprocalState.unChoosed)
            {
                ChooseEvent();
                BeChoosed();
            }
            else
            {
                CancelChoose();
            }
        }
    }
    /// <summary>
    /// 被选中
    /// </summary>
    public void BeChoosed()
    {
        //有白边圈住
        transform.FindChild("白边").gameObject.SetActive(true);

        //棋子稍微上升

        //可以提示出该棋子能移动的所有位置
        Vector2[] canMovePoints = CanMovePoints().ToArray();
        for (int i = 0; i < canMovePoints.Length; i++)
        {
            CalculateUtil.vector2Grids[canMovePoints[i]].GetComponent<Image>().enabled = true;
            //若可移动点上存在其他棋子，那肯定就是敌方棋子了，提示可以击杀之
            if (CalculateUtil.vector2Chesse.ContainsKey(canMovePoints[i]))
            {
                CalculateUtil.vector2Chesse[canMovePoints[i]].transform.FindChild("被成为目标").gameObject.SetActive(true);
            }
        }
        chessReciprocalState = ChessReciprocalState.beChoosed;
    }

    /// <summary>
    /// 取消选中
    /// </summary>
    public void CancelChoose()
    {
        //将被选中时的所有变化还原
        Reset();
        for(int i = 0; i < CalculateUtil.chesses.Length; i++)
        {
            CalculateUtil.chesses[i].transform.FindChild("被成为目标").gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 重置棋子状态
    /// </summary>
    public void ResetChessReciprocalState()
    {
        if (chessReciprocalState != ChessReciprocalState.unChoosed)
            chessReciprocalState = ChessReciprocalState.unChoosed;
    }

    protected void Reset()
    {
        transform.FindChild("白边").gameObject.SetActive(false);
        for (int i = 0; i < CalculateUtil.grids.Length; i++)
        {
            GameObject.Find("Grids").transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
        ResetChessReciprocalState();
    }
}
