using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionTrigger : MonoBehaviour
{
    [SerializeField] private Room _parentRoom;

    [SerializeField] private DoorTypes _doorType;

    [SerializeField] private float _playerTransitionDistance = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController camera = Camera.main.GetComponent<CameraController>();
            Player player = other.GetComponent<Player>();

            Vector2Int targetRoomGridCoordinates = Vector2Int.zero;

            switch (_doorType)
            {
                case DoorTypes.Top:
                    targetRoomGridCoordinates = _parentRoom.GridPosition + Vector2Int.up;
                    player.transform.position += new Vector3(0f, 0f, _playerTransitionDistance);
                    break;

                case DoorTypes.Right:
                    targetRoomGridCoordinates = _parentRoom.GridPosition + Vector2Int.right;
                    player.transform.position += new Vector3(_playerTransitionDistance, 0f, 0f);
                    break;

                case DoorTypes.Bottom:
                    targetRoomGridCoordinates = _parentRoom.GridPosition + Vector2Int.down;
                    player.transform.position += new Vector3(0f, 0f, -_playerTransitionDistance);
                    break;

                case DoorTypes.Left:
                    targetRoomGridCoordinates =_parentRoom.GridPosition + Vector2Int.left;
                    player.transform.position += new Vector3(-_playerTransitionDistance, 0f, 0f);
                    break;
            }

            var cameraDestination = 
                new Vector3(
                    targetRoomGridCoordinates.x * FloorGenerator.Instance.Properties.RoomWidth,
                    camera.transform.position.y,
                    targetRoomGridCoordinates.y * FloorGenerator.Instance.Properties.RoomHeight
                );
            camera.MoveTo(cameraDestination);


            Room targetRoom = FloorGenerator.Instance.GetRoomAt(targetRoomGridCoordinates);

            if (targetRoom != null)
            {
                targetRoom.HandlePlayerEntered();
            }
        }
    }
}
