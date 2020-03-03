using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CrateBehavior : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    public GameStateController GSC;
    public int skillTest;
    public Unit player;

    // Start is called before the first frame update
    void Start()
    {
        GSC = FindObjectOfType<GameStateController>();

    }


    private void OnMouseDown()
    {
        //CurrentState.health++;
        //Debug.Log(("interact with crate" + this.transform.gameObject));
        if(GSC.map.SelectedUnit != null)
            SkillTest();

    }
    
    private void SkillTest()
    {
        player = GSC.map.SelectedUnit.GetComponent < Unit>();
        int[] temp = new int[3];
        if (skillTest == 0)
        {
            int damage = 0;
            for (int i = 0; i < player.strength.Length; i++)
            {
                DieFace d = GSC.map.dice[player.strength[i]].faces[Random.Range(0, 6)];
                damage += d.hit;
                //Debug.Log(damage);
            }
            if (damage > 3)
            {
                Debug.Log("You got 2 more medpacks");
                GSC.items["medpack"] += 2;
            }
            else
                Debug.Log("Skill test failed");
            
          
            
        }

        if (skillTest == 1)
        {
            temp = GSC.map.SelectedUnit.GetComponent<Unit>().insight;
        }
             
        if (skillTest == 2)
        {
            temp = GSC.map.SelectedUnit.GetComponent<Unit>().tech;
        }


    }
             
        
             
        

}
