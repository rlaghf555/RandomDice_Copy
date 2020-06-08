using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TARGET
{
    FRONT,
    RANDOM,
    HEALTH
};
public enum DICETYPE
{
    DEFAULT,
    FIRE,
    METAL,
    POISON,
    THUNDER,
    WIND
};

public class Dice : MonoBehaviour
{

    public int number = 0;
    protected DICETYPE type;
    Image image;
    Button button;
    public Transform dicedotPrefab;
    public bool moving;
    Vector3 savePos;
    List<Transform> dots;
    // Start is called before the first frame update
    public DICETYPE TYPE
    {
        get
        {
            return type;
        }
    }
    public T RandomDiceEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(1, values.Length));
    }
    void Start()
    {
        type = DICETYPE.DEFAULT;       
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        dots = new List<Transform>();
        moving = false;
        number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SavePos()
    {
        savePos = transform.position;
    }
    public void ResetPos()
    {
        Debug.Log("reset");
        gameObject.transform.position = savePos;
    }
    public void ChangeByKnight()
    {
        if (type == DICETYPE.DEFAULT)
            return;
        type = RandomDiceEnum<DICETYPE>();
        switch (type)
        {
            case DICETYPE.FIRE:
                image.color = new Color(255, 0, 0, 255);
               
                break;
            case DICETYPE.METAL:
                image.color = new Color(0, 0, 0, 255);
               
                break;
            case DICETYPE.POISON:
                image.color = new Color(0, 255, 0, 255);
               
                break;
            case DICETYPE.THUNDER:
                image.color = new Color(185, 185, 0, 255);
               
                break;
            case DICETYPE.WIND:
                image.color = new Color(0, 230, 255, 255);
                break;
        }
        foreach (Transform dd in dots)
        {
            dd.GetComponent<DiceDot>().SetDot();
        }
    }
    public void Change(int num, Transform t = null)
    {
        int tmpNum = number;
        number = num;
        if (number == 0)
        {
            foreach(Transform dd in dots)
            {
                dd.GetComponent<DiceDot>().Destroy();
            }
            dots.RemoveRange(0, tmpNum);
            image.color = new Color(255, 255, 255);
            type = DICETYPE.DEFAULT;
            return;
        }
        Transform newDot;
        type = RandomDiceEnum<DICETYPE>();
        switch (type)
        {
            case DICETYPE.FIRE:
                image.color = new Color(255, 0, 0, 255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                foreach (Transform dd in dots)
                {
                    dd.GetComponent<DiceDot>().SetDot();
                }
                break;
            case DICETYPE.METAL:
                image.color = new Color(0, 0, 0, 255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                foreach (Transform dd in dots)
                {
                    dd.GetComponent<DiceDot>().SetDot();
                }
                break;
            case DICETYPE.POISON:
                image.color = new Color(0, 255, 0, 255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                foreach (Transform dd in dots)
                {
                    dd.GetComponent<DiceDot>().SetDot();
                }
                break;
            case DICETYPE.THUNDER:
                image.color = new Color(185, 185, 0, 255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                foreach (Transform dd in dots)
                {
                    dd.GetComponent<DiceDot>().SetDot();
                }
                break;
            case DICETYPE.WIND:
                image.color = new Color(0, 230, 255, 255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                foreach (Transform dd in dots)
                {
                    dd.GetComponent<DiceDot>().SetDot();
                }
                break;
        }

        switch (number)
        {
            case 1:
                dots[0].transform.position = transform.position;
                break;
            case 2:
                dots[0].transform.position = (transform.position + new Vector3(7.5f, 7.5f, 0f));
                dots[1].transform.position = (transform.position + new Vector3(-7.5f, -7.5f, 0f));
                break;
            case 3:
                dots[0].transform.position = (transform.position + new Vector3(7.5f, 7.5f, 0f));
                dots[1].transform.position = (transform.position + new Vector3(-7.5f, -7.5f, 0f));
                dots[2].transform.position = (transform.position);
                break;
            case 4:
                dots[0].transform.position = (transform.position + new Vector3(7.5f, 7.5f, 0f));
                dots[1].transform.position = (transform.position + new Vector3(-7.5f, -7.5f, 0f));
                dots[2].transform.position = (transform.position+ new Vector3(-7.5f, 7.5f, 0f));
                dots[3].transform.position = (transform.position + new Vector3(7.5f, -7.5f, 0f));
                break;
            case 5:
                dots[0].transform.position = (transform.position + new Vector3(7.5f, 7.5f, 0f));
                dots[1].transform.position = (transform.position + new Vector3(-7.5f, -7.5f, 0f));
                dots[2].transform.position = (transform.position + new Vector3(-7.5f, 7.5f, 0f));
                dots[3].transform.position = (transform.position + new Vector3(7.5f, -7.5f, 0f));
                dots[4].transform.position = (transform.position);
                break;
            case 6:
                dots[0].transform.position = (transform.position + new Vector3(-5.0f, 5.0f, 0f));
                dots[1].transform.position = (transform.position + new Vector3(-5.0f, 0.0f, 0f));
                dots[2].transform.position = (transform.position + new Vector3(-5.0f, -5.0f, 0f));
                dots[3].transform.position = (transform.position + new Vector3(5.0f, 5.0f, 0f));
                dots[4].transform.position = (transform.position + new Vector3(5.0f, 0.0f, 0f));
                dots[5].transform.position = (transform.position + new Vector3(5.0f, -5.0f, 0f));
                break;
        }
    }
    public DICETYPE GetDicetype()
    {
        return type;
    }
    public void Upgrade()
    {
        
        foreach(Transform d in dots)
        {
            
            switch (type)
            {
                case DICETYPE.FIRE:
                    d.GetComponent<DiceDot>().attack += 10;
                    FindObjectOfType<GameController>().diceattacks[0] += 10;
                    break;
                case DICETYPE.METAL:
                    d.GetComponent<DiceDot>().attack += 100;
                    FindObjectOfType<GameController>().diceattacks[1] += 100;
                    break;
                case DICETYPE.POISON:
                    d.GetComponent<DiceDot>().attack += 10;
                    FindObjectOfType<GameController>().diceattacks[2] += 10;
                    break;
                case DICETYPE.THUNDER:
                    d.GetComponent<DiceDot>().attack += 10;
                    FindObjectOfType<GameController>().diceattacks[3] += 10;
                    break;
                case DICETYPE.WIND:
                    d.GetComponent<DiceDot>().attack += 15;
                    FindObjectOfType<GameController>().diceattacks[4] += 15;
                    break;
            }
        }
       
    }
    
    public void SetType()
    {
        type = RandomDiceEnum<DICETYPE>();
      //  Debug.Log(type);
        Transform newDot;
        number = 1;
        switch (type)
        {            
            case DICETYPE.FIRE:
                image.color = new Color(255,0,0,255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                break;
            case DICETYPE.METAL:
                image.color = new Color(0,0,0,255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                break;
            case DICETYPE.POISON:
                image.color = new Color(0,255,0,255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                break;
            case DICETYPE.THUNDER:
                image.color = new Color(185,185,0,255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                break;
            case DICETYPE.WIND:
                image.color = new Color(0,230,255,255);
                newDot = Instantiate(dicedotPrefab, transform.position, Quaternion.identity);
                newDot.SetParent(gameObject.transform);
                newDot.localScale = new Vector3(1, 1, 1);
                dots.Add(newDot);
                break;
        }
       
    }

}
