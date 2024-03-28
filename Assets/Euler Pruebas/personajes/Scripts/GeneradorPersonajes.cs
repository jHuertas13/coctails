using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPersonajes : MonoBehaviour
{
    public List<Rasgo> rasgos;
    public GameObject puntoSpawn; // Transform que indica la posici�n donde aparecer� el objeto generado

    void Start()
    {
        Generar();
    }

    public void Generar()
    {
        List<int> ordenRasgo = new List<int>();
        foreach (Rasgo rasgo in rasgos)
        {
            ordenRasgo.Add(elegir(rasgo));
        }

        Cliente persona = new Cliente(ordenRasgo);
    }
    int elegir(Rasgo lista)
    {
        int random = Random.Range(0, lista.opciones.Count);
        GameObject listaObjeto = puntoSpawn.transform.Find(lista.name).gameObject;
        SpriteRenderer listaRender = listaObjeto.GetComponent<SpriteRenderer>();

        // Crear el sprite
        Sprite sprite = Sprite.Create(lista.opciones[random], new Rect(0, 0, lista.opciones[random].width, lista.opciones[random].height), Vector2.one);
        listaRender.sprite = sprite;

        // Centrar el sprite en el objeto vac�o hijo
        Vector3 spriteSize = sprite.bounds.size;
        Vector3 offset = new Vector3(spriteSize.x / 2, spriteSize.y / 2, 0f); // Calcula el offset para centrar el sprite
        listaObjeto.transform.position += offset; // Aplica el offset al objeto vac�o hijo

        return random;
    }
}
