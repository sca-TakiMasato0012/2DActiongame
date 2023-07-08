using System;
using UnityEngine;
using UnityEngine.Events;

public class SearchArea : MonoBehaviour
{
    private bool isSearchEnter, isSearchStay, isSearchExit;
    public bool isSearching;
    public GameObject player;

    public bool IsSearching()
    {
        if(isSearchEnter || isSearchStay)
        {
            isSearching = true;
        }
        if (isSearchExit)
        {
            isSearching = false;
        }

        isSearchEnter = false;
        isSearchStay = false;
        isSearchExit = false;

        return isSearching;
            
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearchEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearchStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearchExit = true;
        }
    }
}