using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static bool playerMoved = false;
    public static bool enemyCanMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool PlayerHasMoved(bool moveEnemy)
    {
        if (playerMoved == true)
        {
            bool enemyCanMove = true;
        }
        else moveEnemy = false;

        return moveEnemy;
    }
}
