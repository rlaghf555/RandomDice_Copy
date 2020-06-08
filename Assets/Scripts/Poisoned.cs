using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    Enemy enemy;
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        if (enemy == null)
            enemy = GetComponent<Boss_Knight>();
        gc = FindObjectOfType<GameController>();
        StartCoroutine(PoisonDamage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PoisonDamage()
    {
        yield return new WaitForSeconds(1.5f);
        enemy.health -= gc.diceattacks[2];
        enemy.healthText.text = enemy.health.ToString();
        Debug.Log("poison damage");
        StartCoroutine(PoisonDamage());
    }
}
