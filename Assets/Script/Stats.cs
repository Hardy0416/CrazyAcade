using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    int speed = 100;
    [SerializeField]
    int bombsNum = 1;
    [SerializeField]
    float power = 1.4f;

    public int Speed { get { return speed; }  set{ speed = value; } }
    public int BombsNum { get { return bombsNum; } set { bombsNum = value; } }
    public float Power { get { return power; } set { power =  value; } }


}
