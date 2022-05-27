using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{



    public static GroundArea[][] groundAreas;



    // Start is called before the first frame update
    void Start()
    {
        GroundArea [] temp = GetComponentsInChildren<GroundArea>();
        int k = 0;

        groundAreas = new GroundArea[4][];
        for (int i = 0; i < 4; i++)
        {
            groundAreas[i] = new GroundArea[3];
            for (int j = 0; j < 3; j++)
            {
                groundAreas[i][j] = temp[k];
                k++;
            }

        }


    }



    public static void deneme(Footballer footballer,int row,int column)
    {


        if(footballer.gameObject.layer == 6)
        {
            denemeForTeam1(footballer,row,column);
        }
        else
        {
            denemeForTeam2(footballer,row,column);
        }



    }




    private static void denemeForTeam1(Footballer footballer, int row, int column)
    {
        bool exit = false;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {

                if (groundAreas[i][j].team1.Contains(footballer))
                {
                    groundAreas[i][j].team1.Remove(footballer);
                    exit = true;
                    break;
                }
            }
            if (exit)
                break;

        }

        groundAreas[row][column].team1.Add(footballer);

    }



    private static void denemeForTeam2(Footballer footballer, int row, int column)
    {
        bool exit = false;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {

                if (groundAreas[i][j].team2.Contains(footballer))
                {
                    groundAreas[i][j].team2.Remove(footballer);
                    exit = true;
                    break;
                }
            }
            if (exit)
                break;

        }

        groundAreas[row][column].team2.Add(footballer);

    }



}
