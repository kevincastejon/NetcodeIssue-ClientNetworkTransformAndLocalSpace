using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;

public class NetworkTimeline : NetworkBehaviour
{
    private PlayableDirector _timeline;
    private void Awake()
    {
        _timeline = GetComponent<PlayableDirector>();
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _timeline.Play();
        }
        else
        {
            SynchronizeTimelineRequestRpc();
        }
    }
    [Rpc(SendTo.Server)]
    private void SynchronizeTimelineRequestRpc(RpcParams rpcParams = default)
    {
        SynchronizeTimelineRpc(_timeline.time, new RpcParams() { Send = new() { Target = RpcTarget.Single(rpcParams.Receive.SenderClientId, RpcTargetUse.Temp) } });
    }
    [Rpc(SendTo.SpecifiedInParams)]
    private void SynchronizeTimelineRpc(double time, RpcParams rpcParams)
    {
        _timeline.time = time;
        _timeline.Play();
    }
}
