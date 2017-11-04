using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareVector2_Y : IComparer<Vector2>
{
    private static CompareVector2_Y instance = null;
    public static CompareVector2_Y Instance()
    {
        if (instance == null)
            instance = new CompareVector2_Y();
        return instance;
    }
    /// <summary>
    /// 比较二维变量a，b的y坐标，从小到大排序
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int Compare(Vector2 a, Vector2 b)
    {
        int result = 0;
        if (a.y > b.y)
            result = 1;
        else if (a.y < b.y)
            result = -1;
        return result;
    }
}
