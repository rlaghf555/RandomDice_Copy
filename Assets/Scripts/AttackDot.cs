using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class AttackDot : MonoBehaviour
{
    public Transform target;
    public int attack = 0;
    private DICETYPE type;
    // Start is called before the first frame update
    void Start()
    {
        type = GetComponentInParent<DiceDot>().type;
        transform.localScale = new Vector3(1, 1, 1);

    }
    public void SetDot(int atk, Color color, Transform tg)
    {
        GetComponent<Image>().color = color;
        attack = atk;
        target = tg;
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        gameObject.transform.position= Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 300);
        if (transform.position == target.transform.position)
        {
            switch (type){
                case DICETYPE.FIRE:

                    break;
                case DICETYPE.POISON:             
                    if(target.gameObject.GetComponent<Poisoned>()==null)
                        target.gameObject.AddComponent<Poisoned>();
                    break;
                case DICETYPE.THUNDER:
                    short flag= target.GetComponent<Enemy>().flag;
                    break;
            }

            Destroy(gameObject);
            target.GetComponent<Enemy>().health-=attack;
        }
    }
}
