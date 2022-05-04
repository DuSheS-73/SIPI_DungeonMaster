using System;
using UnityEngine;

[Serializable]
public class FloorProperties
{
    [SerializeField] private int _minAmountOfRooms = 5;
    [SerializeField] private int _maxAmountOfRooms = 7;
    [SerializeField] private int _maxRoomsInRow = 16;
    [SerializeField] private int _maxRoomsInCol = 16;
    [SerializeField] private int _roomWidth = 16;
    [SerializeField] private int _roomHeight = 10;

    public int MinAmountOfRooms => _minAmountOfRooms;
    public int MaxAmountOfRooms => _maxAmountOfRooms;
    public int MaxRoomsInRow => _maxRoomsInRow;
    public int MaxRoomsInCol => _maxRoomsInCol;
    public int RoomWidth => _roomWidth;
    public int RoomHeight => _roomHeight;
}