using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack : MonoBehaviour
{
    //判断触发器有没有碰到玩家
    private GameObject player;
    private bool PlayerInRange=false;
    private float timer = 0;

    private MyPlayerHeath myPlayerHeath;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayerHeath=player.GetComponent<MyPlayerHeath>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (PlayerInRange&&timer>2.5f)//每帧执行要有冷却
        {
            Attack();//alter +Enter 生成方法

        }
    }

    private void Attack()
    {
        timer = 0;//给timer置零否则一直攻击玩家
        //获取玩家的血量组件(类)，调用其方法，
        myPlayerHeath.TakeDamage(10);

    }

    //出现情况：玩家进入，离开攻击范围时调用了两次，
    //原因1。玩家有多个碰撞体Collider脚本为Player的tag
    //2.Enemy的多个碰撞器都设为is Trigger（把capsule collider 的isTrigger设为false ，仅保留sphere collider
    //unity会自动调用的方法
    private void OnTriggerEnter(Collider other)//碰到的碰撞体会传入
    {
        if(other.gameObject==player)
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if( other.gameObject.tag=="Player")
        {
            PlayerInRange=false;
        }
    }

}
