using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundArea : MonoBehaviour
{


    public int row;
    public int col;


    private void Start()
    {

        row =  int.Parse(transform.parent.name);
        col = int.Parse(name);


        Vector3 scale = transform.localScale;
        transform.localScale = scale * 60f / 100f;

    }

    public List<Footballer> team1;
    public List<Footballer> team2;
    


    public bool ContainRival(Footballer footballer)
    {
        if(footballer.gameObject.layer == 6)
        {

            return team2.Count > 0;

        }
        else
        {
            return team1.Count > 0;

        }


    }


    public bool ContainMate(Footballer footballer)
    {
        if (footballer.gameObject.layer == 6)
        {

            return team1.Count > 0;

        }
        else
        {
            return team2.Count > 0;

        }


    }


}
