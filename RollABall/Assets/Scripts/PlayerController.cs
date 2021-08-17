using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManager Manager;
    bool isJump;

    
    Rigidbody rigid;
    AudioSource audio;
    void Awake()
    {
        isJump = false;
        itemCount = 0;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v),ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            Manager.GetItem(itemCount);
        }
        else if (other.tag == "Finish")
        {
            if(itemCount == Manager.totalItemCount)
            {

                if (Manager.stage == 2 )
                {

                    SceneManager.LoadScene(0);
                }
                else SceneManager.LoadScene(Manager.stage + 1);


            }
            else
            {
                //Restart..
                SceneManager.LoadScene(Manager.stage);

            }
        }
    }

}

