using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于辅助计算的类
/// </summary>
public class CalculateUtil
{
    private static CalculateUtil instance = null;

    public static CalculateUtil Instance()
    {
        if (instance == null)
            instance = new CalculateUtil();
        return instance;
    }
    /// <summary>
    /// 获取当前棋局黑棋所有走法
    /// </summary>
    public List<Vector2> GetAllMoves_Black()
    {
        List<Vector2> allMoves = new List<Vector2>();
        CreateManager cm = CreateManager.Instance;

        //黑将的所有走法
        if (cm.blackBoss != null)
            for (int i = 0; i < cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints().Count; i++)
                allMoves.Add(cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints()[i]);
        //黑车1的所有走法
        if (cm.b_Ju1 != null)
            for (int i = 0; i < cm.b_Ju1.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Ju1.GetComponent<Chess_Ju>().CanMovePoints()[i]);
        //黑车2的所有走法
        if (cm.b_Ju2 != null)
            for (int i = 0; i < cm.b_Ju2.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Ju2.GetComponent<Chess_Ju>().CanMovePoints()[i]);
        //黑马1的所有走法
        if (cm.b_Ma1 != null)
            for (int i = 0; i < cm.b_Ma1.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Ma1.GetComponent<Chess_Ma>().CanMovePoints()[i]);
        //黑马2的所有走法
        if (cm.b_Ma2 != null)
            for (int i = 0; i < cm.b_Ma2.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Ma2.GetComponent<Chess_Ma>().CanMovePoints()[i]);
        //黑炮1的所有走法
        if (cm.b_Pao1 != null)
            for (int i = 0; i < cm.b_Pao1.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Pao1.GetComponent<Chess_Pao>().CanMovePoints()[i]);
        //黑炮2的所有走法
        if (cm.b_Pao2 != null)
            for (int i = 0; i < cm.b_Pao2.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Pao2.GetComponent<Chess_Pao>().CanMovePoints()[i]);
        //黑士1的所有走法
        if (cm.b_Shi1 != null)
            for (int i = 0; i < cm.b_Shi1.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Shi1.GetComponent<Chess_Shi>().CanMovePoints()[i]);
        //黑士2的所有走法
        if (cm.b_Shi2 != null)
            for (int i = 0; i < cm.b_Shi2.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Shi2.GetComponent<Chess_Shi>().CanMovePoints()[i]);
        //黑象1的所有走法
        if (cm.b_Xiang1 != null)
            for (int i = 0; i < cm.b_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
        //黑象2的所有走法
        if (cm.b_Xiang2 != null)
            for (int i = 0; i < cm.b_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
        //黑卒1的所有走法
        if (cm.b_Bing1 != null)
            for (int i = 0; i < cm.b_Bing1.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Bing1.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //黑卒2的所有走法
        if (cm.b_Bing2 != null)
            for (int i = 0; i < cm.b_Bing2.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Bing2.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //黑卒3的所有走法
        if (cm.b_Bing3 != null)
            for (int i = 0; i < cm.b_Bing3.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Bing3.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //黑卒4的所有走法
        if (cm.b_Bing4 != null)
            for (int i = 0; i < cm.b_Bing4.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Bing4.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //黑卒5的所有走法
        if (cm.b_Bing5 != null)
            for (int i = 0; i < cm.b_Bing5.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.b_Bing5.GetComponent<Chess_Zu>().CanMovePoints()[i]);

        return allMoves;
    }
    /// <summary>
    /// 获取当前棋局红棋所有走法
    /// </summary>
    /// <returns></returns>
    public List<Vector2> GetAllMoves_Red()
    {
        List<Vector2> allMoves = new List<Vector2>();
        CreateManager cm = CreateManager.Instance;

        //红将的所有走法
        if (cm.redBoss != null)
            for (int i = 0; i < cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints().Count; i++)
                allMoves.Add(cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints()[i]);
        //红车1的所有走法
        if (cm.r_Ju1 != null)
            for (int i = 0; i < cm.r_Ju1.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Ju1.GetComponent<Chess_Ju>().CanMovePoints()[i]);
        //红车2的所有走法
        if (cm.r_Ju2 != null)
            for (int i = 0; i < cm.r_Ju2.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Ju2.GetComponent<Chess_Ju>().CanMovePoints()[i]);
        //红马1的所有走法
        if (cm.r_Ma1 != null)
            for (int i = 0; i < cm.r_Ma1.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Ma1.GetComponent<Chess_Ma>().CanMovePoints()[i]);
        //红马2的所有走法
        if (cm.r_Ma2 != null)
            for (int i = 0; i < cm.r_Ma2.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Ma2.GetComponent<Chess_Ma>().CanMovePoints()[i]);
        //红炮1的所有走法
        if (cm.r_Pao1 != null)
            for (int i = 0; i < cm.r_Pao1.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Pao1.GetComponent<Chess_Pao>().CanMovePoints()[i]);
        //红炮2的所有走法
        if (cm.r_Pao2 != null)
            for (int i = 0; i < cm.r_Pao2.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Pao2.GetComponent<Chess_Pao>().CanMovePoints()[i]);
        //红仕1的所有走法
        if (cm.r_Shi1 != null)
            for (int i = 0; i < cm.r_Shi1.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Shi1.GetComponent<Chess_Shi>().CanMovePoints()[i]);
        //红仕2的所有走法
        if (cm.r_Shi2 != null)
            for (int i = 0; i < cm.r_Shi2.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Shi2.GetComponent<Chess_Shi>().CanMovePoints()[i]);
        //红象1的所有走法
        if (cm.r_Xiang1 != null)
            for (int i = 0; i < cm.r_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
        //红象2的所有走法
        if (cm.r_Xiang2 != null)
            for (int i = 0; i < cm.r_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
        //红兵1的所有走法
        if (cm.r_Bing1 != null)
            for (int i = 0; i < cm.r_Bing1.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Bing1.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //红兵2的所有走法
        if (cm.r_Bing2 != null)
            for (int i = 0; i < cm.r_Bing2.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Bing2.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //红兵3的所有走法
        if (cm.r_Bing3 != null)
            for (int i = 0; i < cm.r_Bing3.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Bing3.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //红兵4的所有走法
        if (cm.r_Bing4 != null)
            for (int i = 0; i < cm.r_Bing4.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Bing4.GetComponent<Chess_Zu>().CanMovePoints()[i]);
        //红兵5的所有走法
        if (cm.r_Bing5 != null)
            for (int i = 0; i < cm.r_Bing5.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
                allMoves.Add(cm.r_Bing5.GetComponent<Chess_Zu>().CanMovePoints()[i]);

        return allMoves;
    }
}
