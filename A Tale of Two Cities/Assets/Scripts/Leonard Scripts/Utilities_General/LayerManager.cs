﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public static LayerManager instance;

    public List <GameObject> Entities;
    GameObject player; 

    void Awake()
    {
        if(instance == null)
        {            
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CompareEnemyToPlayerLayer(Entities);
    }

    public static bool PlayerIsInRealWorld()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player.gameObject.layer == LayerMask.NameToLayer("Player Layer 1")) return true;

        if(player.gameObject.layer == LayerMask.NameToLayer("Player Layer 2")) return false;

        else return false;
    }

    public static bool EnemyIsInRealWorld(GameObject enemy)
    {
        if(enemy.gameObject.layer == LayerMask.NameToLayer("Enemy Layer 1")) return true;

        if(enemy.gameObject.layer == LayerMask.NameToLayer("Enemy Layer 2")) return false;

        else return false;
    }

    public void CompareEnemyToPlayerLayer(List<GameObject> EnemyEntities)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        foreach (GameObject Enemy in EnemyEntities)
        {
            if(Enemy.gameObject.layer == LayerMask.NameToLayer("Enemy Layer 1")
            && player.gameObject.layer == LayerMask.NameToLayer("Player Layer 1"))
            {
                //Debug.Log(Enemy.gameObject.name + " and I are on the same layer");
                Enemy.SendMessage("MyLayerChecker", true);
            }
            
            if(Enemy.gameObject.layer == LayerMask.NameToLayer("Enemy Layer 2")
            && player.gameObject.layer == LayerMask.NameToLayer("Player Layer 2"))
            {
                //Debug.Log(Enemy.gameObject.name + " and I are on the same layer");
                Enemy.SendMessage("MyLayerChecker", true);
            }

            if(Enemy.gameObject.layer == LayerMask.NameToLayer("Enemy Layer 1")
            && player.gameObject.layer == LayerMask.NameToLayer("Player Layer 2"))
            {
                //Debug.Log(Enemy.gameObject.name + "and I ain't sharing shit, son");
                Enemy.SendMessage("MyLayerChecker", false);
            }
            
            if(Enemy.gameObject.layer == LayerMask.NameToLayer("Enemy Layer 2")
            && player.gameObject.layer == LayerMask.NameToLayer("Player Layer 1"))
            {
                //Debug.Log(Enemy.gameObject.name + "and I ain't sharing shit, son");
                Enemy.SendMessage("MyLayerChecker", false);
            }
        }
    }
}
