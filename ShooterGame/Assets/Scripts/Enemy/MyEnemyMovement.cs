using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MyEnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private NavMeshAgent nav;
    private MyEnemyHeath myEnemyHeath;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        myEnemyHeath= GetComponent<MyEnemyHeath>();
    }
  

    // Update is called once per frame
    void Update()
    {
        if(myEnemyHeath.GetHeath()>0)
            //判断是否死亡，如果死亡，就不再跟随玩家
        nav .SetDestination(player.transform.position);//设置目的地     组件禁用仍会导航
    }
}
