using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class EnemyClassOld : MonoBehaviour
{
    public bool beingSwitchedByPriest = false;
    public bool isActiveInWorld = true;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("I've hit the player");
            //Destroy(collision.gameObject);

            //string activeSceneName = SceneManager.GetActiveScene().name;
            //SceneManager.LoadScene("_Base_Scene");
        }

        if(collision.tag == "ActivationPriest")
        {
            Debug.Log(gameObject + " has hit the priest");
            beingSwitchedByPriest = true;
        }

        else  beingSwitchedByPriest = true;
    }
}