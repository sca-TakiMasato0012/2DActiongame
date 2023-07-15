﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float dmgValue = 4;
	
	public Transform attackCheck;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool canAttack2 = true;
	public bool canAttack3 = true;

	public GameObject ya;
	public GameObject ya2;

	[SerializeField]
	private float kenCooldown = 0;//剣のクールタイム
	[SerializeField]
	private float yaCooldown = 0;//弓のクールタイム
	[SerializeField]
	private float yaCooldown2 = 0;//弓のクールタイム

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.X) && canAttack)//剣を振る　アニメーションを再生
		{
			canAttack = false;
			animator.SetBool("IsAttacking", true);
			StartCoroutine(AttackCooldown());
		}

		if (Input.GetKeyDown(KeyCode.V) && canAttack2)//矢を放つ
		{
			canAttack2 = false;
			Instantiate(ya,transform.position,transform.rotation);
			StartCoroutine(AttackCooldown2());
		}
		//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
		//if (Input.GetKeyDown(KeyCode.F) && canAttack3)//矢を放つ
		//{
			//canAttack2 = false;
			//Instantiate(ya2, transform.position, transform.rotation);
			//StartCoroutine(AttackCooldown2());
		//}\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
	}

	IEnumerator AttackCooldown()//剣のクールタイム
	{
		yield return new WaitForSeconds(kenCooldown);
		canAttack = true;
		canAttack2 = true;
	}

	IEnumerator AttackCooldown2() 
	{

		yield return new WaitForSeconds(yaCooldown);//矢のクールタイム

		canAttack2 = true;
	}

	IEnumerator AttackCooldown3()
	{

		yield return new WaitForSeconds(yaCooldown);//矢のクールタイム

		canAttack3 = true;
	}

	public void DoDashDamage()//敵に与えるダメージ
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
				//cam.GetComponent<CameraFollow>().ShakeCamera();
			}
		}
	}
	
}