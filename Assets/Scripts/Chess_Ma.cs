using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess_Ma : BaseChess
{
    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {

    }

    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 该棋子能移动的所有位置,返回的是平面二维坐标，如(0,0)、(3,5)、(6,6)等
    /// </summary>
    /// <returns></returns>
    public override List<Vector2> CanMovePoints()
    {
        Vector2 currentPos = GameController.coords[transform.position];

        bool stopHourseRight = false;   //在右边绊马脚
        bool stopHourseLeft = false;    //在左边绊马脚
        bool stopHourseUp = false;      //在上边绊马脚
        bool stopHourseDown = false;    //在下边绊马脚
        List<Vector2> canMovePoints = new List<Vector2>();
        //优先判断有没有绊马脚的棋子
        //若马的右方有绊马脚棋子......
        if (GameController.vector2Chesse.ContainsKey(new Vector2(currentPos.x + 1, currentPos.y)))
            stopHourseRight = true;
        if (GameController.vector2Chesse.ContainsKey(new Vector2(currentPos.x - 1, currentPos.y)))
            stopHourseLeft = true;
        if (GameController.vector2Chesse.ContainsKey(new Vector2(currentPos.x, currentPos.y + 1)))
            stopHourseUp = true;
        if (GameController.vector2Chesse.ContainsKey(new Vector2(currentPos.x, currentPos.y - 1)))
            stopHourseDown = true;

        if (stopHourseRight == false)
        {
            //2点钟
            Vector2 v2 = new Vector2(currentPos.x + 2, currentPos.y + 1);
            JudgeMovePoint(v2, canMovePoints);
            //4点钟
            Vector2 v4 = new Vector2(currentPos.x + 2, currentPos.y - 1);
            JudgeMovePoint(v4, canMovePoints);
        }
        if (stopHourseDown == false)
        {
            //5点钟
            Vector2 v5 = new Vector2(currentPos.x + 1, currentPos.y - 2);
            JudgeMovePoint(v5, canMovePoints);
            //7点钟
            Vector2 v7 = new Vector2(currentPos.x - 1, currentPos.y - 2);
            JudgeMovePoint(v7, canMovePoints);
        }
        if (stopHourseLeft == false)
        {
            //8点钟
            Vector2 v8 = new Vector2(currentPos.x - 2, currentPos.y - 1);
            JudgeMovePoint(v8, canMovePoints);
            //10点钟
            Vector2 v10 = new Vector2(currentPos.x - 2, currentPos.y + 1);
            JudgeMovePoint(v10, canMovePoints);

        }
        if (stopHourseUp == false)
        {
            //11点钟
            Vector2 v11 = new Vector2(currentPos.x - 1, currentPos.y + 2);
            JudgeMovePoint(v11, canMovePoints);
            //1点钟
            Vector2 v1 = new Vector2(currentPos.x + 1, currentPos.y + 2);
            JudgeMovePoint(v1, canMovePoints);
        }

        return canMovePoints;
    }
    /// <summary>
    /// 马专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, List<Vector2> canMovePoints)
    {
        //若网格存在，即在棋盘内
        if (GameController.vector2Grids.ContainsKey(value))
        {
            //若有棋子
            if (GameController.vector2Chesse.ContainsKey(value))
            {
                GameObject otherChess = GameController.vector2Chesse[value];
                if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                    canMovePoints.Add(value);
            }
            else
                canMovePoints.Add(value);
        }
    }
}
