using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPanel : MonoBehaviour
{
    public Dice[] dices;
    int requiredSP;
    int[] ai_upgrades;
    GameController gc;
    int highest = 0;
    int[] typenum;

    // Start is called before the first frame update
    void Start()
    {
        requiredSP = 10;
        gc = FindObjectOfType<GameController>();
        ai_upgrades = new int[5];
        for (int i = 0; i < 5; i++)
            ai_upgrades[i] = 100;
        typenum = new int[5];
        StartCoroutine(AI_Upgrade());
    }

    // Update is called once per frame
    void Update()
    {

        if (gc.AI_SP >= requiredSP)
        {
            RandomDice();
        }

    }
    IEnumerator AI_Upgrade()
    {

            for (int i = 0; i < 5; i++)
            {
                if (highest == typenum[i])
                {
                    if (highest == 0)
                      break;
                    highest = i;
                    if (gc.AI_SP >= ai_upgrades[i])
                    {
                        Upgrade((DICETYPE)highest);
                        gc.AI_SP -= ai_upgrades[i];
                        ai_upgrades[i] *= 2;
                        Debug.Log((DICETYPE)highest + "Upgrade" + gc.AI_SP);
                    break;
                    }
                }
            }
            for(int i = 0; i < 15; i++)
            {
                 for(int j = 0; j < 15; j++)
                 {
                    if (i == j)
                        continue;
                    if(dices[i].TYPE==dices[j].TYPE&&dices[i].TYPE!=DICETYPE.DEFAULT)
                    {
                        if(dices[i].number==dices[j].number)
                        {
                            dices[i].Change(dices[i].number + 1);
                            dices[j].Change(0);
                        }
                    }
                 }
            }
       
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(AI_Upgrade());
    }
    void MouseEvent()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 왼쪽 버튼을 눌렀을 때의 처리
            Debug.Log("마우스 입력");
            Ray2D ray = new Ray2D(new Vector2(Input.mousePosition.x, Input.mousePosition.y), Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
            if (rayHit.collider != null)
            {
                Debug.Log("충돌O");
                foreach (Dice d in dices)
                {
                    if (rayHit.collider.gameObject.name == d.name)
                    {
                        Debug.Log(d.name);
                        d.SavePos();
                        d.moving = true;
                    }
                }

            }
            else {
                Debug.Log("충돌X");
            }
                 

        }
        if (Input.GetMouseButton(0))
        {
            // 마우스 왼쪽 버튼을 누르고 있는 도중의 처리
            //Debug.Log(Input.mousePosition);
            foreach (Dice d in dices)
            {
                if (d.moving == true)
                {
                    d.gameObject.transform.position = Input.mousePosition;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // 마우스 왼쪽 버튼을 뗄 때의 처리

            Ray2D ray = new Ray2D(new Vector2(Input.mousePosition.x, Input.mousePosition.y), Vector2.zero);
            RaycastHit2D[] rayHit = Physics2D.RaycastAll(ray.origin, ray.direction);
            Dice colDice=null;
            foreach(RaycastHit2D hit in rayHit)
            {
                if (hit.collider != null)
                {
                    foreach (Dice d in dices)
                    {
                        if (hit.collider.gameObject.name == d.name && d.moving == false)
                        {
                            colDice = d;
                            Debug.Log("마우스 입력 해제" + d);
                            break;
                        }
                    }

                }
            }
            if (colDice == null)
            {
                foreach (Dice d in dices)
                {
                    if (d.moving == true)
                    {
                        d.moving = false;
                        d.ResetPos();
                        return;
                    }
                }
            }
            Dice selectedDice;
            foreach (Dice d in dices)
            {
                if (d.moving == true)
                {
                    selectedDice = d;
                    selectedDice.moving = false;
                    if(colDice.TYPE == selectedDice.TYPE)
                    {
                        if(colDice.number == selectedDice.number)
                        {
                            colDice.Change(colDice.number + 1);
                            selectedDice.Change(0);
                        }
                    }
                    selectedDice.ResetPos();
                    return;
                }
            }
        }
    }

    public void RandomDice()
    {
        int flag = 0;
        foreach(Dice d in dices)
        {
            if(d.TYPE == DICETYPE.DEFAULT)
            {
                flag += 1;
                break;
            }
        }
        if(flag == 0)
        {
            return;
        }
        
        if (gc.AI_SP < requiredSP)
            return;
        int random = Random.Range(0, 15);
        while (dices[random].TYPE != DICETYPE.DEFAULT)
            random = Random.Range(0, 15);
        dices[random].SetType();
        gc.AI_SP -= requiredSP;
        requiredSP += 10;

        //많은 주사위 check
        for(int i = 0; i < 5; i++)
        {
            typenum[i] = 0;
        }
        foreach (Dice d in dices)
        {
            switch (d.TYPE)
            {
                case DICETYPE.FIRE:
                    typenum[0] += 1;
                    break;
                case DICETYPE.METAL:
                    typenum[1] += 1;
                    break;
                case DICETYPE.POISON:
                    typenum[2] += 1;
                    break;
                case DICETYPE.THUNDER:
                    typenum[3] += 1;
                    break;
                case DICETYPE.WIND:
                    typenum[4] += 1;
                    break;
            }
        }
        foreach (int i in typenum)
        {
            if (highest <= i)
                highest = i;
        }

    }
    public void Upgrade(DICETYPE dicetype)
    {
        foreach(Dice d in dices)
        {
            if(d.GetDicetype()== dicetype)
            {
                d.Upgrade();
            }
        }
    }
 
}
