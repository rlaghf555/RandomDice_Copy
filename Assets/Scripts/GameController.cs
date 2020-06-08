using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int enemynum = 20;
    int enemyMaxNum = 20;
    int enemyHealth = 100;
    int enemySpawned = 0;
    int bossnum = 2;
    int bossHealth = 2500;
    int level = 1;
    public Transform enenmyPrefab;
    public Transform knightPrefab;
    public Transform spawnPos1;
    public Transform spawnPos2;
    public Text ui_sp;
    [HideInInspector()]
    public int[] diceattacks;//공격력 저장
    [Tooltip("boss_round의 배수인 Stage에서 보스가 나옴(ex.2로 지정 - 2,4,6,8...")]
    public int boss_round;
    int sp = 100;
    int ai_sp = 100;
    public int SP
    {
        get
        {
            return sp;
        }
        set
        {
            sp = value;
        }
    }
    public int AI_SP
    {
        get
        {
            return ai_sp;
        }
        set
        {
            ai_sp = value;
        }
    }
    public int EnemyHealth
    {
        get
        {
            return enemyHealth;
        }
    }
    public int BossHealth
    {
        get
        {
            return bossHealth;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        enemynum = 20;
        enemyMaxNum = 20;
        enemyHealth = 100;
        enemySpawned = 0;
        StartCoroutine(CreateEnemy());
        diceattacks = new int[5];
        diceattacks[0] = 20;
        diceattacks[1] = 100;
        diceattacks[2] = 20;
        diceattacks[3] = 30;
        diceattacks[4] = 20;
        sp = 100;
        ai_sp = 100;
        Sp_Update();
        level = 1;

    }
    public void Enemydeath()
    {
        enemynum--;
        //Debug.Log("enemydeath"+enemynum);
        sp += 10;
        ai_sp += 10;
        Sp_Update();
        if (enemynum == 0)
        {
            // Debug.Log("enemynum==0");
            level++;
            if (level % boss_round == 0)
            {
                CreateBoss();
                return;
            }
            StartCoroutine(NextLevel());
        }
    }
    public void BossDeath()
    {
        bossnum--;
        sp += 100;
        ai_sp += 100;
        Sp_Update();
        if(bossnum==0)
        {
            level++;
            StartCoroutine(NextLevel());
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Sp_Update()
    {
        ui_sp.text = sp.ToString();

    }
    IEnumerator CreateEnemy()
    {
        if (enemySpawned == enemyMaxNum)
        {
            Debug.Log("stoplevel");
            StopCoroutine("CreateEnemy");
        }
        else
        {
            Transform newobject1 = Instantiate(enenmyPrefab, spawnPos1);
            newobject1.SetParent(spawnPos1.parent);
            newobject1.GetComponent<Enemy>().flag = 1;
            Transform newobject2=Instantiate(enenmyPrefab, spawnPos2);
            newobject2.SetParent(spawnPos2.parent);
            newobject2.GetComponent<Enemy>().flag = 2;
            enemySpawned += 2;

            yield return new WaitForSeconds(1.0f);
            StartCoroutine(CreateEnemy());
        }

    }
    IEnumerator NextLevel()
    {
        Debug.Log("Nextlevel");

        enemyMaxNum += 4;
        enemynum = enemyMaxNum;
        enemySpawned = 0;
        enemyHealth = (int)(enemyHealth*1.5f);
        yield return new WaitForSeconds(5);
        StartCoroutine(CreateEnemy());
    }
    void CreateBoss()
    {
        Transform newobject1 = Instantiate(knightPrefab, spawnPos1);
        newobject1.SetParent(spawnPos1.parent);
        newobject1.GetComponent<Boss_Knight>().flag = 1;
        Transform newobject2 = Instantiate(knightPrefab, spawnPos2);
        newobject2.SetParent(spawnPos2.parent);
        newobject2.GetComponent<Boss_Knight>().flag = 2;
        bossnum = 2;
    }
}
