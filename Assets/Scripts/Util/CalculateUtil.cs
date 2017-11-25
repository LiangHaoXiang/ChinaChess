using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于辅助计算与处理的类
/// </summary>
public class CalculateUtil : MonoBehaviour
{
    public static Vector3[,] grids;
    public static Vector2[,] points;
    /// <summary>
    /// 场景grids或棋子与平面直角坐标(x方向0-8，y方向0-9)之间的映射关系
    /// </summary>
    public static Dictionary<Vector3, Vector2> coords;
    /// <summary>
    /// 二维坐标与场景中的网格点物体的映射
    /// </summary>
    public static Dictionary<Vector2, GameObject> vector2Grids;
    /// <summary>
    /// 棋子与他现在二维坐标的映射
    /// </summary>
    public static Dictionary<GameObject, Vector2> chess2Vector;
    /// <summary>
    /// 棋子二维坐标与自身的映射
    /// </summary>
    public static Dictionary<Vector2, GameObject> vector2Chess;

    void Awake()
    {
        //初始化场景网格点，获取场景中的每个排列好的网格点
        grids = new Vector3[10, 9];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                grids[i, j] = GameObject.Find("Grids").transform.GetChild((9 - i) * 9 + j).position;
            }
        }
        //初始化平面直角坐标系的二维坐标点
        points = new Vector2[10, 9];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                points[i, j] = new Vector2(j, i);//坐标和二维数组不同
            }
        }
        //初始化坐标映射字典，将场景中的网格点分别对应于二维坐标点
        coords = new Dictionary<Vector3, Vector2>();
        for (int i = 0; i < 10; i++)  //行
        {
            for (int j = 0; j < 9; j++) //列
            {
                coords.Add(grids[i, j], points[i, j]);
            }
        }
        //初始化二维坐标与场景中的网格点物体的映射，将网格点物体分别对应于以左下角为原点的二维坐标
        vector2Grids = new Dictionary<Vector2, GameObject>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                vector2Grids.Add(points[i, j], GameObject.Find("Grids").transform.GetChild((9 - i) * 9 + j).gameObject);
            }
        }

        chess2Vector = new Dictionary<GameObject, Vector2>();
        vector2Chess = new Dictionary<Vector2, GameObject>();
    }

    /// <summary>
    /// 获取当前棋局信息
    /// </summary>
    /// <returns></returns>
    public static void UpdateChessData()
    {
        if (chess2Vector != null && vector2Chess != null)
        {
            chess2Vector.Clear();
            vector2Chess.Clear();
        }

        for (int i = 0; i < PoolManager.work_List.Count; i++)
        {
            chess2Vector.Add(PoolManager.work_List[i], coords[PoolManager.work_List[i].transform.position]);
            vector2Chess.Add(coords[PoolManager.work_List[i].transform.position], PoolManager.work_List[i]);
        }
    }
    #region 先注释
    ///// <summary>
    ///// 获取当前棋局黑棋所有走法
    ///// </summary>
    //public static List<Vector2> GetAllMoves_Black()
    //{
    //    List<Vector2> allMoves = new List<Vector2>();
    //    CreateManager cm = CreateManager.Instance;

    //    //黑将的所有走法
    //    if (cm.blackBoss.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints()[i]);
    //    //黑车1的所有走法
    //    if (cm.b_Ju1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ju1.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ju1.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //黑车2的所有走法
    //    if (cm.b_Ju2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ju2.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ju2.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //黑马1的所有走法
    //    if (cm.b_Ma1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ma1.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ma1.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //黑马2的所有走法
    //    if (cm.b_Ma2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ma2.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ma2.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //黑炮1的所有走法
    //    if (cm.b_Pao1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Pao1.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Pao1.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //黑炮2的所有走法
    //    if (cm.b_Pao2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Pao2.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Pao2.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //黑士1的所有走法
    //    if (cm.b_Shi1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Shi1.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Shi1.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //黑士2的所有走法
    //    if (cm.b_Shi2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Shi2.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Shi2.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //黑象1的所有走法
    //    if (cm.b_Xiang1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //黑象2的所有走法
    //    if (cm.b_Xiang2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //黑卒1的所有走法
    //    if (cm.b_Bing1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing1.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing1.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒2的所有走法
    //    if (cm.b_Bing2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing2.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing2.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒3的所有走法
    //    if (cm.b_Bing3.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing3.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing3.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒4的所有走法
    //    if (cm.b_Bing4.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing4.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing4.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒5的所有走法
    //    if (cm.b_Bing5.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing5.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing5.GetComponent<Chess_Zu>().CanMovePoints()[i]);

    //    return allMoves;
    //}
    ///// <summary>
    ///// 获取当前棋局红棋所有走法
    ///// </summary>
    ///// <returns></returns>
    //public static List<Vector2> GetAllMoves_Red()
    //{
    //    List<Vector2> allMoves = new List<Vector2>();
    //    CreateManager cm = CreateManager.Instance;

    //    //红将的所有走法
    //    if (cm.redBoss.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints()[i]);
    //    //红车1的所有走法
    //    if (cm.r_Ju1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ju1.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ju1.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //红车2的所有走法
    //    if (cm.r_Ju2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ju2.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ju2.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //红马1的所有走法
    //    if (cm.r_Ma1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ma1.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ma1.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //红马2的所有走法
    //    if (cm.r_Ma2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ma2.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ma2.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //红炮1的所有走法
    //    if (cm.r_Pao1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Pao1.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Pao1.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //红炮2的所有走法
    //    if (cm.r_Pao2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Pao2.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Pao2.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //红仕1的所有走法
    //    if (cm.r_Shi1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Shi1.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Shi1.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //红仕2的所有走法
    //    if (cm.r_Shi2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Shi2.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Shi2.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //红象1的所有走法
    //    if (cm.r_Xiang1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //红象2的所有走法
    //    if (cm.r_Xiang2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //红兵1的所有走法
    //    if (cm.r_Bing1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing1.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing1.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵2的所有走法
    //    if (cm.r_Bing2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing2.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing2.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵3的所有走法
    //    if (cm.r_Bing3.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing3.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing3.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵4的所有走法
    //    if (cm.r_Bing4.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing4.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing4.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵5的所有走法
    //    if (cm.r_Bing5.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing5.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing5.GetComponent<Chess_Zu>().CanMovePoints()[i]);

    //    return allMoves;
    //}
    #endregion
    /// <summary>
    /// 还原某个棋子在某个棋局的位置
    /// </summary>
    /// <param name="chess"></param>
    /// <param name="point"></param>
    /// <param name="mapIndex">棋谱第index步</param>
    public static void ResetChessByMaps(GameObject chess, Vector2 point)
    {
        if (chess.transform.parent != PoolManager.workChesses)
            PoolManager.Take(chess);    //被吃的棋子从回收区提取回来
        chess.transform.position = vector2Grids[point].transform.position;
    }
    /// <summary>
    /// 假设棋子移动到某点后，棋局的状态
    /// </summary>
    /// <param name="chess">某棋子</param>
    /// <param name="point">移动到某点</param>
    /// <returns>返回第一个元素为assumptionChesse2Vector，第二个元素为assumptionVector2Chesse</returns>
    public static ArrayList MoveAssumption(GameObject chess,Vector2 point)
    {
        ArrayList arrList = new ArrayList(2);
        #region 先记录好此时棋局信息，到时候可回到原状态
        Dictionary<GameObject, Vector2> originChesse2Vector = new Dictionary<GameObject, Vector2>();
        Dictionary<Vector2, GameObject> originVector2Chesse = new Dictionary<Vector2, GameObject>();
        foreach (KeyValuePair<GameObject, Vector2> kvp in chess2Vector)
            originChesse2Vector.Add(kvp.Key, kvp.Value);
        foreach (KeyValuePair<Vector2, GameObject> kvp in vector2Chess)
            originVector2Chesse.Add(kvp.Key, kvp.Value);
        #endregion
        #region 开始假设
        Dictionary<GameObject, Vector2> assumptionChesse2Vector = new Dictionary<GameObject, Vector2>();
        Dictionary<Vector2, GameObject> assumptionVector2Chesse = new Dictionary<Vector2, GameObject>();

        if (chess2Vector.ContainsKey(chess))    //若要走的点存在棋子，也就会吃了，先将他移除映射
            chess2Vector.Remove(chess);
        if (vector2Chess.ContainsKey(point))    //若要走的点存在棋子，也就会吃了，先将他移除映射
            vector2Chess.Remove(point);

        foreach(KeyValuePair<GameObject,Vector2> kvp in chess2Vector)
        {
            if (chess == kvp.Key)
                assumptionChesse2Vector.Add(kvp.Key, point);    //假设这个棋子移动到point点处
            else
                assumptionChesse2Vector.Add(kvp.Key, kvp.Value);
        }
        foreach(KeyValuePair<Vector2,GameObject> kvp in vector2Chess)
        {
            if (chess == kvp.Value)
            {
                assumptionVector2Chesse.Add(point, kvp.Value);
            }
            else
                assumptionVector2Chesse.Add(kvp.Key, kvp.Value);
        }
        #endregion

        chess2Vector = originChesse2Vector;//计算完让他回到原来状态
        vector2Chess = originVector2Chesse;

        arrList.Add(assumptionChesse2Vector);
        arrList.Add(assumptionVector2Chesse);
        return arrList;
    }
    #region 先注释
    ///// <summary>
    ///// 获取当前棋局黑棋所有走法后的棋局信息//未修改
    ///// </summary>
    //public static List<Vector2> GetAllBlackMoves_Maps()
    //{
    //    List<Vector2> allMoves = new List<Vector2>();
    //    CreateManager cm = CreateManager.Instance;

    //    //黑将的所有走法
    //    if (cm.blackBoss.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints()[i]);
    //    //黑车1的所有走法
    //    if (cm.b_Ju1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ju1.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ju1.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //黑车2的所有走法
    //    if (cm.b_Ju2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ju2.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ju2.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //黑马1的所有走法
    //    if (cm.b_Ma1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ma1.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ma1.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //黑马2的所有走法
    //    if (cm.b_Ma2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Ma2.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Ma2.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //黑炮1的所有走法
    //    if (cm.b_Pao1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Pao1.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Pao1.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //黑炮2的所有走法
    //    if (cm.b_Pao2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Pao2.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Pao2.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //黑士1的所有走法
    //    if (cm.b_Shi1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Shi1.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Shi1.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //黑士2的所有走法
    //    if (cm.b_Shi2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Shi2.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Shi2.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //黑象1的所有走法
    //    if (cm.b_Xiang1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //黑象2的所有走法
    //    if (cm.b_Xiang2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //黑卒1的所有走法
    //    if (cm.b_Bing1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing1.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing1.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒2的所有走法
    //    if (cm.b_Bing2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing2.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing2.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒3的所有走法
    //    if (cm.b_Bing3.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing3.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing3.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒4的所有走法
    //    if (cm.b_Bing4.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing4.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing4.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //黑卒5的所有走法
    //    if (cm.b_Bing5.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.b_Bing5.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.b_Bing5.GetComponent<Chess_Zu>().CanMovePoints()[i]);

    //    return allMoves;
    //}
    ///// <summary>
    ///// 获取当前棋局红棋所有走法后的棋局信息//未修改
    ///// </summary>
    ///// <returns></returns>
    //public static List<Vector2> GetAllRedMoves_Maps()
    //{
    //    List<Vector2> allMoves = new List<Vector2>();
    //    CreateManager cm = CreateManager.Instance;

    //    //红将的所有走法
    //    if (cm.redBoss.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.blackBoss.GetComponent<Chess_Boss>().CanMovePoints()[i]);
    //    //红车1的所有走法
    //    if (cm.r_Ju1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ju1.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ju1.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //红车2的所有走法
    //    if (cm.r_Ju2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ju2.GetComponent<Chess_Ju>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ju2.GetComponent<Chess_Ju>().CanMovePoints()[i]);
    //    //红马1的所有走法
    //    if (cm.r_Ma1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ma1.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ma1.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //红马2的所有走法
    //    if (cm.r_Ma2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Ma2.GetComponent<Chess_Ma>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Ma2.GetComponent<Chess_Ma>().CanMovePoints()[i]);
    //    //红炮1的所有走法
    //    if (cm.r_Pao1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Pao1.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Pao1.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //红炮2的所有走法
    //    if (cm.r_Pao2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Pao2.GetComponent<Chess_Pao>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Pao2.GetComponent<Chess_Pao>().CanMovePoints()[i]);
    //    //红仕1的所有走法
    //    if (cm.r_Shi1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Shi1.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Shi1.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //红仕2的所有走法
    //    if (cm.r_Shi2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Shi2.GetComponent<Chess_Shi>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Shi2.GetComponent<Chess_Shi>().CanMovePoints()[i]);
    //    //红象1的所有走法
    //    if (cm.r_Xiang1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Xiang1.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //红象2的所有走法
    //    if (cm.r_Xiang2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Xiang2.GetComponent<Chess_Xiang>().CanMovePoints()[i]);
    //    //红兵1的所有走法
    //    if (cm.r_Bing1.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing1.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing1.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵2的所有走法
    //    if (cm.r_Bing2.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing2.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing2.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵3的所有走法
    //    if (cm.r_Bing3.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing3.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing3.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵4的所有走法
    //    if (cm.r_Bing4.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing4.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing4.GetComponent<Chess_Zu>().CanMovePoints()[i]);
    //    //红兵5的所有走法
    //    if (cm.r_Bing5.parent == PoolManager.workChesses)
    //        for (int i = 0; i < cm.r_Bing5.GetComponent<Chess_Zu>().CanMovePoints().Count; i++)
    //            allMoves.Add(cm.r_Bing5.GetComponent<Chess_Zu>().CanMovePoints()[i]);

    //    return allMoves;
    //}
    #endregion
}
