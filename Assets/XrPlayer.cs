using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class XrPlayer : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkObject.TrySetParent(PlayerSpawner.Instance.NetworkObject, false);
        }
    }
}
