using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private bool _isServer;
    private void Start()
    {
        if (_isServer)
        {
            NetworkManager.Singleton.StartServer();
        }
        else
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
