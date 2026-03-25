using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerHeath : MonoBehaviour
{
    //
    private int PlayerOriginalHeath = 100;
    public bool IsPlayerDead = false;



    public void TakeDamage(int amount )//不关心玩家受伤方向
    {
        PlayerOriginalHeath -= amount;
        Debug.Log("当前玩家血量"+PlayerOriginalHeath);
    } 
    void Death()
    {

    }
}
