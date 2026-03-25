
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("平滑移动系数")]
    public float Smoothing=5f;
    //先找到玩家位置
    private GameObject player; //拖入法
    //另一种方法，主动找，需要设置标签tag  unity 方法就是用tag找
    private Vector3 offset; //距离差
    
    private void Awake()
    {
    player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()   //大写
    {
    // player.getComponent<Rigidbody>();//获取玩家的刚体组件
    //记录距离差   !!注意这个值是固定的，不要猜测会不小心改变
    offset = transform.position- player.transform.position;//玩家到摄像机的向量    
    }
    private void FixedUpdate()
    {

        //摄像机位置=玩家位置+距离差
        // transform.position =  offset+player.transform.position ;
        //优化 角色动，相机 更平滑动
        //Lerp(a,b,t) a起点 b终点 t时间 0-1
        transform.position = Vector3.Lerp(transform.position, offset + player.transform.position,Time.deltaTime * Smoothing);

    }
}
