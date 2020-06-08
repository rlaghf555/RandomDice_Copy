using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public DICETYPE diceType;
    public int requiredSP;
    // Start is called before the first frame update
    void Start()
    {
        requiredSP = 100;
        GetComponentInChildren<Text>().text = requiredSP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Upgrade()
    {
        if (requiredSP > 800)
            return;

        if (FindObjectOfType<GameController>().SP> requiredSP)
        {
            FindObjectOfType<GameController>().SP -= requiredSP;
            requiredSP *= 2;
            if (requiredSP > 800)
            {
                GetComponentInChildren<Text>().text = "Max";
                return;
            }
            GetComponentInChildren<Text>().text = requiredSP.ToString();
            FindObjectOfType<GameController>().Sp_Update();
        }
        Dice[] dices = FindObjectsOfType<Dice>();
        foreach (Dice d in dices)
        {
            if (d.TYPE == diceType)
                d.Upgrade();
        }
    }
}
