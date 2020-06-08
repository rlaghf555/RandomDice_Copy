using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class Boss_Knight : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        target1 = GameObject.Find("TargetPos1");
        target2 = GameObject.Find("TargetPos2");
        //target3 = GameObject.Find("TargetPos3");
        gamecontroller = FindObjectOfType<GameController>();
        health = gamecontroller.EnemyHealth*20;
        StartCoroutine(ChangeAllDices());
    }

    private void Update()
    {
        healthText.text = health.ToString();

        //목적지 까지 이동
        if (flag == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target1.transform.position, Time.deltaTime * speed);
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
        if (health <= 0)
        {
            gamecontroller.BossDeath();
            Destroy(gameObject);
        }
    }
    IEnumerator ChangeAllDices()
    {
        yield return new WaitForSeconds(1.0f);

       //Dice[] dices = FindObjectsOfType<Dice>();
       //foreach(Dice d in dices)
       //{
       //    d.StopAllCoroutines();
       //    d.Change(d.number);
       //}
       //yield return new WaitForSeconds(5.0f);
       //StartCoroutine(ChangeAllDices());


        Dice[] dices = FindObjectsOfType<Dice>();

        foreach(Dice d in dices)
        {
            d.ChangeByKnight();
        }
        
    }

}
