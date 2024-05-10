using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public PickUpItem PickUpItemScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, Container, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;//方塊是否被撿起
    public static bool slotFull;//玩家是否拿著方塊

    private void Start()
    {
        //初始化
        if (!equipped)
        {
           PickUpItemScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            PickUpItemScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //E撿起方塊
        Vector3 distanceToPlayer = player.position - transform.position;
        print(distanceToPlayer.magnitude);
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) 
        {
            PickUp();
            print("picked!");
        }

        //Q丟下方塊
        if (equipped && Input.GetKeyDown(KeyCode.Q)) 
        {
            Drop();
            print("droped!");
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //將方塊設為container的子物件
        transform.SetParent(Container);
        transform.localPosition = Vector3.zero+ new Vector3(0,2f,1f);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //啟用動力學
        rb.isKinematic = true;
        coll.isTrigger = true;

        //啟用方塊上的腳本(吸附到九宮格正確位置的判斷)
        PickUpItemScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //將方塊覆物件設為無
        transform.SetParent(null);

    
        rb.isKinematic = false;
        coll.isTrigger = false;

        //將方塊力學方向設為玩家朝向
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //增加往前往下的力道
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        /*增加隨機旋轉
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);*/

        
        PickUpItemScript.enabled = false;
    }
}
