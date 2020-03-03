using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameState : State
{
    protected GameStateController owner;
    public Camera cam { get { return owner.cam; } }
    public TileMap map { get { return owner.map; } }
    public Canvas gameUI { get { return owner.gameUI; } }

    protected virtual void Awake()
    {
        owner = GetComponent<GameStateController>();
    }

    protected override void AddListeners()
    {
        //InputController.moveEvent += OnMove;
        //InputController.fireEvent += OnFire;
    }

    protected override void RemoveListeners()
    {
        //InputController.moveEvent -= OnMove;
        //InputController.fireEvent -= OnFire;
    }

    //protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
    //{

    //}

    //protected virtual void OnFire(object sender, InfoEventArgs<int> e)
    //{

    //}

}