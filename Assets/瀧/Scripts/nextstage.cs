using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //Textを使うため追加
using UnityEngine.SceneManagement;  //Sceneを切り替えるために追加

public class nextstage : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			SceneManager.LoadScene("Stage2");
		}
	}
}