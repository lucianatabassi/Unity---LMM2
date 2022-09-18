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

    public Image energia;
    public int cantEnergia;
    public RectTransform posPrimerBarrita; // transform dentro del canvas para manjear ui
    public Canvas MyCanvas;  // para dibujar mas energia (hacer hijos)
    public int Offset; // donde dibujar las barritas
 

 /*   public GameObject topRightLimitGameObject;
     public GameObject bottomLeftLimitGameObject;
     private Vector3 topRightLimit;
     private Vector3 bottomLeftLimit;*/

    void Start()
    {
        puntosVidaPlayer = vidaMaxPlayer;

       /* topRightLimit = topRightLimitGameObject.transform.position;
        bottomLeftLimit = bottomLeftLimitGameObject.transform.position;*/
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
        gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(0, 400f));
        
    }

    if (Input.GetKey ("space") && canJump) {
        canJump = false;
        gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(0, 600f));
        //gameObject.GetComponent <Animator>().SetBool("jumping", true);
    }

    if (Input.GetKey("f")) {
        gameObject.GetComponent <Animator>().SetBool("shoot", true);

        
    }

    if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKeyDown("w") && !Input.GetKey ("space") && canJump) {
         gameObject.GetComponent <Animator>().SetBool("mooving", false);
         gameObject.GetComponent <Animator>().SetBool("jumping", false);
        

    
    }
    if (Input.GetKeyDown ("f")) {
        if (cantEnergia > 0 ) { 
             Instantiate(Bullet, PuntoDisparo.position, transform.rotation);// crea objeto en base a la rotacion           
             Destroy(MyCanvas.transform.GetChild(cantEnergia + 1).gameObject);
                cantEnergia -= 1;
        } 
       
    }

    if (cantEnergia <=0) { //si no tiene energia, no dispara
        Destroy(energia);
    }
    }

    

//SOLO SALTA CUANDO TOCA EL PISO 
    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.transform.tag =="ground" ) {
            canJump = true;
            
        }

    
    }

   private void OnTriggerEnter2D (Collider2D col) {
    Transform posBarrita = posPrimerBarrita; 

        for (int i = 0; i < cantEnergia; i++)
        {
            //crea una var llamado newenergia. es una instancia de energia  y la ubica en la primera posicion de la barra
            Image NewEnergia = Instantiate(energia, posBarrita.position, Quaternion.identity );

              NewEnergia.transform.parent = MyCanvas.transform;
              posBarrita.position = new Vector2 (posBarrita.position.x , posBarrita.position.y + Offset);
        }
        //JUNTAR BALAS (ENERGIA)
    //ESTO HAY QUE LIMITARLO MEJOR pq el max de balas tiene que ser siempre 8. ponele si le quedan 5 restantes se le tienen q sumar 3 no 8
        if (col.gameObject.tag == "balas") {
              Destroy(col.gameObject);
              cantEnergia +=8;
 
        
        //Debug.Log ("agarra");
        
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
