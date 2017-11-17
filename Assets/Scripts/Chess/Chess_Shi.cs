using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 士/仕
/// </summary>
public class Chess_Shi : BaseChess
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

    public override List<Vector2> CanMovePoints()
    {
        Vector2 currentPos = CalculateUtil.chesse2Vector[gameObject];
        List<Vector2> canMovePoints = new List<Vector2>();

        if (GetComponent<ChessCamp>().camp == Camp.Red)
        {
            if (currentPos.x < 5 && currentPos.y < 2)   //45°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x + 1, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints);
            }
            
            if (currentPos.x < 5 && currentPos.y > 0)   //-45°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x + 1, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints);
            }
            
            if (currentPos.x > 3 && currentPos.y < 2)   //135°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x - 1, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints);
            }
            
            if (currentPos.x > 3 && currentPos.y > 0)   //-135°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x - 1, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints);
            }
        }

        if (GetComponent<ChessCamp>().camp == Camp.Black)
        {
            if (currentPos.x < 5 && currentPos.y < 9)   //45°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x + 1, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints);
            }

            if (currentPos.x < 5 && currentPos.y > 7)   //-45°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x + 1, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints);
            }

            if (currentPos.x > 3 && currentPos.y < 9)   //135°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x - 1, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints);
            }

            if (currentPos.x > 3 && currentPos.y > 7)   //-135°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x - 1, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints);
            }
        }

        return canMovePoints;
    }

    /// <summary>
    /// 士专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, List<Vector2> canMovePoints)
    {
        //若网格存在，即在棋盘内
        if (CalculateUtil.vector2Grids.ContainsKey(value))
        {
            //若有棋子
            if (CalculateUtil.vector2Chesse.ContainsKey(value))
            {
                GameObject otherChess = CalculateUtil.vector2Chesse[value];
                if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                    canMovePoints.Add(value);
            }
            else
                canMovePoints.Add(value);
        }
    }
}
