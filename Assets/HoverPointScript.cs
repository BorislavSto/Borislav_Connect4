using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HoverPointScript : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject Arrow;
    public int spawnedOrbs;

    [SerializeField]
    int colum;

    float newWaitTime = 2;
    private void OnMouseOver()
    {

        //will show an arrow over the location over which the player can play
        Arrow.transform.position = this.transform.position + this.transform.up * 2;
        //===================================================================

        if (Input.GetMouseButtonDown(0) && gameManager.doneWaiting && spawnedOrbs < 6)
        {
        //sends the colums number to the game manager
        gameManager.SelectedColum(colum);
        //===========================================
        gameManager.UpdateBoardState(colum);
            gameManager.TakeTurn(this.gameObject);
            gameManager.SwitchTurn();
            gameManager.doneWaiting = false;
            gameManager.waitTime = newWaitTime;
            spawnedOrbs++; // current stop from playing more than 6 in a row
        }
    }

    private void OnMouseExit()
    {
        Arrow.transform.position = this.transform.position + this.transform.up * -10;
    }
}
