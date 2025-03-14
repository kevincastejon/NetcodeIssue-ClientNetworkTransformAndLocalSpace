using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class XrPlayer : NetworkBehaviour
{
    private void Update()
    {
        if (IsServer)
        {
            FakeRpc(5f, new() { Send = new() { Target = RpcTarget.Single(OwnerClientId, RpcTargetUse.Temp) } });
        }
    }
    [Rpc(SendTo.SpecifiedInParams)]
    private void FakeRpc(float fakeData, RpcParams rpcParams)
    {
        Debug.Log("MSG RECEIVED");
    }
}
