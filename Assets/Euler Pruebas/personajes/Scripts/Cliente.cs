using System.Collections.Generic;
using UnityEngine;

public class Cliente
{
    public List<int> identidad;

    public Cliente(List<int> _identidad)
    {
        identidad = _identidad;
    }

    // Método para obtener los rasgos del cliente
    public List<int> ObtenerRasgos()
    {
        return identidad;
    }
}
