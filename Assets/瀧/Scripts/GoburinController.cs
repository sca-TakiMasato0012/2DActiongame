using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoburinController : MonoBehaviour
{
    public SearchArea searchArea;

    private bool isSearchArea = false;
    void Start()
    {
//        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isSearchArea = searchArea.IsSearching();

        if(isSearchArea)
        {
            Debug.Log("はいってるよ");
        }
    }
}
