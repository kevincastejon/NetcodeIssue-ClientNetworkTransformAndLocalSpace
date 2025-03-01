using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    private static PlayerSpawner _instance;

    public static PlayerSpawner Instance { get => _instance; }

    private void Awake()
    {
        _instance = this;
    }
}
