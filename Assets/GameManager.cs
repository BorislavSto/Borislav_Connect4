using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public enum Turn
    {
        red,
        yellow
    }

    public GameObject WinScreen;

    public Turn turn;
    public GameObject playerRed;
    public GameObject playerYellow;

    public float waitTime = 2;
    public bool doneWaiting = false;

    public int boardHeight = 6;
    public int boardWidth = 7;
    int[,] boardState; // 0 = null, 1 = playerRed, 2 = playerYellow

    void Start()
    {
        turn = Turn.red;

        boardState = new int[boardWidth, boardHeight];
    }
    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            doneWaiting = true;
        }       
    }
    public void SwitchTurn()
    {
        if (turn == Turn.red)
        {
            turn = Turn.yellow;
        }
        else
            turn = Turn.red;
    }

    public void TakeTurn(GameObject colum)
    {
        if (turn == Turn.red) 
        {
            Instantiate(playerRed, colum.gameObject.transform.position, Quaternion.identity);
            if (DidWin(1))
            {
                Debug.Log("Player Red Won");
                WinScreen.SetActive(true);
                WinScreen.GetComponentInChildren<Text>().text = "Player One Wins";
            }
        }
        else
        {
            Instantiate(playerYellow, colum.gameObject.transform.position, Quaternion.identity);
            if (DidWin(2))
            {
                Debug.Log("Player Yellow Won");
                WinScreen.SetActive(true);
                WinScreen.GetComponentInChildren<Text>().text = "Player Two Wins";
            }
        }

        if (Draw())
        {
            Debug.Log("Draw");
        }
    }

    public void SelectedColum(int colum)
    {
        Debug.Log("test message in gm" + colum);
    }

    public void UpdateBoardState(int colum)
    {
        for(int row = 0; row < boardHeight; row++)
        {
            if (boardState[colum, row] == 0)
            {
                if (turn == Turn.red)
                {
                    boardState[colum, row] = 1;
                    Debug.Log("red got this one");
                }
                else
                {
                    boardState[colum, row] = 2;
                    Debug.Log("yellow got this one");
                }
                Debug.Log("Orb being spawned at " + colum + ", " + row);
                break;
            }
        }
    }


    //coppied from video :\
    bool DidWin(int playerNum)
    {
        //Horizontal
        for (int x = 0; x < boardWidth - 3; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                if ((boardState[x, y] == playerNum && boardState[x + 1, y] == playerNum && boardState[x + 2, y] == playerNum && boardState[x + 3,y] == playerNum))
                    {
                    return true;
                }
            }
        }
        //Vertical
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                if ((boardState[x, y] == playerNum && boardState[x, y + 1] == playerNum && boardState[x, y + 2] == playerNum && boardState[x, y + 3] == playerNum))
                {
                    return true;
                }
            }
        }
        //x+y line
        for (int x = 0; x < boardWidth - 3; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                if ((boardState[x, y] == playerNum && boardState[x + 1, y + 1] == playerNum && boardState[x + 2, y + 2] == playerNum && boardState[x + 3, y + 3] == playerNum))
                {
                    return true;
                }
            }
        }
        //Addition, checking for right to left x+y line
        for (int x = 0; x < boardWidth - 3; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                if ((boardState[x, y + 3] == playerNum && boardState[x + 1, y + 2] == playerNum && boardState[x + 2, y + 1] == playerNum && boardState[x + 3, y] == playerNum))
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool Draw()
    {
        for(int x = 0; x < boardWidth; x++)
        {
            if (boardState[x, boardHeight - 1] == 0)
            {
                return false;
            }
        }
        return true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
