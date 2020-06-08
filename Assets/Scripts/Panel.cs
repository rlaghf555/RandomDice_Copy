using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public Dice[] dices;
    public Text ui_RequiredSP;
    int requiredSP;
    // Start is called before the first frame update
    void Start()
    {
        requiredSP = 10;
        ui_RequiredSP.text = requiredSP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MouseEvent();

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
        if(flag== 0)
        {
            return;
        }
        
       GameController gc =  FindObjectOfType<GameController>();
        if (gc.SP < requiredSP)
            return;
        int random = Random.Range(0, 15);
        while (dices[random].TYPE != DICETYPE.DEFAULT)
            random = Random.Range(0, 15);
        dices[random].SetType();
        gc.SP -= requiredSP;
        requiredSP += 10;
        ui_RequiredSP.text = requiredSP.ToString();

        gc.Sp_Update();
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
