using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{

    none=0,

    forward = 1, /*opposite*/ backward = ~forward,

    left = 3, /*opposite*/ right = ~left,

    forwardLeft = 2, /*opposite*/ backwardRight = ~forwardLeft,

    backwardLeft = 4, /*opposite*/ forwardRight = ~backwardLeft,





}

public static class DirectionHelper
{

    private static readonly Map<Vector2, Direction> directionMap = CreateMap();

    private static Map<Vector2, Direction> CreateMap()
    {
        
        Map<Vector2, Direction> temp =  new Map<Vector2, Direction>();
        temp.Add(new Vector2(0, 0), Direction.none);                //0

        temp.Add(new Vector2(0, 1), Direction.forward);             //1
        temp.Add(new Vector2(0, -1), Direction.backward);           //5
        temp.Add(new Vector2(1, 0), Direction.left);                //3
        temp.Add(new Vector2(-1, 0), Direction.right);              //7

        temp.Add(new Vector2(1, 1), Direction.forwardLeft);         //2
        temp.Add(new Vector2(-1, 1), Direction.forwardRight);       //8
        temp.Add(new Vector2(1, -1), Direction.backwardLeft);       //4
        temp.Add(new Vector2(-1, -1), Direction.backwardRight);     //6


        return temp;

    }

    public static Direction FindDirection(float x, float z)
    {
        return directionMap.Forward[new Vector2(x, z)];
    }


    public static Vector2 DirectionToVector(Direction direction)
    {
        return directionMap.Reverse[direction];
    }


}

