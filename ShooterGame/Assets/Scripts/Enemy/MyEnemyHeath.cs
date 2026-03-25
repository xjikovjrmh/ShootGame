using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHeath : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip DeathClip;
    private int OriginalHeathy = 100;
    private AudioSource enemyAudioSource;
    private ParticleSystem enemyParticles;
    private Animator enemyAnimator;
    private CapsuleCollider enemyCapsuleCollider;
    private bool IsDead=false;
    private bool IsSinking =false;
    private void Awake()
    {
        enemyAudioSource =GetComponent<AudioSource>();//直接获取敌人身上音量脚本
        enemyParticles = GetComponentInChildren<ParticleSystem>();//获取子物体组件  预先给敌人作为子物体
        enemyAnimator =GetComponent<Animator>();
        enemyCapsuleCollider =GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        //移动物体
        if (IsSinking)
        {                                   //Time.deltaTime本质是对时间进行分割
            transform.Translate(-transform.up*2.5f*Time.deltaTime);         //没transform.down      仅有up right forward 需要加负号
        }
    }
    //私有化，不直接访问，要对外暴露方法
    public void TakeDamage(int amount,Vector3 Hitpoint)
    {
        //判断敌人是否死亡，若死亡直接返回
        if (IsDead) return;




        //声音的播放
        enemyAudioSource.Play();
        //粒子特效播放   设置位置
        enemyParticles.transform.position = Hitpoint;
        enemyParticles.Play();
        OriginalHeathy-=amount;
        if (OriginalHeathy < 0)
        {
            //死亡
            Death();
        }
        //Debug.Log(OriginalHeathy);
    }
    public int GetHeath()
    {
        return OriginalHeathy;
    }
    private void Death()
    {
        IsDead = true;//状态改变
        //播放死亡动画
        enemyAnimator.SetTrigger("Death");//触发

        enemyCapsuleCollider.isTrigger = true;//触发器，不会碰撞，会挡子弹
        //enemyCapsuleCollider.enabled = false;//不启用组件，不会挡子弹

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;//不受物理影响，减少性能开销

        //播放死亡音效 需要切换音效
        enemyAudioSource.clip = DeathClip;
        enemyAudioSource.Play();

    }
    //对外暴露一个公共方法，动画事件进行到一定阶段会通过名字查找方法，调用
    public void StartSinking()
    {
        //死亡后沉入地面
        IsSinking=true;
        Destroy(gameObject, 2f);//两秒后销毁

    }
}

