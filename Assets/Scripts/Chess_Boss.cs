using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool BeAttackingEventHandler(); //被将军
public class Chess_Boss : BaseChess
{
    public static event BeAttackingEventHandler BeAttackingEvent;   //被将军事件
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
    /// 提示被将军
    /// </summary>
    public static void TipsBeAttacking()
    {
        BeAttackingEvent();
    }
    /// <summary>
    /// 被将死
    /// </summary>
    public void NoWayOut()
    {

    }

    public override List<Vector2> CanMovePoints()
    {
        Vector2 currentPos = GameController.chesse2Vector[gameObject];
        List<Vector2> canMovePoints = new List<Vector2>();

        if(GetComponent<ChessCamp>().camp == Camp.Red)
        {
            if (currentPos.y <= 1)  //可向上走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints);
            }
            if (currentPos.y >= 1)  //可向下走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints);
            }
        }
        if (GetComponent<ChessCamp>().camp == Camp.Black)
        {
            if (currentPos.y <= 8)  //可向上走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints);
            }
            if (currentPos.y >= 8)  //可向下走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints);
            }
        }

        if (currentPos.x <= 4)  //可向右走
        {
            Vector2 value = new Vector2(currentPos.x + 1, currentPos.y);
            JudgeMovePoint(value, canMovePoints);
        }
        if (currentPos.x >= 4)  //可向右走
        {
            Vector2 value = new Vector2(currentPos.x - 1, currentPos.y);
            JudgeMovePoint(value, canMovePoints);
        }

        return canMovePoints;
    }

    /// <summary>
    /// 帅/将专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, List<Vector2> canMovePoints)
    {
        if (GameController.vector2Grids.ContainsKey(value))
        {
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
