using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Bejye : MonoBehaviour
{
    // Time to move from sunrise to sunset position, in seconds.
    [SerializeField]
    private float Arrow_speed;
    private float Arrow_destroy = 5.0f;

    public GameObject objectToGenerate; // 生成するオブジェクト
    public Transform spawnPoint; // 生成する位置


    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, Arrow_destroy);
    }
    // Update is called once per frame
    void Update()
    {
        

        transform.position += new Vector3(1,2,0) * Arrow_speed * Time.deltaTime;

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ArrowCheck") 
        {

            Destroy(gameObject);
            Debug.Log("あたった！");

            GenerateObject();
        }
        
    }
    void GenerateObject()
    {
        // オブジェクトを生成
        GameObject newObject = Instantiate(objectToGenerate, spawnPoint.position, spawnPoint.rotation);

        // 生成したオブジェクトに対する追加の処理を行うことができます
        // 例えば、生成後に新しいオブジェクトに力を加えたり、スクリプトをアタッチしたりできます
    }
}