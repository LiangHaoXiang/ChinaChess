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

        bool findFirstLeftOtherChess = false;
        bool findFirstRightOtherChess = false;
        bool findFirstUpOtherChess = false;
        bool findFirstDownOtherChess = false;

        for (int i = (int)currentPos.x - 1; i >= 0; i--)     //向左检索
        {
            Vector2 value = new Vector2(i, currentPos.y);
            bool findSecondChess = false;
            JudgeMovePoint(value, ref findFirstLeftOtherChess, ref findSecondChess, canMovePoints);
            if (findSecondChess) break;
        }

        for (int i = (int)currentPos.x + 1; i <= 8; i++)    //向右检索
        {
            Vector2 value = new Vector2(i, currentPos.y);
            bool findSecondChess = false;
            JudgeMovePoint(value, ref findFirstRightOtherChess, ref findSecondChess, canMovePoints);
            if (findSecondChess) break;
        }

        for (int i = (int)currentPos.y + 1; i <= 9; i++)     //向上检索
        {
            Vector2 value = new Vector2(currentPos.x, i);
            bool findSecondChess = false;
            JudgeMovePoint(value, ref findFirstUpOtherChess, ref findSecondChess, canMovePoints);
            if (findSecondChess) break;
        }

        for(int i = (int)currentPos.y - 1; i >= 0; i--)     //向下检索
        {
            Vector2 value = new Vector2(currentPos.x, i);
            bool findSecondChess = false;
            JudgeMovePoint(value, ref findFirstDownOtherChess, ref findSecondChess, canMovePoints);
            if (findSecondChess) break;
        }

        return canMovePoints;
    }

    /// <summary>
    /// 炮专属判断是否可以走这个点
    /// </summary>
    /// <param name="value"></param>
    void JudgeMovePoint(Vector2 value, ref bool findFirstOtherChess, ref bool findSecondChess, List<Vector2> canMovePoints)
    {
        if (findFirstOtherChess == false)    //若还没找到第一个棋子，就让他继续找
        {
            if (GameController.vector2Chesse.ContainsKey(value))
                findFirstOtherChess = true;
            else
                canMovePoints.Add(value);
        }
        else//找到了第一个棋子后，就找第二个
        {
            if (GameController.vector2Chesse.ContainsKey(value))//找到第二个且是敌方棋子，那就可以杀
            {
                GameObject targetChess = GameController.vector2Chesse[value];
                if (targetChess.GetComponent<ChessCamp>().camp != GetComponent<ChessCamp>().camp)
                    canMovePoints.Add(value);
                findSecondChess = true;
            }
        }
    }
}
