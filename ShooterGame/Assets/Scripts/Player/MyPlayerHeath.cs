using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

using UnityEngine.SceneManagement;
public class MyPlayerHeath : MonoBehaviour
{
    //
    private int PlayerOriginalHeath = 100;
    public bool IsPlayerDead = false;

    private AudioSource playerAudio;
    public AudioClip playerDeathClip;//角色死亡音效，因为要切换，所以用public对外暴露

    private Animator playerAnim;

    private PlayerMovement playerMovement;
    private MyPlayerShooting playerShooting;
    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        Debug.Log(playerAudio == null);
        playerAnim= GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<MyPlayerShooting>();//因为在子物体中

    }
    public void TakeDamage(int amount )//不关心玩家受伤方向
    {
        if (IsPlayerDead)//死亡后直接跳过，不再播放音效
            return;
        //播放受伤
        playerAudio.Play();

        PlayerOriginalHeath -= amount;
        Debug.Log(PlayerOriginalHeath);
        if (PlayerOriginalHeath <= 0)
            Death();
    } 
    void Death()
    {
        IsPlayerDead = true;
        //播放死亡音效
        playerAudio.clip= playerDeathClip;//切换
        playerAudio.Play();
        //播放死亡动画
        playerAnim.SetTrigger("Die");//这里会
        //禁用组件 禁止移动，射击  有bug，还能通过鼠标控制旋转，但是点击后不能
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
    //用公共方法
    public void RestartLevel()
    {
        //提前在unity文件-buidl setting -Add open Scenes
        SceneManager.LoadScene(0);//需要引用命名空间using UnityEngine.SceneManagement;

        //敌人动画改变，为idle

        //重启游戏
    }
}
