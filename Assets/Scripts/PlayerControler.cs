using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
   // public Transform bala;
    public Transform PuntoDisparo;  // desde donde sale la bala
    public bullet Bullet; // img bala
    bool  canJump;

    public float puntosVidaPlayer; 
    public float vidaMaxPlayer = 3;
    public Image barraDeVida;
    // Start is called before the first frame update
    void Start()
    {
        puntosVidaPlayer = vidaMaxPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        barraDeVida.fillAmount = puntosVidaPlayer / vidaMaxPlayer;

        //UNA FORMA DE HACERLO//
       /* gameObject.transform.position = new Vector3(gameObject.transform.position.x+3f * Time.deltaTime , gameObject.transform.position.y, gameObject.transform.position.z); */

       //FORMA ABREVIADA//
       /*gameObject.transform.Translate(2f * Time.deltaTime, 0, 0);*/

        //COMPROBAR EN CADA FRAME SI ESTAMOS PULSANDO UNA TECLA O NO//
      /*  if (Input.GetKey("left")) {
            gameObject.transform.Translate(-2f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("right")) {
            gameObject.transform.Translate(2f * Time.deltaTime, 0, 0);
        }

    ManageJump();*/

    if (Input.GetKey("a")) 
    {
        gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(-800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("jumping", false);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
       //gameObject.GetComponent <SpriteRenderer>().flipX = true;
       transform.eulerAngles = new Vector3 (0,180, 0);
        
    }
   

    if (Input.GetKey("d")) 
    {
        gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("jumping", false);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
        //gameObject.GetComponent <SpriteRenderer>().flipX = false;
        transform.eulerAngles = new Vector3 (0,0, 0);
    }
    
    if (Input.GetKeyDown ("w") && canJump) {
        canJump = false;
        gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(0, 500f));
        
    }

    if (Input.GetKey ("space") && canJump) {
        canJump = false;
        gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(0, 500f));
        gameObject.GetComponent <Animator>().SetBool("jumping", true);
    }

    if (Input.GetKey("f")) {
        gameObject.GetComponent <Animator>().SetBool("shoot", true);
        
    }

    if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKeyDown("w") && !Input.GetKey ("space") && canJump) {
         gameObject.GetComponent <Animator>().SetBool("mooving", false);
         gameObject.GetComponent <Animator>().SetBool("jumping", false);
        

    if (Input.GetKeyDown ("f")) {
        Instantiate(Bullet, PuntoDisparo.position, transform.rotation); // crea objeto en base a la rotacion
    }
    }

    }

    

//SOLO SALTA CUANDO TOCA EL PISO
    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.transform.tag =="ground") {
            canJump = true;
            
        }
    }

    public void TakeHit (float golpe) {
        puntosVidaPlayer -= golpe;
       if (puntosVidaPlayer <=0) {
            Destroy(gameObject);
        }


    }

    /*void ManageJump() {
        if (gameObject.transform.position.y <= 0) {
            canJump = true;
        }

        if (Input.GetKey("up") && canJump && gameObject.transform.position.y < 20) {
            gameObject.transform.Translate(0, 10f * Time.deltaTime, 0);
        }
        else {
            canJump = false;
        }

        if (gameObject.transform.position.y > 0){
            gameObject.transform.Translate(0, -5f * Time.deltaTime, 0);
        }
    }*/

   
}
