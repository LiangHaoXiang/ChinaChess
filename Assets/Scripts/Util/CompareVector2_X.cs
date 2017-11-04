using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareVector2_X : IComparer<Vector2>
{
    private static CompareVector2_X instance = null;
    public static CompareVector2_X Instance()
    {
        if (instance == null)
            instance = new CompareVector2_X();
        return instance;
    }
    /// <summary>
    /// 比较二维变量a，b的x坐标，从小到大排序
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int Compare(Vector2 a, Vector2 b)
    {
        int result = 0;
        if (a.x > b.x)
            result = 1;
        else if (a.x < b.x)
            result = -1;
        return result;
    }
}
