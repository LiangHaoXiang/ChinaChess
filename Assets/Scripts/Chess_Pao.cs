using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess_Pao : BaseChess
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
        Vector2 currentPos = GameController.chesse2Vector[gameObject];
        List<Vector2> canMovePoints = new List<Vector2>();

        for(int i = (int)currentPos.x - 1; i >= 0; i--)     //向左检索
        {
            Vector2 value = new Vector2(i, currentPos.y);
            bool findOtherChess = false;
            JudgeMovePoint(value, ref findOtherChess, canMovePoints);
            if (findOtherChess) break;
        }

        for (int i = (int)currentPos.x + 1; i <= 8; i++)    //向右检索
        {
            Vector2 value = new Vector2(i, currentPos.y);
            bool findOtherChess = false;
            JudgeMovePoint(value, ref findOtherChess, canMovePoints);
            if (findOtherChess) break;
        }

        for(int i = (int)currentPos.y + 1; i <= 9; i++)     //向上检索
        {
            Vector2 value = new Vector2(currentPos.x, i);
            bool findOtherChess = false;
            JudgeMovePoint(value, ref findOtherChess, canMovePoints);
            if (findOtherChess) break;
        }

        for(int i = (int)currentPos.y - 1; i >= 0; i--)     //向下检索
        {
            Vector2 value = new Vector2(currentPos.x, i);
            bool findOtherChess = false;
            JudgeMovePoint(value, ref findOtherChess, canMovePoints);
            if (findOtherChess) break;
        }

        return canMovePoints;
    }

    /// <summary>
    /// 炮专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, ref bool findOtherChess, List<Vector2> canMovePoints)
    {
        if (GameController.vector2Chesse.ContainsKey(value))    //若有其他棋子，那就停下来
        {
            GameObject otherChess = GameController.vector2Chesse[value];
            if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                canMovePoints.Add(value);
            findOtherChess = true;
        }
        else
            canMovePoints.Add(value);
    }
}
