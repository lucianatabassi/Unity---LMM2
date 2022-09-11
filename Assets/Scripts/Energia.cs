using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energia : MonoBehaviour
{
    public Image energia;
    public int cantEnergia;
    public RectTransform posPrimerBarrita; // transform dentro del canvas para manjear ui
    public Canvas MyCanvas;  // para dibujar mas energia (hacer hijos)
    public int Offset; // donde dibujar las barritas

    void Start()
    {
        Transform posBarrita = posPrimerBarrita; 

        for (int i = 0; i < cantEnergia; i++)
        {
            //crea una var llamado newenergia. es una instancia de tipo energia  y la ubica en la primera posicion de la barra
            Image NewEnergia = Instantiate(energia, posBarrita.position, Quaternion.identity );
              NewEnergia.transform.parent = MyCanvas.transform;
              posBarrita.position = new Vector2 (posBarrita.position.x , posBarrita.position.y + Offset);
        }
    }

    
    void Update()
    {
        
    }
}
