using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void PushEventHandler();
public delegate void TakeEventHandler();
public delegate void RestoreEventHandler();

public class PoolManager : MonoBehaviour
{
    //public static event PushEventHandler PushEvent; //刚加入工作区事件(棋子刚被创建就订阅一堆事件)
    public static event TakeEventHandler TakeEvent; //从回收区提取棋子事件(需要将被吃时取消订阅的事件全订阅回来)
    public static event RestoreEventHandler RestoreEvent;//棋子回收事件(需要取消订阅一堆事件)

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
        //PushEvent();
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
        TakeEvent();//把取消订阅的事件全订阅回来
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
        //RestoreEvent();
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
