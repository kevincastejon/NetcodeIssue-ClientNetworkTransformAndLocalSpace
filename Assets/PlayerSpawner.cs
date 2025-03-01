using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject _playerPrefab;
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
        if (IsClient)
        {
            RequestPlayerObjectSpawnRpc();
        }
    }
    [Rpc(SendTo.Server)]
    private void RequestPlayerObjectSpawnRpc(RpcParams rpcParams = default)
    {
        NetworkObject playerObject = Instantiate(_playerPrefab);
        playerObject.SpawnAsPlayerObject(rpcParams.Receive.SenderClientId);
        playerObject.TrySetParent(NetworkObject, false);
        SynchronizeTimelineRpc(_timeline.time, new RpcParams() { Send = new() { Target = RpcTarget.Single(rpcParams.Receive.SenderClientId, RpcTargetUse.Temp) } });
    }
    [Rpc(SendTo.SpecifiedInParams)]
    private void SynchronizeTimelineRpc(double time, RpcParams rpcParams)
    {
        _timeline.time = time;
        _timeline.Play();
    }
}
