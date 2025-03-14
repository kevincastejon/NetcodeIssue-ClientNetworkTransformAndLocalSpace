using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject _playerPrefab;
    private bool _isReversed;
    private float _travellingDuration = 4f;
    private float _travellingStartTime;
    private void Update()
    {
        if (IsServer)
        {
            float elaspedTime = Time.time - _travellingStartTime;
            Vector3 startPos = !_isReversed ? new() : new(5f, 5f, 5f);
            Quaternion startRot = !_isReversed ? Quaternion.identity : Quaternion.Euler(new(0f, 180f, 0f));
            Vector3 destPos = _isReversed ? new(): new(5f, 5f, 5f);
            Quaternion destRot = _isReversed ? Quaternion.identity : Quaternion.Euler(new(0f, 180f, 0f));
            if (elaspedTime >= _travellingDuration)
            {
                transform.position = destPos;
                transform.rotation = destRot;
                _isReversed = !_isReversed;
                _travellingStartTime = Time.time;
            }
            else
            {
                float progress = elaspedTime / _travellingDuration;
                transform.position = Vector3.Lerp(startPos, destPos, progress);
                transform.rotation = Quaternion.Slerp(startRot, destRot, progress);
            }
        }
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _travellingStartTime = Time.time;
        }
        if (IsClient)
        {
            RequestPlayerObjectSpawnServerRpc();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void RequestPlayerObjectSpawnServerRpc(ServerRpcParams rpcParams = default)
    {
        NetworkObject playerObject = Instantiate(_playerPrefab);
        playerObject.SpawnAsPlayerObject(rpcParams.Receive.SenderClientId);
        playerObject.TrySetParent(NetworkObject, false);
    }
}
