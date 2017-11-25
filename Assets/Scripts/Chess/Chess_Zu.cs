using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 兵、卒 脚本
/// </summary>
public class Chess_Zu : BaseChess
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

    public override List<Vector2> CanMovePoints(Dictionary<GameObject, Vector2> chess2Vector, Dictionary<Vector2, GameObject> vector2Chess)
    {
        Vector2 currentPos = chess2Vector[gameObject];
        List<Vector2> canMovePoints = new List<Vector2>();

        //若是红方且过了河 或是 黑方且过了河，就能左右走
        if ((GetComponent<ChessCamp>().camp == Camp.Red && currentPos.y >= 5) ||
            (GetComponent<ChessCamp>().camp == Camp.Black && currentPos.y <= 4))
        {
            Vector2 valueLeft = new Vector2(currentPos.x - 1, currentPos.y);
            JudgeMovePoint(valueLeft, canMovePoints, vector2Chess);

            Vector2 valueRight = new Vector2(currentPos.x + 1, currentPos.y);
            JudgeMovePoint(valueRight, canMovePoints, vector2Chess);
        }

        //若是红方，只能向上走。
        if (GetComponent<ChessCamp>().camp == Camp.Red)
        {
            Vector2 valueUp = new Vector2(currentPos.x, currentPos.y + 1);
            JudgeMovePoint(valueUp, canMovePoints, vector2Chess);
        }
        //若是黑方，只能向下走
        if (GetComponent<ChessCamp>().camp == Camp.Black)
        {
            Vector2 valueDown = new Vector2(currentPos.x, currentPos.y - 1);
            JudgeMovePoint(valueDown, canMovePoints, vector2Chess);
        }

        return canMovePoints;
    }

    /// <summary>
    /// 兵专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, List<Vector2> canMovePoints, Dictionary<Vector2, GameObject> vector2Chess)
    {
        if (CalculateUtil.vector2Grids.ContainsKey(value))
        {
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
