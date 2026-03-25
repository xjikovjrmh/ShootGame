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
    private Animator enemyAnim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayerHeath=player.GetComponent<MyPlayerHeath>();
        enemyAnim=GetComponent<Animator>();  //要获取enemy的animtor组件，而不是玩家的
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!myPlayerHeath.IsPlayerDead&&PlayerInRange&&timer>0.5f)//每帧执行要有冷却 //角色死亡就不攻击
        {
            Attack();//alter +Enter 生成方法

        }
        if(myPlayerHeath.IsPlayerDead)
        {
            //播放死亡动画
            enemyAnim.SetTrigger("PlayerDeath"); //这是角色死亡的触发器，播放的是敌人游荡动画，不要搞混为角色动画
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

//注意，要修改预制体并覆盖原来的预制体，点击场景做好的预制体 Inspector窗口中找prefeb的overrides点击apply to all

}
