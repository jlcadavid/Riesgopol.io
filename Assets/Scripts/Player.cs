using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController gameController;
    public Route currentRoute;

    int routePosition = 0;

    bool turnOn = false;
    bool isMoving;

    public Player(GameController gameController)
    {
        this.gameController = gameController;
    }

    void Update()
    {
        if (turnOn && Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            StartCoroutine(Move(RollDice()));
        }
    }

    IEnumerator Move(int steps)
    {
        Debug.Log("Dice rolled: " + steps);

        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps > 0)
        {
            routePosition++;
            routePosition %= currentRoute.childNodeList.Count;

            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            while(MoveToNextNode(nextPos)){yield return null;}

            yield return new WaitForSeconds(0.05f);
            steps--;
        }

        isMoving = false;
        
        EndTurn();
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 24f * Time.deltaTime));
    }

    int RollDice()
    {
        return Random.Range(1,7);
    }

    void EndTurn()
    {
        gameController.EndTurnCallback(this);
    }

    public void SetTurn(bool value)
    {
        turnOn = value;
    }
}
