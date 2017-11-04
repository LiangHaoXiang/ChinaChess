using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chess_Ju : BaseChess
{
    public override void Awake()
    {
        base.Awake();
    }

    void Start ()
    {

	}
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }
    }

    public override void Eat()
    {

    }
    /// <summary>
    /// 该棋子能移动的所有位置,返回的是平面二维坐标，如(0,0)、(3,5)、(6,6)等
    /// </summary>
    /// <returns></returns>
    public override List<Vector2> CanMovePoints()
    {
        Vector2 currentPos = GameController.coords[transform.position];

        List<Vector2> sameX_points = new List<Vector2>();   //存储与本棋子相同x坐标(同列)的其他棋子
        List<Vector2> sameY_points = new List<Vector2>();   //存储与本棋子相同y坐标(同行)的其他棋子
        List<Vector2> canMovePoints = new List<Vector2>();

        for(int i = 0; i < GameController.chesses.Length; i++)
        {
            Vector2 otherChessPos = GameController.coords[GameController.chesses[i].transform.position];
            if (otherChessPos.x == currentPos.x && otherChessPos.y != currentPos.y)
            {
                //有同列的棋子记录下来
                sameX_points.Add(otherChessPos);
            }
            if (otherChessPos.y == currentPos.y && otherChessPos.x != currentPos.x)
            {
                //有同行的棋子也记录下来
                sameY_points.Add(otherChessPos);
            }
        }
#region 多虑了
        //sameX_points.Sort(CompareVector2_Y.Instance().Compare);
        //sameY_points.Sort((a, b) =>
        //    {
        //        int result = 0;
        //        if (a.x > b.x)
        //            result = 1;
        //        else if (a.x < b.x)
        //            result = -1;
        //        return result;
        //    });
        sameY_points.Sort(CompareVector2_X.Instance().Compare);
        //foreach (Vector2 p in sameY_points)
        //{
        //    Debug.Log(p + "   时间:  " + Time.time);
        //}
#endregion
        //若车线上没有棋子，则可以走17个点
        if (sameX_points.Count == 0 && sameY_points.Count == 0)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i != currentPos.x)
                    canMovePoints.Add(new Vector2(i, currentPos.y));
            }
            for (int j = 0; j < 10; j++)
            {
                if (j != currentPos.y)
                    canMovePoints.Add(new Vector2(currentPos.x, j));
            }
        }
        else//同行或同列有棋子，那么在上下左右方选最靠近自身棋子的位置为可移动界限，还要再分辨是哪方棋子
        {
            float upBorder = 9;
            float downBorder = 0;
            float leftBorder = 0;
            float rightBorder = 8;
            for (int i = 0; i < sameX_points.Count; i++)
            {
                if (sameX_points[i].y > currentPos.y)   //若其他棋子在上方
                {
                    if (upBorder > sameX_points[i].y)
                        upBorder = (int)sameX_points[i].y;  //上边界不断降低
                }
                if (sameX_points[i].y < currentPos.y)  //若其他棋子在下方
                {
                    if (downBorder < sameX_points[i].y)
                        downBorder = (int)sameX_points[i].y;//下边界不断上升
                }
            }

            for (int j = 0; j < sameY_points.Count; j++)
            {
                if (sameY_points[j].x < currentPos.x)   //若其他棋子在左方
                {
                    if (leftBorder < sameY_points[j].x)
                        leftBorder = (int)sameY_points[j].x; //左边界不断向右缩进
                }
                if (sameY_points[j].x > currentPos.x)   //若其他棋子在右方
                {
                    if (rightBorder > sameY_points[j].x)
                        rightBorder = (int)sameY_points[j].x;//右边界不断向左缩进
                }
            }

            //可移动边界获取好了，还要判断是否为空(即边界还是初始值时要注意)和为开闭区间，即红方还是黑方的棋子
            Vector2 cu = new Vector2(currentPos.x, upBorder);
            if (GameController.vector2Chesse.ContainsKey(cu)) 
            {
                GameObject cuChesses = GameController.vector2Chesse[new Vector2(currentPos.x, upBorder)];
                //首先要判断边界处的棋子是否不等于自身？
                //若上边界处有棋子并且是己方棋，那就是开区间,否则是闭区间
                if (cuChesses != gameObject && cuChesses.GetComponent<ChessCamp>().camp == GetComponent<ChessCamp>().camp)
                    upBorder = upBorder - 1;
            }

            Vector2 cd = new Vector2(currentPos.x, downBorder);
            if (GameController.vector2Chesse.ContainsKey(new Vector2(currentPos.x, downBorder)))
            {
                GameObject cdChesses = GameController.vector2Chesse[new Vector2(currentPos.x, downBorder)];
                if (cdChesses != gameObject && cdChesses.GetComponent<ChessCamp>().camp == GetComponent<ChessCamp>().camp)
                    downBorder = downBorder + 1;
            }

            Vector2 lc = new Vector2(leftBorder, currentPos.y);
            if (GameController.vector2Chesse.ContainsKey(lc))
            {
                GameObject lcChesse = GameController.vector2Chesse[new Vector2(leftBorder, currentPos.y)];
                if (lcChesse != gameObject && lcChesse.GetComponent<ChessCamp>().camp == GetComponent<ChessCamp>().camp)
                        leftBorder = leftBorder + 1;
            }

            Vector2 rc = new Vector2(rightBorder, currentPos.y);
            if (GameController.vector2Chesse.ContainsKey(rc))
            {
                GameObject rcChesse = GameController.vector2Chesse[new Vector2(rightBorder, currentPos.y)];
                if (rcChesse != gameObject && rcChesse.GetComponent<ChessCamp>().camp == GetComponent<ChessCamp>().camp)
                    rightBorder = rightBorder - 1;
            }

            //最后还要给canMovePoints赋值
            for (int i = (int)leftBorder; i <= rightBorder; i++)
            {
                if (i != currentPos.x)
                    canMovePoints.Add(new Vector2(i, currentPos.y));
            }
            for (int j = (int)downBorder; j <= upBorder; j++)
            {
                if (j != currentPos.y)
                    canMovePoints.Add(new Vector2(currentPos.x, j));
            }
        }
        return canMovePoints;
    }
}
