using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;//Player a ses kayna�� eklemeliyiz klipleri �almak i�in
    public float jumpForce = 3f;
    public float gravityModifier;
    public bool isGrounded = true;
    public bool gameOver = false;

    public ParticleSystem explotionParticle;//Patlama efektini atad�k
    public ParticleSystem dirtParticle;//Toprak efekti

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public bool doublejump = true;
    public bool dash = false;
    private int score;
    //Score u burda yapt�m acan �neri olarak GameManager script olu�turup orda hesaplamak
    //En a�a��ya koydum
    public bool isplayerReady=false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;//Yer �ekimini de�i�tirdik
        playerAnim = GetComponent<Animator>();//Animasyonlara eri�mek i�in referans ald�k
        playerAudio = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isplayerReady)
        {
            GameStart();//Ba�lang��ta playerin y�r�yerek gelmesi i�in yapt�k
        }else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);//ForceMode.Impulse an�nda g�� uygular
                isGrounded = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();//Z�plarken toprak animasyonu �al��mamal�
                playerAudio.PlayOneShot(jumpSound, 1f);//Z�plama sesini �ald�k
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !gameOver && doublejump)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doublejump = false;
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound, 1f);
                dirtParticle.Stop();
            }
            if (!gameOver)
            {
                if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !gameOver)
                {
                    dash = true;
                    playerAnim.SetFloat("Speed_f", 5.0f);
                    score += 2;
                }
                else
                {
                    dash = false;
                    playerAnim.SetFloat("Speed_f", 1.5f);
                    score++;
                }
               
            }

        }



    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtParticle.Play();//Yerdeyeken �al��acak
            doublejump=true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explotionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound,1f);
            Debug.Log("Score : " + score);

        }

    }
    void GameStart()
    {
        if (transform.position.x < 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2.0f);
        }
        else
        {
            playerAnim.SetFloat("Speed_f", 1.5f);
            isplayerReady = true;
        }
    }
    //public float score;
    //private PlayerController playerControllerScript;
    //void Start()
    //{
    //    playerControllerScript =
    //    GameObject.Find("Player").GetComponent<PlayerController>();
    //    score = 0;
    //}
    //void Update()
    //{
    //    if (!playerControllerScript.gameOver)
    //    {
    //        if (playerControllerScript.doubleSpeed)
    //        {
    //            score += 2;
    //        }
    //        else
    //        {
    //            score++;
    //        }
    //        Debug.Log("Score: " + score);
    //    }
    //}
    //Bu �ekilde yaparak kullan�lmas� �nerilmi�
}
