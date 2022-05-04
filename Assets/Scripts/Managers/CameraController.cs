using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides camera logic  
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _transitionDuration = 3f;

    private float _timeElapsed;

    private Vector3 _destination = Vector3.zero;

    private void Update()
    {
        if (_destination != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, _destination, _timeElapsed / _transitionDuration);
            _timeElapsed += Time.deltaTime;
        }
    }

    /// <summary>
    /// Moves camera object to specified destination 
    /// </summary>
    /// <param name="destination">Worldspace destination</param>
    public void MoveTo(Vector3 destination)
    {
        _timeElapsed = 0;
        _destination = destination;
    }
}
