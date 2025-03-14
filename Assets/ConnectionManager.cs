using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        NetworkManager.Singleton.StartServer();
#else
        NetworkManager.Singleton.StartClient();
#endif
    }
}
