using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetColor(Color newColor)
    {
        _image.color = newColor;
    }
}
