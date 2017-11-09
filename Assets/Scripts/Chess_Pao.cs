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

        return canMovePoints;
    }
}
