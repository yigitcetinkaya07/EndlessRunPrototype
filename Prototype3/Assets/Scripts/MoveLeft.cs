using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30f;
    private PlayerController playerControllerScript;
    public float leftBound = -15;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //Player i bul onun içinden PlayerController i bul
        //Farklý bir nesnenin üstündeki scripte ulaþmak için kullanabiliriz
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isplayerReady)
        {
            if (!playerControllerScript.gameOver)//Oyun bitmediyse
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
            if (playerControllerScript.dash)
            {
                speed = 60f;
            }
            else
            {
                speed = 30f;
            }
        }
       
     
    }
}
