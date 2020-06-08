using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceDot : MonoBehaviour
{
    public DICETYPE type;
    protected TARGET target;
    public int attack = 0;
    public float attackSpeed = 1;
    Image image;
    public Transform attackDotPrefab;
    bool isPlayers;
    // Start is called before the first frame update
    public DiceDot(DICETYPE d)
    {
        type = d;
    }
    void Start()
    {
        if (GetComponentInParent<Panel>() != null)
        {
            isPlayers = true;
        }
        SetDot();
        StartCoroutine(Attack());
    }
    public void SetDot()
    {
        Dice tmp = GetComponentInParent<Dice>();
        image = GetComponent<Image>();
        type = tmp.TYPE;
        //attack, attackSpeed 초기화
        switch (type)
        {
            case DICETYPE.FIRE:
                target = TARGET.FRONT;
                attack = FindObjectOfType<GameController>().diceattacks[0];
                attackSpeed = 0.8f;
                image.color = new Color(255, 0, 0, 255);
                break;
            case DICETYPE.METAL:
                target = TARGET.HEALTH;
                attack = FindObjectOfType<GameController>().diceattacks[1];
                attackSpeed = 1f;
                image.color = new Color(0, 0, 0, 255);
                break;
            case DICETYPE.POISON:
                target = TARGET.RANDOM;
                attack = FindObjectOfType<GameController>().diceattacks[2];
                attackSpeed = 1.3f;
                image.color = new Color(0, 255, 0, 255);
                break;
            case DICETYPE.THUNDER:
                target = TARGET.FRONT;
                attack = FindObjectOfType<GameController>().diceattacks[3];
                attackSpeed = 0.8f;
                image.color = new Color(185, 185, 0, 255);
                break;
            case DICETYPE.WIND:
                target = TARGET.FRONT;
                attack = FindObjectOfType<GameController>().diceattacks[4];
                attackSpeed = 0.45f;
                image.color = new Color(0, 230, 255, 255);
                break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackSpeed);
        if (isPlayers)
        {
            switch (target)
            {
                case TARGET.FRONT:
                    {
                        Enemy[] enemies = FindObjectsOfType<Enemy>();

                        float Distance = 0;
                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 2)
                                continue;
                            if (Distance <= e.movedDistance)
                            {

                                Distance = e.movedDistance;
                            }
                        }

                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 2)
                                continue;
                            if (Distance == e.movedDistance)
                            {
                                Transform tmp = Instantiate(attackDotPrefab, transform.position, Quaternion.identity);
                                tmp.SetParent(gameObject.transform);
                                tmp.GetComponent<AttackDot>().SetDot(attack, image.color, e.transform);
                                break;
                            }
                        }
                    }
                    break;
                case TARGET.HEALTH:
                    {
                        Enemy[] enemies = FindObjectsOfType<Enemy>();
                        int highestHealth = 0;
                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 2)
                                continue;
                            if (highestHealth <= e.health)
                            {
                                Debug.Log(e);

                                highestHealth = e.health;
                            }
                        }
                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 2)
                                continue;
                            if (highestHealth == e.health)
                            {
                                // Debug.Log(e + "Attack");
                                Transform tmp = Instantiate(attackDotPrefab, transform.position, Quaternion.identity);
                                tmp.SetParent(gameObject.transform);
                                tmp.GetComponent<AttackDot>().SetDot(attack, image.color, e.transform);
                                break;
                            }
                        }

                    }
                    break;
                case TARGET.RANDOM:
                    {
                        Enemy[] enemies = FindObjectsOfType<Enemy>();
                        if (enemies.Length == 0)
                            break;
                        Enemy e = enemies[Random.Range(0, enemies.Length - 1)];

                        Transform tmp = Instantiate(attackDotPrefab, transform.position, Quaternion.identity);
                        tmp.SetParent(gameObject.transform);
                        tmp.GetComponent<AttackDot>().SetDot(attack, image.color, e.transform);
                    }
                    break;
            }
        }
        else
        {
            switch (target)
            {
                case TARGET.FRONT:
                    {
                        Enemy[] enemies = FindObjectsOfType<Enemy>();

                        float Distance = 0;
                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 1)
                                continue;
                            if (Distance <= e.movedDistance)
                            {

                                Distance = e.movedDistance;
                            }
                        }

                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 1)
                                continue;
                            if (Distance == e.movedDistance)
                            {
                                Transform tmp = Instantiate(attackDotPrefab, transform.position, Quaternion.identity);
                                tmp.SetParent(gameObject.transform);
                                tmp.GetComponent<AttackDot>().SetDot(attack, image.color, e.transform);
                                break;
                            }
                        }
                    }
                    break;
                case TARGET.HEALTH:
                    {
                        Enemy[] enemies = FindObjectsOfType<Enemy>();
                        int highestHealth = 0;
                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 1)
                                continue;
                            if (highestHealth <= e.health)
                            {
                                Debug.Log(e);

                                highestHealth = e.health;
                            }
                        }
                        foreach (Enemy e in enemies)
                        {
                            if (e.flag == 1)
                                continue;
                            if (highestHealth == e.health)
                            {
                                // Debug.Log(e + "Attack");
                                Transform tmp = Instantiate(attackDotPrefab, transform.position, Quaternion.identity);
                                tmp.SetParent(gameObject.transform);
                                tmp.GetComponent<AttackDot>().SetDot(attack, image.color, e.transform);
                                break;
                            }
                        }

                    }
                    break;
                case TARGET.RANDOM:
                    {
                        Enemy[] enemies = FindObjectsOfType<Enemy>();
                        if (enemies.Length == 0)
                            break;
                        Enemy e = enemies[Random.Range(0, enemies.Length - 1)];

                        Transform tmp = Instantiate(attackDotPrefab, transform.position, Quaternion.identity);
                        tmp.SetParent(gameObject.transform);
                        tmp.GetComponent<AttackDot>().SetDot(attack, image.color, e.transform);
                    }
                    break;
            }
        }
        StartCoroutine(Attack());

    }
    public void Destroy()
    {
        Destroy(gameObject);
        Debug.Log("destroy dot");
    }

}
