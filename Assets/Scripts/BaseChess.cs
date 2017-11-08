using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ChooseEventHandler();
public delegate void EatEventHandler(GameObject chess);

/// <summary>
/// 棋子状态
/// </summary>
public enum ChessState
{
    idle,
    beChoosed,
    moving,
}
public abstract class BaseChess : MonoBehaviour
{
    public static event ChooseEventHandler ChooseEvent;//选择本棋子事件，通知其他棋子为取消选择状态
    public static event EatEventHandler EatEvent;

    protected ChessState chessState;    //棋子状态

    public virtual void Awake()
    {
        chessState = ChessState.idle;
        ChooseEvent += new ChooseEventHandler(CancelChoose);//订阅事件
        EatEvent += new EatEventHandler(Eat);               //订阅吃事件
        GameController.ResetChessStateEvent += ResetChessState; //订阅重置棋子状态事件
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
        if (chessState == ChessState.beChoosed)
        {
            Vector2[] canMovePoints = CanMovePoints().ToArray();
            Vector3[] canMoveGrids = new Vector3[canMovePoints.Length];
            for (int i = 0; i < canMoveGrids.Length; i++)   //将所有可移动的二维坐标转化成网格点三维坐标
            {
                canMoveGrids[i] = GameController.vector2Grids[canMovePoints[i]].transform.position;
            }

            for (int i = 0; i < canMovePoints.Length; i++)
            {
                //鼠标点击在可移动点附近范围内，即可走步
                if (canMoveGrids[i].x - 30 <= Input.mousePosition.x && Input.mousePosition.x <= canMoveGrids[i].x + 30 &&
                    canMoveGrids[i].y - 27.5 <= Input.mousePosition.y && Input.mousePosition.y <= canMoveGrids[i].y + 27.5)
                {
                    //若点击位置存在其他棋子 且 是敌方棋子，那就是吃
                    if (GameController.vector2Chesse.ContainsKey(canMovePoints[i]))
                    {
                        GameObject otherChess = GameController.vector2Chesse[canMovePoints[i]];
                        if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                        {
                            EatEvent(otherChess);   //吃，有bug
                        }
                    }
                    //这里在移动的时候时间是0.1秒，这个时间段的状态改成moving状态，还需要改进，否则会有bug
                    chessState = ChessState.moving;
                    iTween.MoveTo(gameObject, iTween.Hash("time", 0.1f, "position", canMoveGrids[i],
                        "easetype", iTween.EaseType.linear, "oncomplete", "TBS", "oncompletetarget", GameObject.Find("GameController")));
                }
            }

            CancelChoose();
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
            GameController.ResetChessStateEvent -= ResetChessState;
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
    /// 棋子点击事件
    /// </summary>
    public void ChesseClicked()
    {
        if ((GameController.whoWalk == 着法状态.到红方走 && GetComponent<ChessCamp>().camp == Camp.Red) ||
            (GameController.whoWalk == 着法状态.到黑方走 && GetComponent<ChessCamp>().camp == Camp.Black))
        {
            if (chessState == ChessState.idle)
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
        ////这里先更新棋盘，测试用
        //GameController.UpdateChessGame();


        //可以提示出该棋子能移动的所有位置
        Vector2[] canMovePoints = CanMovePoints().ToArray();
        for (int i = 0; i < canMovePoints.Length; i++)
        {
            GameController.vector2Grids[canMovePoints[i]].GetComponent<Image>().enabled = true;
        }
        chessState = ChessState.beChoosed;
    }

    /// <summary>
    /// 取消选中
    /// </summary>
    public void CancelChoose()
    {
        //将被选中时的所有变化还原
        transform.FindChild("白边").gameObject.SetActive(false);
        for (int i = 0; i < GameController.grids.Length; i++)
        {
            GameObject.Find("Grids").transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
        chessState = ChessState.idle;
    }
    /// <summary>
    /// 重置棋子状态
    /// </summary>
    public void ResetChessState()
    {
        if (chessState != ChessState.idle)
            chessState = ChessState.idle;
    }
}
