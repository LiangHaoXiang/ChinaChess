using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool DetectBeAttackedEventHandler(Dictionary<GameObject, Vector2> chess2Vector, Dictionary<Vector2, GameObject> vector2Chess); //被将军
public class Chess_Boss : BaseChess
{
    public static event DetectBeAttackedEventHandler DetectBeAttackedEvent;   //被将军事件
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
    /// 检测是否被将军
    /// </summary>
    public static void DetectBeAttacked(Dictionary<GameObject, Vector2> chess2Vector, Dictionary<Vector2, GameObject> vector2Chess)
    {
        DetectBeAttackedEvent(chess2Vector, vector2Chess);
    }
    /// <summary>
    /// 被将死，换言之，无论怎么走都无法改变被将军的状态。
    /// </summary>
    public bool NoWayOut()
    {
        #region 不太对
        ////如果是正在被将军的时候，
        //if (BeAttackingEvent() == true)
        //{

        //    //将军可移动点数为0，就算将死
        //    if (CanMovePoints().Count == 0)
        //        return true;

        //    //或将军所有能走的点都存在敌方能走的位置，也算将死
        //    Vector2[] canMovePoints = CanMovePoints().ToArray();
        //    int isAllExist = 0;
        //    //for (int i = 0; i < canMovePoints.Length; i++)
        //    //{
        //    //    //如果存在
        //    //    if(canMovePoints[i]==)
        //    //    isAllExist++;
        //    //}
        //    if (isAllExist == canMovePoints.Length)
        //        return true;
        //    else
        //        return false;

        //    //若无论下一步怎么走都还无法避免正在被将军的状态，那就认为是将死军
        //}
        //else
        #endregion
        if (chessSituationState == ChessSituationState.BeAttacked)
        {

        }

        return false;
    }

    public override List<Vector2> CanMovePoints(Dictionary<GameObject, Vector2> chess2Vector, Dictionary<Vector2, GameObject> vector2Chess)
    {
        Vector2 currentPos = chess2Vector[gameObject];
        List<Vector2> canMovePoints = new List<Vector2>();

        if(GetComponent<ChessCamp>().camp == Camp.Red)
        {
            if (currentPos.y <= 1)  //可向上走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints, gameObject, chess2Vector, vector2Chess);
            }
            if (currentPos.y >= 1)  //可向下走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints, gameObject, chess2Vector, vector2Chess);
            }
        }
        if (GetComponent<ChessCamp>().camp == Camp.Black)
        {
            if (currentPos.y <= 8)  //可向上走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y + 1);
                JudgeMovePoint(value, canMovePoints, gameObject, chess2Vector, vector2Chess);
            }
            if (currentPos.y >= 8)  //可向下走
            {
                Vector2 value = new Vector2(currentPos.x, currentPos.y - 1);
                JudgeMovePoint(value, canMovePoints, gameObject, chess2Vector, vector2Chess);
            }
        }

        if (currentPos.x <= 4)  //可向右走
        {
            Vector2 value = new Vector2(currentPos.x + 1, currentPos.y);
            JudgeMovePoint(value, canMovePoints, gameObject, chess2Vector, vector2Chess);
        }

        if (currentPos.x >= 4)  //可向左走
        {
            Vector2 value = new Vector2(currentPos.x - 1, currentPos.y);
            JudgeMovePoint(value, canMovePoints, gameObject, chess2Vector, vector2Chess);
        }

        return canMovePoints;
    }

    /// <summary>
    /// 帅/将专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, List<Vector2> canMovePoints, GameObject self, Dictionary<GameObject, Vector2> chess2Vector, Dictionary<Vector2, GameObject> vector2Chess)
    {
        GameObject enemyBoss;
        if (self == createManager.GetRedBoss())
            enemyBoss = createManager.GetBlackBoss();
        else
            enemyBoss = createManager.GetRedBoss();

        if (CalculateUtil.vector2Grids.ContainsKey(value))
        {
            //不管value处有没有棋子，先判断value是否和敌方公照面(是否x坐标相同)
            bool existOtherChessOnSame_X_Axis = false;   //在公想要走的位置和对面公的位置之间是否有其他棋子
            //若想要走的位置和对面公同一条竖线，那要判断是否照面
            if (value.x == chess2Vector[enemyBoss].x)
            {
                float enemyBoss_Y = chess2Vector[enemyBoss].y;
                if (enemyBoss == createManager.GetBlackBoss())
                {
                    for (int i = (int)value.y + 1; i < enemyBoss_Y; i++)
                    {
                        if (vector2Chess.ContainsKey(new Vector2(value.x, i)))
                            existOtherChessOnSame_X_Axis = true;
                    }
                }
                else if(enemyBoss == createManager.GetRedBoss())
                {
                    for (int i = (int)value.y - 1; i > enemyBoss_Y; i--)
                    {
                        if (vector2Chess.ContainsKey(new Vector2(value.x, i)))
                            existOtherChessOnSame_X_Axis = true;
                    }
                }
                if (existOtherChessOnSame_X_Axis)
                {
                    //判断value位置是否有棋子
                    if (vector2Chess.ContainsKey(value))
                    {
                        GameObject otherChess = vector2Chess[value];
                        //判断value位置的棋子阵营
                        if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                            canMovePoints.Add(value);
                    }
                    else
                        canMovePoints.Add(value);
                }
            }
            else
            {
                //判断value位置是否有棋子
                if (vector2Chess.ContainsKey(value))
                {
                    GameObject otherChess = vector2Chess[value];
                    //判断value位置的棋子阵营
                    if (otherChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                        canMovePoints.Add(value);
                }
                else
                    canMovePoints.Add(value);
            }
        }
    }

}
