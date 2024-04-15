using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObjectsManager : MonoBehaviour
{
    public static SpawnedObjectsManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
