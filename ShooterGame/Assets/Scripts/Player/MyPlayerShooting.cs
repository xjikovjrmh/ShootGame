using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 0f;
    public float timeBetweenShots = 0.15f;
    private float effectsDisplayTime = 0.2f;
    private AudioSource gunAudio;
    private Light gunLight;
    private LineRenderer gunLine;
    private ParticleSystem gunParticle;

    //开枪发射射线相关的变量 
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootMask;

    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        gunLine = GetComponent<LineRenderer>();
        gunParticle = GetComponent<ParticleSystem>();
        shootMask = LayerMask.GetMask("Shootable");
    }
    // Update is called once per frame
    void Update()
    {

        time = time +Time.deltaTime;
        
        //获取开火按钮
        //Input.GetAxisRaw("Fire1");//绑定左键

        if(Input.GetButton("Fire1")&&time>=timeBetweenShots)
        {
            //射击
            Shoot();
        }
        if (time >= timeBetweenShots*effectsDisplayTime)//百分比 开火后时间0.2秒如果 不清零（开火），则关灯
        {
            gunLight.enabled = false;   //物体启用不启用 用setActive  ，物体组件用enabled
            gunLine.enabled = false;
        }

        //发射射线  检测有没有命中

        //更改属性

    }

    void Shoot()
    {
        //time 清0
        time = 0;
        //启用灯光
        gunLight.enabled = true;
        //绘制线条
        gunLine.SetPosition(0, transform.position);//第一个点
        //gunLine.SetPosition(1, transform.position + transform.forward * 100);//终点
        gunLine.enabled = true;
        //播放粒子组件
        gunParticle.Play();//自带停止时长，不用手动停止
        //Debug.Log(DateTime.Now.ToString("HH:mm:ss:fff"));
        gunAudio.Play();//直接播放会很吵。要加间隔时间


        //发射射线，检测是否命中

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;//方向

        if(Physics.Raycast(shootRay,out shootHit ,100,shootMask))//击中
        {
            gunLine.SetPosition(1, shootHit.point);
            //shootHit.collider.gameObject可以通过这个获取击中的物体，并得到其所有组件
            MyEnemyHeath enemyHeathy = shootHit.collider.GetComponent<MyEnemyHeath>();
            enemyHeathy.TakeDamage(20,shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100);
        }



    }
}
