using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Licors : ScriptableObject
{

    public Sprite image;

    public int sweet;
    public int sour;
    public int salt;
    public int bitter;
    public int umami;
    public Propiety propiety; 
    public enum Propiety{
        None,
        Spicy,
        Dry,
        Carbonated,
        Minty,
        Smoked
    }
}

