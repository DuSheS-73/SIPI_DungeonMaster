using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private RectTransform _rectTransform;

    private void Start()
    {
        // for (int x = 0; x < FloorGenerator.Instance.Properties.MaxRoomsInRow; x++)
        // {
        //     for (int y = 0; y < FloorGenerator.Instance.Properties.MaxRoomsInCol; y++)
        //     {
        //         //Instantiate(_image, )
        //     }
        // }
        // Debug.Log(_rectTransform.rect.width / 16);
    }
}
