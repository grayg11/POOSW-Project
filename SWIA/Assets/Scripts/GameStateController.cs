using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : StateMachine
{
    public TileMap map;
    public MapGen generator;
    public Pathfinding path;
    public GameObject rig;
    public Camera cam;
    public Canvas gameUI;
    public DataManager data;
    public List<GameObject> heroes;
    public List<GameObject> enemies;
    public int[] activated;
    public Dictionary<string, int> items;
    public bool attacking;
    public bool moving;
    public Unit enemyAttack;
    public GameObject selectedUnit;
    public int gameType;
    public int difficulty;

    public CombatController cControl;

    void Start()
    {
        activated = new int[4];
        heroes = new List<GameObject>();
        enemies = new List<GameObject>();
        // remove later
        map = FindObjectOfType<TileMap>();
        cControl = FindObjectOfType<CombatController>();

        items = new Dictionary<string, int>();
        items.Add("medpack", 1);
        gameType = mainmenu.gameType;
        //gameType = 1;
        difficulty = mainmenu.gameDifficulty;
        
        ChangeState<InitState>();
    }

    void Update()
    {

    }

}
