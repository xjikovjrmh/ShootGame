
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("玩家移动速度")]
    public float Speed = 6f;
    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        rb= GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();//获取角色动画组件
    }
    private void FixedUpdate()
    {
        //获取玩家输入
        float h = Input.GetAxisRaw("Horizontal");//只要按了就是0或者1，不会有中间值
        float v =Input.GetAxisRaw("Vertical");
        //移动
        Move(h, v);
        //旋转
        Turning();

        //切换动画
        Animating(h, v);
    }


    private void Move(float h,float v)
    {
        //移动方向向量*speed =速度
        Vector3 movementV3 = new Vector3(h, 0, v);//归一化，保持移动方向不变，速度不变
        movementV3 = movementV3.normalized * Speed * Time.deltaTime;
        rb.MovePosition(transform.position + movementV3);
    }
    private void Turning()
    {

        int FloorLayer = LayerMask.GetMask("Floor");

        //Debug.Log(FloorLayer);  //图层的id是2的n次方
        //创建射线    获取主相机      从屏幕点       鼠标
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;//穿透点



        //// 在 Physics.Raycast 之前添加此行
        //Debug.DrawRay(cameraRay.origin, cameraRay.direction * 100f, Color.red, 2.0f);

        //射线检测       射线       若穿透则返回点 距离 laymask
        bool isTouchGround = Physics.Raycast(cameraRay, out floorHit, 200, FloorLayer);

        if (isTouchGround)
        {
            Vector3 dr = floorHit.point - transform.position;
            dr.y = 0;
            Quaternion targetRot = Quaternion.LookRotation(dr);                
            rb.MoveRotation(targetRot);
            
        }
        else
        {
            //原因 ，地面旋转-90度
            // 如果一直打印这个，说明射线没打到地
             //Debug.Log("射线未击中地面");
        }
    }


    void Animating(float h, float v)
    {
        bool isW = h != 0 || v != 0;//都不等于0
        
        //写在外面，不然一直都动
        anim.SetBool("IsWalking", isW);

    }
}
