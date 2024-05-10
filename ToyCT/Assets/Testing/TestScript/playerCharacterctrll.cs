using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCharacterctrll : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;
    public float speed = 5f;
    public float turnSpeed = 3000;
    public float jumpForce = 5f;
    public float gravity = 9.8f;
    private Animator anim;
    public float playerAttTime;
    public static bool IsAtt=false;
   
    public float playerHP=100;



    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        
    }
    private void Update()
    {


        if(playerHP==0) {

            anim.SetBool("playerDying",true);
            //Destroy(this.gameObject,5);//設定五秒後死掉
            
            }

        // 取得玩家輸入
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        transform.Rotate( Vector3.up * moveX * Time.deltaTime * turnSpeed);//vector3.up=y軸
     

       // transform.Rotate(0, 1, 0);
        if(Input.GetKey(KeyCode.LeftShift))
        {
        moveZ=Input.GetAxis("Vertical")*2;
        }

        if (moveZ > Input.GetAxis("Vertical"))
        {
            anim.SetFloat("Speed", 3);
        }
        else
        {
            anim.SetFloat("Speed", Mathf.Abs(moveZ));
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            if (Input.GetAxis("Vertical") == 0) anim.SetFloat("Speed", 0);

        }



        if (Input.GetKey(KeyCode.B)) anim.SetFloat("Speed", 5);

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("Att", true);
        }
        else
        {
            anim.SetBool("Att", false);
        }

        // 檢查角色是否在地面上
        if (controller.isGrounded)
        {
            // 計算移動方向
            moveDirection = new Vector3(0f, 0f, moveZ);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

          //  print(moveX);print(moveZ);
            // 處理跳躍
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce * 1.2f;
                anim.SetBool("playerJump", true);
            }

        }
        else
        {
            anim.SetBool("playerJump", false);
        }
        // 處理重力
        moveDirection.y -= gravity * Time.deltaTime;
        // 移動角色
        controller.Move(moveDirection * Time.deltaTime);
        //旋轉
       

        playerAttTime = anim.GetFloat("playerAtt");
        IsAtt = playerAttTime > 0.01f? true : false;

    }

  


}
