using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemyMainState : GameState
{
    Unit player;
    GameObject uiGO;
    GameObject target = null;
    bool turnFinish = false;

    public override void Enter()
    {
        Debug.Log("Entered Enemy main state");

        StartCoroutine(CheckForGameWin());

    }

    public override void Update()
    {
        if (turnFinish)
            changeTo();
    }

    public override void Exit()
    {
        uiGO.SetActive(false);
        Destroy(this);
    }

    void changeTo()
    {
        Debug.Log("Leaving Enemy Main State");

        uiGO.SetActive(false);

        turnFinish = false;

        owner.ChangeState<PlayerMainState>();
    }

    void chooseTarget()
    {
        // Choose closet target
        int min = 50;
        for (int i = 0; i < owner.heroes.Count; i++)
        {
            GameObject hero = owner.heroes[i];
            if (owner.activated[i] != -1)
            {
                Debug.Log((int)hero.transform.position.x + " , " + (int)hero.transform.position.y);
                owner.path.GeneratePathTo(Mathf.RoundToInt(hero.transform.position.x), Mathf.RoundToInt(hero.transform.position.y), false, false);
                if (player.currentPath.Count < min)
                {
                    min = player.currentPath.Count;
                    owner.data.enemyTarget = hero;
                    // Sets hero in GSC for combat
                    owner.enemyAttack = owner.data.enemyTarget.GetComponent<Unit>();
                }
            }

        }

        Debug.Log(player.name + " targets " + owner.data.enemyTarget.name);

    }

    void findMoveSpot()
    {
        Debug.Log("current position x = " + player.transform.position.x + " y = " + player.transform.position.y);

        // Set movement range
        int moveSpaces = player.MaxMovemment;
        player.movement = moveSpaces;

        // Generate path to target
        owner.path.GeneratePathTo(Mathf.RoundToInt(owner.data.enemyTarget.transform.position.x), Mathf.RoundToInt(owner.data.enemyTarget.transform.position.y), false, false);

        // Trim path to fit movement speed/distance to target
        if (player.currentPath.Count == 2 || player.currentPath.Count == 1)
            return;

        if (player.currentPath.Count <= moveSpaces)
            player.currentPath = player.currentPath.GetRange(0, player.currentPath.Count - 1);
        else
            player.currentPath = player.currentPath.GetRange(0, moveSpaces + 1);

        // Make the move (hopefully)
        player.NextTurnEnemy();
        SetCamera();

        Debug.Log("new position x = " + player.transform.position.x + " y = " + Mathf.RoundToInt(player.transform.position.y));


    }

    IEnumerator SlowEnemyPhase()
    {
        owner.gameUI.transform.GetChild(6).gameObject.SetActive(true);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(2);


        owner.gameUI.transform.GetChild(6).gameObject.SetActive(false);

        StartCoroutine(SlowEnemyCamera());
    }

    IEnumerator SlowEnemyAttack()
    {
        owner.gameUI.transform.GetChild(7).gameObject.SetActive(true);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(2);

        owner.gameUI.transform.GetChild(7).gameObject.SetActive(false);

        AttackPlayer();
    }

    void StartEnemyPhase()
    {
        chooseTarget();
        findMoveSpot();

        owner.path.GeneratePathTo(Mathf.RoundToInt(owner.data.enemyTarget.transform.position.x), Mathf.RoundToInt(owner.data.enemyTarget.transform.position.y), false, false);

        if (player.currentPath.Count <= 10)
            StartCoroutine(SlowEnemyAttack());
        else
            StartCoroutine(SlowEndPlayerTurn());
    }

    IEnumerator SlowEnemyCamera()
    {
        // Reset enemy turn counter
        if (owner.data.enemyTurnCount > owner.enemies.Count - 1)
            owner.data.enemyTurnCount = 0;

        // choose the player
        owner.selectedUnit = owner.enemies[owner.data.enemyTurnCount]; //owner.enemies[(int)Random.Range(0, owner.enemies.Count - 1)];
        player = owner.selectedUnit.GetComponent<Unit>();

        // Increment enemy turn counter
        owner.data.enemyTurnCount++;

        // Set UI
        uiGO = owner.gameUI.transform.GetChild(5).gameObject;
        uiGO.GetComponent<Image>().sprite = player.playerCard;
        uiGO.SetActive(true);

        //Set Camera
        SetCamera();

        yield return new WaitForSeconds(1);

        StartEnemyPhase();
    }

    void AttackPlayer()
    {
        /*cam.GetComponent<AudioSource>().Play();

        int diceRoll = player.weapon.weaponDice[0] + player.weapon.weaponDice[1];
        // eventually call combatcontroller initicombat()
        owner.data.enemyTarget.GetComponent<Unit>().health -= diceRoll;
        owner.data.enemyTarget.GetComponent<Unit>().CheckForDeath(owner.gameUI.transform.GetChild(1).gameObject, owner);*/

        
        player.attackRange = player.currentPath.Count - 1;

        owner.cControl.initCombat();

        StartCoroutine(SlowEndPlayerTurn());

        return;
    }

    void SetCamera()
    {
        Vector3 playerPos = new Vector3(owner.selectedUnit.transform.position.x, owner.selectedUnit.transform.position.y, owner.rig.transform.position.z);
        owner.rig.GetComponent<MyCamera>().newPosition = playerPos;
    }

    IEnumerator SlowEndPlayerTurn()
    {
        owner.gameUI.transform.GetChild(8).gameObject.SetActive(true);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(2);

        owner.gameUI.transform.GetChild(8).gameObject.SetActive(false);
        turnFinish = true;
    }

    IEnumerator CheckForGameWin()
    {
        yield return new WaitForSeconds(2);
        if (owner.enemies.Count == 0)
            SceneManager.LoadScene("YouWin");

        StartCoroutine(SlowEnemyPhase());
    }
}

