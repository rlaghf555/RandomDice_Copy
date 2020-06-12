using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health = 0;
    protected float speed = 40.0f;
    protected GameObject target1;
    protected GameObject target2;
    protected GameObject target3;
    protected GameController gamecontroller;
    public Text healthText;
    public float movedDistance = 0;
    public short flag;
    // Start is called before the first frame update
    void Start()
    {
        target1 = GameObject.Find("TargetPos1");
        target2 = GameObject.Find("TargetPos2");
        //target3 = GameObject.Find("TargetPos3");
        gamecontroller = FindObjectOfType<GameController>();
        health = gamecontroller.EnemyHealth;
       // healthText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update() 
    {

        //transform.position = transform.position + new Vector3(0, 1, 0);
        healthText.text = health.ToString();

        //목적지 까지 이동
        if (flag == 1)
        {
            transform.position =  Vector3.MoveTowards(transform.position, target1.transform.position, Time.deltaTime * speed);
            movedDistance += Time.deltaTime * speed;
            if (transform.position == target1.transform.position)
            {
                flag = 2;
            }
        }
        else if (flag == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, target1.transform.position, Time.deltaTime * speed);
            movedDistance += Time.deltaTime * speed;
            if (transform.position == target1.transform.position)
            {
                flag = 3;
            }
        }
        else if (flag == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, target2.transform.position, Time.deltaTime * speed);
            movedDistance += Time.deltaTime * speed;

            if (transform.position == target2.transform.position)
            {
                //플레이어 목숨 감소
                Debug.Log("GameEnd");
                SceneManager.LoadScene("GamePlay");               

            }
            //if (transform.position == target2.transform.position)
            //    flag = 3;
        }
        /*
        else if (flag == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, target3.transform.position, Time.deltaTime * speed);
            movedDistance += Time.deltaTime * speed;
            if (transform.position == target3.transform.position)
            {
                //플레이어 목숨 감소
                Debug.Log("목숨감소");

            }

        }
        */
        if(health <= 0)
        {
            gamecontroller.Enemydeath();
            Destroy(gameObject);
        }
    }
}
