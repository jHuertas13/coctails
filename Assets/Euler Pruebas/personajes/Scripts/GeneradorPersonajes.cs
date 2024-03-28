using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPersonajes : MonoBehaviour
{
    public Rasgo Nariz;
    public Rasgo Ojos;

    public GameObject puntoSpawn; // Transform que indica la posición donde aparecerá el objeto generado

    void Start()
    {
        Generar();
    }

    public void Generar()
    {
        int ordenNariz = Random.Range(0, Nariz.opciones.Count);
        int ordenOjos = Random.Range(0, Ojos.opciones.Count);

        Cliente persona = new Cliente(ordenNariz, ordenOjos);

        // Obtener los GameObjects de nariz y ojos desde el punto de spawn
        GameObject narizObjeto = puntoSpawn.transform.Find("Nariz").gameObject;
        GameObject ojosObjeto = puntoSpawn.transform.Find("Ojos").gameObject;

        // Obtener los componentes SpriteRenderer de nariz y ojos
        SpriteRenderer narizRenderer = narizObjeto.GetComponent<SpriteRenderer>();
        SpriteRenderer ojosRenderer = ojosObjeto.GetComponent<SpriteRenderer>();

        // Asignar las texturas elegidas a los componentes SpriteRenderer
        narizRenderer.sprite = Sprite.Create(Nariz.opciones[ordenNariz], new Rect(0, 0, Nariz.opciones[ordenNariz].width, Nariz.opciones[ordenNariz].height), Vector2.one * 0.0001f);
        ojosRenderer.sprite = Sprite.Create(Ojos.opciones[ordenOjos], new Rect(0, 0, Ojos.opciones[ordenOjos].width, Ojos.opciones[ordenOjos].height), Vector2.one * 0.01f);
    }
}
