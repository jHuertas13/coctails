using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneradorPersonajes : MonoBehaviour
{
    public List<Rasgo> rasgos;
    public GameObject puntoSpawn; // Transform que indica la posición donde aparecerá el objeto generado
    private Dictionary<string, Transform> hijosPorNombre = new Dictionary<string, Transform>(); // Almacena los hijos por nombre
    private Dictionary<string, Vector3> posicionesRelativas = new Dictionary<string, Vector3>(); // Almacena las posiciones relativas de los hijos
    public List<Cliente> listaClientes = new List<Cliente>(); // Lista de clientes generados
    public Text textoCliente; // Referencia al Text donde se mostrarán los datos del cliente

    void Start()
    {
        ObtenerHijosPorNombre(); // Llama a la función para obtener los hijos por nombre
        GuardarPosicionesRelativas(); // Guarda las posiciones relativas de los hijos
    }

    void ObtenerHijosPorNombre()
    {
        foreach (Transform hijo in puntoSpawn.transform)
        {
            hijosPorNombre[hijo.name] = hijo;
        }
    }

    void GuardarPosicionesRelativas()
    {
        foreach (KeyValuePair<string, Transform> par in hijosPorNombre)
        {
            posicionesRelativas[par.Key] = par.Value.localPosition;
        }
    }

    public void Generar()
    {
        List<int> ordenRasgo = new List<int>();
        HashSet<int> rasgosUsados = new HashSet<int>(); // HashSet para mantener un registro de rasgos usados

        foreach (Rasgo rasgo in rasgos)
        {
            int rasgoElegido = elegir(rasgo);

            // Verificar si el rasgo ya ha sido utilizado en esta identidad
            while (rasgosUsados.Contains(rasgoElegido))
            {
                rasgoElegido = elegir(rasgo);
            }

            ordenRasgo.Add(rasgoElegido);
            rasgosUsados.Add(rasgoElegido);
        }

        Cliente persona = new Cliente(ordenRasgo);
        listaClientes.Add(persona); // Agrega el cliente a la lista pública de clientes
    }


    public void ReusarCliente(int indiceElegido)
    {
        if (indiceElegido >= 0 && indiceElegido < listaClientes.Count)
        {
            Cliente clienteReusado = listaClientes[indiceElegido];
            MostrarClienteExistente(clienteReusado); // Muestra el cliente reusado en pantalla
        }
        else
        {
            Debug.LogError("Índice de cliente no válido.");
        }
    }


    int elegir(Rasgo lista)
    {
        int random = Random.Range(0, lista.opciones.Count);
        GameObject listaObjeto = puntoSpawn.transform.Find(lista.name).gameObject;
        SpriteRenderer listaRender = listaObjeto.GetComponent<SpriteRenderer>();

        // Crear el sprite
        Sprite sprite = Sprite.Create(lista.opciones[random], new Rect(0, 0, lista.opciones[random].width, lista.opciones[random].height), Vector2.one);
        listaRender.sprite = sprite;

        // Obtener la posición inicial del hijo correspondiente al ScriptableObject
        Vector3 posicionInicial = ObtenerPosicionInicial(lista.name);

        // Calcular el offset para centrar el sprite
        Vector3 offset = CalcularOffset(sprite);

        // Aplicar la posición inicial y el offset al objeto
        listaObjeto.transform.position = posicionInicial + offset;

        return random;
    }

    Vector3 ObtenerPosicionInicial(string nombre)
    {
        if (posicionesRelativas.ContainsKey(nombre))
        {
            return posicionesRelativas[nombre];
        }
        else
        {
            return puntoSpawn.transform.position;
        }
    }

    Vector3 CalcularOffset(Sprite sprite)
    {
        Vector3 spriteSize = sprite.bounds.size;
        return new Vector3(spriteSize.x / 2, spriteSize.y / 2, 0f);
    }


    void MostrarClienteExistente(Cliente cliente)
    {
        foreach (int rasgo in cliente.identidad)
        {
            foreach (Rasgo rasgoObj in rasgos)
            {
                if (rasgoObj.opciones.Count > rasgo)
                {
                    GameObject objetoRasgo = puntoSpawn.transform.Find(rasgoObj.name).gameObject;
                    SpriteRenderer renderRasgo = objetoRasgo.GetComponent<SpriteRenderer>();
                    Sprite sprite = Sprite.Create(rasgoObj.opciones[rasgo], new Rect(0, 0, rasgoObj.opciones[rasgo].width, rasgoObj.opciones[rasgo].height), Vector2.one);
                    renderRasgo.sprite = sprite;
                    objetoRasgo.transform.position = ObtenerPosicionInicial(rasgoObj.name) + CalcularOffset(sprite);
                }
            }
        }
    }

}
