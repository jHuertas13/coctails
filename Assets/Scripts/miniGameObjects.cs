using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiniGame", menuName = "Minigame", order = 1)]
public class MiniGameEntry : ScriptableObject
{
    public GameObject miniGamePrefab;
    public GameObject miniGameTrigger;
}

