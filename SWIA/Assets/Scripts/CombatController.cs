using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CombatController : MonoBehaviour
{

    public GameObject combatPanel;
    public Image attackerImg;
    public Image enemyAttackerImg;
    public Image weaponImg;
    public Image defenderImg;
    public Image[] attackDice;
    public Image[] defDice;
    public Button initAttack;
    public Button modifyDice;
    public Button useSurge;
    public Button confirm;
    public GameObject surgePanel;
    public Toggle[] surgeList;
    public TMPro.TextMeshProUGUI mainText;
    public TMPro.TextMeshProUGUI attackResults;
    public TMPro.TextMeshProUGUI defenseResults;
    public TMPro.TextMeshProUGUI surgeText;
    public TMPro.TextMeshProUGUI finalText;
    GameStateController GSC;
    public GameObject heroDefender;

    Unit attacker;
    Unit defender;

    public int accuracy = 0;
    public int damage = 0;
    public int surge = 0;
    public int block = 0;
    public int evade = 0;
    public int dodge = 0;
    List<int> selectedToggles;
    int[] surgeFunction;
    GameObject enemyUI;

    string[] surgeTexts = { "+1 Damage", "+2 Damage", "+3 Damage", "Cleave 1", "Cleave 2", "Blast 1", "Blast 2", "Pierce 1", "Pierce 2",
                                "+1 Accuracy", "+2 Accuracy", "+3 Accuracy", "Bleed", "Stun", "Weaken", "+1 Dmg, +2 Acc", "+1 Dmg, Pierce 1",
                                "+1 Dmg, Bleed", "Pierce 1, Bleed", "Pierce 2, Bleed", "+1 Acc, Pierce 1", "+2 Acc, Pierce 1", "+1 Acc, Weaken"
                          };


    // Start is called before the first frame update
    void Start()
    {
        GSC = GameObject.FindObjectOfType<GameStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GSC.attacking && Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(GSC.cam.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity);
                if (hit && hitInfo.transform.parent.gameObject.GetComponent<Unit>() != null)
                {
                    defender = hitInfo.transform.parent.gameObject.GetComponent<Unit>();
                    GSC.path.GeneratePathTo((int)defender.transform.position.x, (int)defender.transform.position.y, false, false);
                    GSC.selectedUnit.GetComponent<Unit>().attackRange = GSC.selectedUnit.GetComponent<Unit>().currentPath.Count - 1;
                    GSC.selectedUnit.GetComponent<Unit>().currentPath = null;

                    enemyUI = GSC.gameUI.transform.GetChild(9).GetChild(3).gameObject;
                    enemyUI.transform.GetChild(0).GetComponent<Image>().sprite = defender.playerImage;
                    enemyUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = defender.health.ToString();
                    enemyUI.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = defender.MaxMovemment.ToString();
                    enemyUI.SetActive(true);
                    string str = string.Format(defender.name + "\n" + "Range: " + GSC.selectedUnit.GetComponent<Unit>().attackRange);
                    GSC.gameUI.transform.GetChild(9).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = str;
                    GSC.gameUI.transform.GetChild(9).GetChild(2).transform.gameObject.SetActive(true);
                    GSC.gameUI.transform.GetChild(9).GetChild(0).GetComponent<Button>().interactable = true;

                }
            }
        }
    }


    public void initCombat()
    {

        GSC.gameUI.transform.GetChild(9).GetChild(2).transform.gameObject.SetActive(false);
        attacker = GSC.selectedUnit.GetComponent<Unit>();
        if (attacker.unit < 6)
            enemyUI.SetActive(false);

        // If a Hero
        if (attacker.unit < 6)
        {
            foreach (KeyValuePair<Vector2, GameObject> go in GSC.data.attackSpaces)
            {
                //GSC.data.clickables[new Vector2(go.Value.transform.position.x, go.Value.transform.position.y)].GetComponent<BoxCollider>().enabled = false;
                Destroy(go.Value);
            }
            attackerImg.transform.gameObject.SetActive(true);
            attackerImg.sprite = attacker.playerImage;
            weaponImg.transform.gameObject.SetActive(true);
            weaponImg.sprite = attacker.weapon.weaponImage;
            defenderImg.transform.gameObject.SetActive(true);
            defenderImg.sprite = defender.playerImage;
            defenderImg.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = defender.health.ToString();
            defenderImg.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = defender.MaxMovemment.ToString();
            enemyAttackerImg.transform.gameObject.SetActive(false);
            heroDefender.SetActive(false);

            initAttack.transform.gameObject.SetActive(true);
            initAttack.enabled = true;
            modifyDice.transform.gameObject.SetActive(true);
            modifyDice.interactable = false;

            mainText.transform.gameObject.SetActive(true);
            mainText.text = string.Format("Range " + attacker.attackRange + " Attack");
        }
        else
        {
            defender = GSC.data.enemyTarget.GetComponent<Unit>();

            enemyAttackerImg.transform.gameObject.SetActive(true);
            enemyAttackerImg.sprite = attacker.playerImage;
            enemyAttackerImg.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = attacker.health.ToString();
            enemyAttackerImg.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = attacker.MaxMovemment.ToString();

            heroDefender.SetActive(true);
            heroDefender.transform.GetChild(0).GetComponent<Image>().sprite = GSC.data.enemyTarget.GetComponent<Unit>().playerCard;
            heroDefender.transform.GetChild(4).GetComponent<Image>().sprite = GSC.data.enemyTarget.GetComponent<Unit>().playerImage;
            heroDefender.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = GSC.data.enemyTarget.GetComponent<Unit>().health.ToString();
            heroDefender.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = GSC.data.enemyTarget.GetComponent<Unit>().endurance.ToString();
            heroDefender.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = GSC.data.enemyTarget.GetComponent<Unit>().movement.ToString();
            attackerImg.transform.gameObject.SetActive(false);
            weaponImg.transform.gameObject.SetActive(false);
            defenderImg.transform.gameObject.SetActive(false);

            initAttack.transform.gameObject.SetActive(true);
            initAttack.enabled = true;
            modifyDice.transform.gameObject.SetActive(false);

            mainText.transform.gameObject.SetActive(true);
            mainText.text = string.Format("Range " + attacker.attackRange + " Attack");
        }

        GSC.gameUI.transform.GetChild(10).gameObject.SetActive(true);

    }

    public void Attack()
    {
        StartCoroutine(startCombat());
    }

    IEnumerator startCombat()
    {
        accuracy = 0;
        damage = 0;
        surge = 0;
        block = 0;
        evade = 0;
        dodge = 0;

        initAttack.transform.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
        for (int i = 0; i < attackDice.Length; i++)
        {
            if (i < attacker.weapon.weaponDice.Length)
            {
                DieFace d = GSC.data.dice[attacker.weapon.weaponDice[i]].faces[Random.Range(0, 6)];
                attackDice[i].color = Color.white;
                attackDice[i].sprite = d.face;
                // add attack audio here
                accuracy += d.accuracy;
                damage += d.hit;
                surge += d.surge;
            }
            else
            {
                attackDice[i].color = Color.clear;
            }
            yield return new WaitForSeconds(.5f);
        }

        if (attacker.weaken && surge > 0)
            surge--;

        attackResults.text = string.Format(accuracy + " Accuracy, " + damage + " Hit, " + surge + " Surge");
        attackResults.transform.gameObject.SetActive(true);

        yield return new WaitForSeconds(.5f);
        if (accuracy >= attacker.attackRange)
            mainText.text = string.Format("Attack Hit!");
        else
            mainText.text = string.Format("Range: " + attacker.attackRange + " Accuracy: " + accuracy);
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < defDice.Length; i++)
        {
            if (i < defender.defDice.Length)
            {
                DieFace d = GSC.data.dice[defender.defDice[i]].faces[Random.Range(0, 6)];
                defDice[i].color = Color.white;
                defDice[i].sprite = d.face;
                // add defend audio here
                block += d.block;
                evade += d.evade;
                dodge += d.dodge;
            }
            else
            {
                defDice[i].color = Color.clear;
            }
            yield return new WaitForSeconds(.5f);
        }

        if (defender.weaken && evade > 0)
            evade--;

        surge -= evade;
        if (surge < 0)
            surge = 0;

        if (dodge > 0)
            defenseResults.text = string.Format("Attack Dodged!");
        else
            defenseResults.text = string.Format(block + " Block, " + evade + " Evade");

        defenseResults.transform.gameObject.SetActive(true);
        if (surge > 0)
            modifyDice.interactable = true;
        initAttack.transform.gameObject.SetActive(false);
        confirm.transform.gameObject.SetActive(true);
        yield return null;
    }

    public void modDice()
    {
        confirm.interactable = false;
        useSurge.transform.gameObject.SetActive(true);
        useSurge.enabled = true;
        surgePanel.SetActive(true);

        surgeText.text = string.Format(surge + " Surge");

        surgeFunction = new int[attacker.weapon.surgeAbilities.Length];

        for (int i = 0; i < surgeList.Length; i++)
        {
            if (i < attacker.weapon.surgeAbilities.Length)
            {
                surgeList[i].transform.GetChild(1).GetComponent<Text>().text = surgeTexts[attacker.weapon.surgeAbilities[i]];
                surgeFunction[i] = attacker.weapon.surgeAbilities[i];
                surgeList[i].transform.gameObject.SetActive(true);
            }
            else
            {
                surgeList[i].transform.gameObject.SetActive(false);
            }
        }
        modifyDice.transform.gameObject.SetActive(false);
        useSurge.transform.gameObject.SetActive(true);
        tog = new bool[5];
    }

    bool[] tog;

    public void usedSurge(int i)
    {
        if (tog[i])
        {
            surge++;
            tog[i] = false;
        }
        else
        {
            surge--;
            tog[i] = true;
        }
        surgeText.text = string.Format(surge + " Surge");

        if (surge <= 0)
        {
            foreach (Toggle t in surgeList)
            {
                if (!t.isOn)
                    t.enabled = false;
            }
        }
        else
        {
            foreach (Toggle t in surgeList)
            {
                if (!t.isOn)
                    t.enabled = true;
            }
        }
    }

    public void gatherSurge()
    {
        useSurge.transform.gameObject.SetActive(false);
        surgePanel.SetActive(false);
        selectedToggles = new List<int>();

        for (int i = 0; i < surgeList.Length; i++)
        {
            if (surgeList[i].isOn)
                selectedToggles.Add(i);
        }

        foreach (int i in selectedToggles)
        {
            applySurge(surgeFunction[i]);

            attackResults.text = string.Format(accuracy + " Accuracy, " + damage + " Hit, " + surge + " Surge");

            if (accuracy >= attacker.attackRange)
                mainText.text = string.Format("Attack Hit!");
            else
                mainText.text = string.Format("Range: " + attacker.attackRange + " Accuracy: " + accuracy);

            if (dodge > 0)
                defenseResults.text = string.Format("Attack Dodged!");
            else
                defenseResults.text = string.Format(block + " Block, " + evade + " Evade");
        }

        for (int i = 0; i < surgeList.Length; i++)
            surgeList[i].isOn = false;

        confirm.interactable = true;
    }

    void applySurge(int i)
    {
        /// 0 - +1 dmg        /// 1 - +2 dmg        /// 2 - +3 dmg
        /// 3 - cleave 1      /// 4 - cleave 2      /// 5 - blast 1
        /// 6 - blast 2       /// 7 - pierce 1      /// 8 - pierce 2
        /// 9 - +1 acc        /// 10 - +2 acc       /// 11 - +3 acc
        /// 12 - bleed        /// 13 - stun         /// 14 - weaken
        /// 15 - +1 dmg, +2 acc        /// 16 - +1 dmg, peirce 1
        /// 17 - +1 dmg, bleed         /// 18 - pierce 1, bleed
        /// 19 - pierce 2, bleed       /// 20 - +1 acc, pierce 1
        /// 21 - +2 acc, pierce 1      /// 22 - +1 acc, weaken
        /// 
        switch (i)
        {
            case 0:
                damage += 1;
                break;
            case 1:
                damage += 2;
                break;
            case 2:
                damage += 3;
                break;
            case 3:
                //cleave(1);
                break;
            case 4:
                //cleave(2);
                break;
            case 5:
                //blast(1);
                break;
            case 6:
                //blast(2);
                break;
            case 7:
                block -= 1;
                if (block < 0)
                    block = 0;
                break;
            case 8:
                block -= 2;
                if (block < 0)
                    block = 0;
                break;
            case 9:
                accuracy += 1;
                break;
            case 10:
                accuracy += 2;
                break;
            case 11:
                accuracy += 3;
                break;
            case 12:
                //bleed;
                break;
            case 13:
                //stun;
                break;
            case 14:
                //weaken;
                break;
            case 15:
                damage += 1;
                accuracy += 2;
                break;
            case 16:
                damage += 1;
                block -= 1;
                if (block < 0)
                    block = 0;
                break;
            case 17:
                damage += 1;
                //bleed
                break;
            case 18:
                block -= 1;
                if (block < 0)
                    block = 0;
                //bleed
                break;
            case 19:
                block -= 2;
                if (block < 0)
                    block = 0;
                //bleed
                break;
            case 20:
                accuracy += 1;
                block -= 1;
                if (block < 0)
                    block = 0;
                break;
            case 21:
                accuracy += 2;
                block -= 1;
                if (block < 0)
                    block = 0;
                break;
            case 22:
                accuracy += 1;
                //weaken
                break;
        }
    }

    public void confrimAttack()
    {
        StartCoroutine(endAttack());
    }

    IEnumerator endAttack()
    {
        confirm.transform.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        mainText.transform.gameObject.SetActive(false);
        attackResults.transform.gameObject.SetActive(false);
        defenseResults.transform.gameObject.SetActive(false);

        foreach (Image i in attackDice)
            i.color = Color.clear;

        foreach (Image i in defDice)
            i.color = Color.clear;

        if (attacker.attackRange > 1 && accuracy < attacker.attackRange)
        {
            finalText.text = string.Format("Attack Missed");
            finalText.transform.gameObject.SetActive(true);
        }
        else if (dodge == 0)
        {
            if (block < 0)
                block = 0;
            int totalDmg = damage - block;
            if (totalDmg < 0)
                totalDmg = 0;

            finalText.text = string.Format(defender.name + "\nSuffers " + totalDmg + " Damage");
            finalText.transform.gameObject.SetActive(true);
            
            for (int i = 0; i < totalDmg; i++)
            {
                defender.health--;
                if (attacker.unit < 6)
                    defenderImg.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = defender.health.ToString();
                else
                    heroDefender.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = GSC.data.enemyTarget.GetComponent<Unit>().health.ToString();
                yield return new WaitForSeconds(.5f);
                // add audio to tick health
            }

            defender.GetComponent<Unit>().CheckForDeathEnemy(GSC);

        }
        else
        {
            finalText.text = string.Format("Attack Dodged");
            finalText.transform.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2);

        finalText.transform.gameObject.SetActive(false);

        GSC.gameUI.transform.GetChild(10).gameObject.SetActive(false);

        if (attacker.actions <= 0 && (attacker.movement <= 0 || attacker.movement == attacker.MaxMovemment))
            GSC.ChangeState<PlayerEndState>();
        else
            findNextState();
    }

    private void findNextState()
    {
        if (attacker.name.Equals("Diala"))
        {
            GSC.ChangeState<DialaState>();
        }
        if (attacker.name.Equals("Fenn"))
        {
            GSC.ChangeState<FennState>();
        }
        if (attacker.name.Equals("Gaarkhan"))
        {
            GSC.ChangeState<GaarkhanState>();
        }
        if (attacker.name.Equals("Gideon"))
        {
            GSC.ChangeState<GideonState>();
        }
        if (attacker.name.Equals("Jyn"))
        {
            GSC.ChangeState<JynState>();
        }
        if (attacker.name.Equals("Mak"))
        {
            GSC.ChangeState<MakState>();
        }
    }
}