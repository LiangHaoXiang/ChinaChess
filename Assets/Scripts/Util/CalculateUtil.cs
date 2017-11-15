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
    /// 获取当前棋局所有走法
    /// </summary>
    public void GetAllMoves()
    {

    }
}
