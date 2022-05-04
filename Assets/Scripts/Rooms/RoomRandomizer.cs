using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRandomizer : MonoBehaviour
{
    [SerializeField] private Mesh[] _blockMeshes;

    private void Start()
    {
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {
            if (meshFilter.sharedMesh == _blockMeshes[0])
            {
                meshFilter.sharedMesh = _blockMeshes[Random.Range(0, _blockMeshes.Length)];
                meshFilter.transform.rotation = Quaternion.Euler(-90, 0, 90 * Random.Range(0, 4));
            }
        }
    }
}
