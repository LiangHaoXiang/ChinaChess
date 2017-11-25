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
    public override List<Vector2> CanMovePoints(Dictionary<GameObject, Vector2> chess2Vector, Dictionary<Vector2, GameObject> vector2Chess)
    {
        Vector2 currentPos = chess2Vector[gameObject];

        bool stopHourseRight = false;   //在右边绊马脚
        bool stopHourseLeft = false;    //在左边绊马脚
        bool stopHourseUp = false;      //在上边绊马脚
        bool stopHourseDown = false;    //在下边绊马脚
        List<Vector2> canMovePoints = new List<Vector2>();
        //优先判断有没有绊马脚的棋子
        //若马的右方有绊马脚棋子......
        if (vector2Chess.ContainsKey(new Vector2(currentPos.x + 1, currentPos.y)))
            stopHourseRight = true;
        if (vector2Chess.ContainsKey(new Vector2(currentPos.x - 1, currentPos.y)))
            stopHourseLeft = true;
        if (vector2Chess.ContainsKey(new Vector2(currentPos.x, currentPos.y + 1)))
            stopHourseUp = true;
        if (vector2Chess.ContainsKey(new Vector2(currentPos.x, currentPos.y - 1)))
            stopHourseDown = true;

        if (stopHourseRight == false)
        {
            //2点钟
            Vector2 v2 = new Vector2(currentPos.x + 2, currentPos.y + 1);
            JudgeMovePoint(v2, canMovePoints, vector2Chess);
            //4点钟
            Vector2 v4 = new Vector2(currentPos.x + 2, currentPos.y - 1);
            JudgeMovePoint(v4, canMovePoints, vector2Chess);
        }
        if (stopHourseDown == false)
        {
            //5点钟
            Vector2 v5 = new Vector2(currentPos.x + 1, currentPos.y - 2);
            JudgeMovePoint(v5, canMovePoints, vector2Chess);
            //7点钟
            Vector2 v7 = new Vector2(currentPos.x - 1, currentPos.y - 2);
            JudgeMovePoint(v7, canMovePoints, vector2Chess);
        }
        if (stopHourseLeft == false)
        {
            //8点钟
            Vector2 v8 = new Vector2(currentPos.x - 2, currentPos.y - 1);
            JudgeMovePoint(v8, canMovePoints, vector2Chess);
            //10点钟
            Vector2 v10 = new Vector2(currentPos.x - 2, currentPos.y + 1);
            JudgeMovePoint(v10, canMovePoints, vector2Chess);

        }
        if (stopHourseUp == false)
        {
            //11点钟
            Vector2 v11 = new Vector2(currentPos.x - 1, currentPos.y + 2);
            JudgeMovePoint(v11, canMovePoints, vector2Chess);
            //1点钟
            Vector2 v1 = new Vector2(currentPos.x + 1, currentPos.y + 2);
            JudgeMovePoint(v1, canMovePoints, vector2Chess);
        }

        return canMovePoints;
    }
    /// <summary>
    /// 马专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, List<Vector2> canMovePoints, Dictionary<Vector2, GameObject> vector2Chess)
    {
        //若网格存在，即在棋盘内
        if (CalculateUtil.vector2Grids.ContainsKey(value))
        {
            //若有棋子
            if (vector2Chess.ContainsKey(value))
            {
                GameObject otherChess = vector2Chess[value];
                if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                    canMovePoints.Add(value);
            }
            else
                canMovePoints.Add(value);
        }
    }
}
