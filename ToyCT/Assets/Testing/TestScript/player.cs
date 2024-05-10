using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float turnSpeed = 200f;
    public float jumpForce = 5f;
    private bool isJumping = false;

    public MeshRenderer corn;
    private Vector3 size;
    public LayerMask groundLayer;
    private Vector3 colliderOffset;
  
    private Rigidbody rb;
    private Animator anim;
private void Start()
{
    rb = GetComponent<Rigidbody>();
    anim=GetComponent<Animator>();
    colliderOffset=GetComponent<MeshCollider>().sharedMesh.bounds.center;
 
    
    
}
private void Update()
{

   
        
    
// Move forward and backward
    float moveInput = Input.GetAxis("Vertical");//取得水平輸入值-1~1
    transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
    /*Vector3 targetPosition = transform.forward * moveInput * moveSpeed * Time.deltaTime; 
    Vector3 rayOrigin = targetPosition + new Vector3(0f, 0.5f,0f); // 发射射线的起点在目标点上方
    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector3.down, 0.0f, groundLayer);

    Vector3 currentPosition = colliderOffset- (transform.localScale.y / 2f )* Vector3.up;
    print(currentPosition+"cr");
    Vector3 actualTargetPosition = hit.collider != null ? hit.point : targetPosition;
    Vector3 moveDirection = (actualTargetPosition - currentPosition).normalized;
        
    rb.velocity = moveDirection * moveSpeed;
    print(rb.velocity);*/
    
    // Turn left and right
    float turnInput = Input.GetAxis("Horizontal");//取得垂直輸入值-1~1
    transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);

    if(Input.GetKey(KeyCode.LeftShift)){
        moveInput=Input.GetAxis("Vertical")*2;
    }

    if(moveInput>Input.GetAxis("Vertical")){
        anim.SetFloat("Speed",3);
    }else{
        anim.SetFloat("Speed",Mathf.Abs(moveInput));
    }
    if(Input.GetAxis("Vertical")==0){
        if(Input.GetAxis("Vertical")==0) anim.SetFloat("Speed",0);
        
    }

    if(Input.GetKey(KeyCode.B))anim.SetFloat("Speed",5);
    
   
    

    // Jump
    if (Input.GetButtonDown("Jump") && !isJumping)
        {
        rb.AddForce(Vector3.up * jumpForce * 2.0f, ForceMode.Impulse);
        anim.SetBool("playerJump",true);
        isJumping = true;
        }
    

    if(Input.GetKeyDown(KeyCode.A)){
        anim.SetBool("Att",true);
    }else{
        anim.SetBool("Att",false);
    }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
    // Reset jump state when touching the ground
        if (collision.gameObject.CompareTag("Ground"))
            {
            anim.SetBool("playerJump",false);
            isJumping = false;
            }
    }
}
