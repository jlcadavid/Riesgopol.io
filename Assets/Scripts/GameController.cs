using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    int round = 1;
    int playerTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Async send to $steps to back-end and refresh
        //string url = "";
        //List<IMultipartFormSection> formSections = new List<IMultipartFormSection>();
        //Set server url & populate list with player's steps
        //UnityWebRequest.Post(url, formSections);
        //await server response
    }

    void NewTurn(int turnNumber)
    {
        players[turnNumber].SetTurn(true);
        Debug.Log("ROUND: " + round);
        Debug.Log("PLAYER " + (playerTurn + 1) + " TURN!");
    }

    public void EndTurnCallback(Player lastPlayer)
    {
        lastPlayer.SetTurn(false);
        playerTurn++;
        if (playerTurn % players.Count == 0)
        {
            players.ForEach(x => x.SetTurn(false));
            round++;
            playerTurn = 0; 
        }

        NewTurn(playerTurn);
    }

    void StartGame()
    {
        Debug.Log("RIESGOPOL.IO");
        Debug.Log("PLAYERS: " + players.Count);
        NewTurn(playerTurn);
    }

}
