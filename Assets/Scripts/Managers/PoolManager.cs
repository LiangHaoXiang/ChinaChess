using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static List<GameObject> work_List;       //工作区列表
    public static List<GameObject> restore_List;    //回收区列表

    public static Transform restoreChesses;         //回收区父物体
    public static Transform workChesses;            //工作区父物体

    void Awake()
    {
        restoreChesses = GameObject.Find("RestoreChesses").transform;
        workChesses = GameObject.Find("WorkChesses").transform;
        work_List = new List<GameObject>();
        restore_List = new List<GameObject>();
    }
    /// <summary>
    /// 将创建的棋子加入工作区
    /// </summary>
    /// <param name="chess"></param>
    public static void Push(GameObject chess)
    {
        work_List.Add(chess);
        SetParentToWork(chess);
    }
    /// <summary>
    /// 提取棋子
    /// </summary>
    /// <param name="chess"></param>
    public static void Take(GameObject chess)
    {
        work_List.Add(chess);
        restore_List.Remove(chess);
        SetParentToWork(chess);
        chess.SetActive(true);
    }
    /// <summary>
    /// 回收棋子
    /// </summary>
    /// <param name="chess"></param>
    public static void Restore(GameObject chess)
    {
        restore_List.Add(chess);
        work_List.Remove(chess);
        SetParentToRestore(chess);
        chess.SetActive(false);
    }

    public static void SetParentToWork(GameObject chess)
    {
        chess.transform.parent = workChesses;
    }

    public static void SetParentToRestore(GameObject chess)
    {
        chess.transform.parent = restoreChesses;
    }
}
