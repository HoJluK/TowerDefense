using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    [SerializeField]
    private int startMoney = 400;

    public static int Lives;
    [SerializeField]
    private int startLives = 20;

    public static int Rounds;

     void Start()
    {
        Lives = startLives;
        Money = startMoney;

        Rounds = 0;
    }
    


}
