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

    public float velCorrer;
    public float velSaltar = 3;
    Rigidbody2D rb2D;

    public float puntosVidaPlayer; 
    public float vidaMaxPlayer = 3;
    public Image barraDeVida;

    public Image energia;
    public int cantEnergia;
    public RectTransform posPrimerBarrita; // transform dentro del canvas para manjear ui
    public Canvas MyCanvas;  // para dibujar mas energia (hacer hijos)
    public int Offset; // donde dibujar las barritas
 

    void Start()
    {
        puntosVidaPlayer = vidaMaxPlayer;
        rb2D = GetComponent<Rigidbody2D>(); // mete el componente rigidbody dentro de la variable


    // QUE ARRANQUE CON 8 BARRITAS DE ENTRADA
        for (int i = 0; i < cantEnergia; i++)
        
        {
            //crea una var llamado newenergia. es una instancia de energia  y la ubica en la primera posicion de la barra
            Image NewEnergia = Instantiate(energia,posPrimerBarrita.position, Quaternion.identity );

                NewEnergia.transform.parent = MyCanvas.transform;
                posPrimerBarrita.position = new Vector2 (posPrimerBarrita.position.x , posPrimerBarrita.position.y + Offset);
        }

    }

    void Update()
    {
        barraDeVida.fillAmount = puntosVidaPlayer / vidaMaxPlayer;

    if (Input.GetKey("a")) 
    {
        rb2D.velocity = new Vector2 (-velCorrer, rb2D.velocity.y); // en que direccion ir (eje y se queda como esta)
        //gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(-800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
       //gameObject.GetComponent <SpriteRenderer>().flipX = true;
       transform.eulerAngles = new Vector3 (0,180, 0); // para voltear al personaje
        
    }
   

    if (Input.GetKey("d")) 
    {
         rb2D.velocity = new Vector2 (velCorrer, rb2D.velocity.y); // en que direccion ir (eje y se queda como esta)
       // gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(800f * Time.deltaTime, 0));
        gameObject.GetComponent <Animator>().SetBool("mooving", true);
        gameObject.GetComponent <Animator>().SetBool("shoot", false);
        //gameObject.GetComponent <SpriteRenderer>().flipX = false;
        transform.eulerAngles = new Vector3 (0,0, 0); // para voltear al personaje
    }
    

   /* if (Input.GetKey ("space") && canJump) {
        canJump = false;
        rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);
        //gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(0, velSaltar));
       // gameObject.GetComponent <Animator>().SetBool("jumping", true);
        gameObject.GetComponent <Animator>().SetBool("mooving", false);
    }*/
      if (Input.GetKey ("space")  && tocaPlataforma.enPlat )
    {
        rb2D.velocity = new Vector2 (rb2D.velocity.x, velSaltar);
        gameObject.GetComponent <Animator>().SetBool("mooving", false);
    }

    if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKeyDown("w") && !Input.GetKey ("space") ) { // esto es para que las animaciones no sigan funcionando cuando se dejan d presionar las teclas
         gameObject.GetComponent <Animator>().SetBool("mooving", false);
         gameObject.GetComponent <Animator>().SetBool("jumping", false);
           
    }


    if (Input.GetKey("mouse 0")) { //dispara con el mouse
        gameObject.GetComponent <Animator>().SetBool("shoot", true);

        
    }
   
    if (Input.GetKeyDown ("mouse 0")) { //dispara con el mouse
        if (cantEnergia > 0 ) { 
             Instantiate(Bullet, PuntoDisparo.position, transform.rotation);// crea objeto en base a la rotacion           
             Destroy(MyCanvas.transform.GetChild(cantEnergia + 1).gameObject);
                cantEnergia -= 1;
            posPrimerBarrita.position = new Vector2 (posPrimerBarrita.position.x , posPrimerBarrita.position.y - Offset); // cuando se elimina una barrita, tambien se elimina su posicion. Esto es para que las nuevas se dibujen a partir de esa ultima que se elimino
                
        } 
       
    }

    if (cantEnergia <=0) { //si no tiene energia, no dispara
        Destroy(energia);
       
    }
    
    }

    

//SOLO SALTA CUANDO TOCA EL PISO 
  /* private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.transform.tag =="ground" ) {
            canJump = true;
            
        }
        /*if (collision.transform.tag =="platform" ) {
            canJump = true;
            
        }
           
    }*/


    //ESTO NO SE Ni COMO FUNCA LA VERDAD 
   private void OnTriggerEnter2D (Collider2D col) { //cuando collider entra en contacto con otro collider

   Transform posBarrita = posPrimerBarrita; 
   int cantEnergiaRecogida = 1;


        //JUNTAR BALAS (ENERGIA)
       if (col.gameObject.tag == "balas" && cantEnergia < 8 ) {
              Destroy(col.gameObject);
    
              cantEnergia +=1;
    
        for (int i = 0; i < cantEnergiaRecogida; i++)
        
        {
            //crea una var llamado newenergia. es una instancia de energia  y la ubica en la primera posicion de la barra
            Image NewEnergia = Instantiate(energia, posBarrita.position, Quaternion.identity );

                NewEnergia.transform.parent = MyCanvas.transform;
                posBarrita.position = new Vector2 (posBarrita.position.x , posBarrita.position.y + Offset);
        }
           
        
        } 
       
    }

    public void TakeHit (float golpe) {
        puntosVidaPlayer -= golpe;
       if (puntosVidaPlayer <=0) {
            Destroy(gameObject);
        }


    }



   
}
