using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 相/象
/// </summary>
public class Chess_Xiang : BaseChess
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

        //红方，不可过河
        if (GetComponent<ChessCamp>().camp == Camp.Red)
        {              
            if (currentPos.y <= 2 && currentPos.x <= 6) //45°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x + 2, currentPos.y + 2);
                Vector2 valueEye = new Vector2(currentPos.x + 1, currentPos.y + 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
            if (currentPos.y >= 2 && currentPos.x <= 6) //-45°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x + 2, currentPos.y - 2);
                Vector2 valueEye = new Vector2(currentPos.x + 1, currentPos.y - 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
            if (currentPos.y >= 2 && currentPos.x >= 2) //-135°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x - 2, currentPos.y - 2);
                Vector2 valueEye = new Vector2(currentPos.x - 1, currentPos.y - 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
            if (currentPos.y <= 2 && currentPos.x >= 2) //135°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x - 2, currentPos.y + 2);
                Vector2 valueEye = new Vector2(currentPos.x - 1, currentPos.y + 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
        }
        //黑方，不可过河
        if (GetComponent<ChessCamp>().camp == Camp.Black)
        {
            if (currentPos.y <= 7 && currentPos.x <= 6) //45°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x + 2, currentPos.y + 2);
                Vector2 valueEye = new Vector2(currentPos.x + 1, currentPos.y + 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
            if (currentPos.y >= 7 && currentPos.x <= 6) //-45°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x + 2, currentPos.y - 2);
                Vector2 valueEye = new Vector2(currentPos.x + 1, currentPos.y - 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
            if (currentPos.y >= 7 && currentPos.x >= 2) //-135°斜向下走
            {
                Vector2 value = new Vector2(currentPos.x - 2, currentPos.y - 2);
                Vector2 valueEye = new Vector2(currentPos.x - 1, currentPos.y - 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
            if (currentPos.y <= 7 && currentPos.x >= 2) //135°斜向上走
            {
                Vector2 value = new Vector2(currentPos.x - 2, currentPos.y + 2);
                Vector2 valueEye = new Vector2(currentPos.x - 1, currentPos.y + 1);   //卡象眼的位置
                JudgeMovePoint(value, valueEye, canMovePoints);
            }
        }


        return canMovePoints;
    }

    /// <summary>
    /// 相/象专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    /// <param name="valueEye">象眼位置</param>
    void JudgeMovePoint(Vector2 value, Vector2 valueEye, List<Vector2> canMovePoints)
    {
        if (GameController.vector2Grids.ContainsKey(valueEye))
        {
            if (GameController.vector2Chesse.ContainsKey(valueEye))//若象眼位置存在棋子，那就憋住了
            {
                return;
            }
            else
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
    }
}
