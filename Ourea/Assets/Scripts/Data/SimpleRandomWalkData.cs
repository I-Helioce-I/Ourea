using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simle Random Walk Parameters_", menuName = "Helioce/PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkData : ScriptableObject
{
    public int iterations = 10, walkLenght = 10;
    public bool startRandomlyEachIteration = true;
}