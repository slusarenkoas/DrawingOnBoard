using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DrawingController : MonoBehaviour
{
    public UnityEvent<Color> ColorChanged;

    [SerializeField] private Material _material;
    [SerializeField] private float _deep = 0.5f;
    
    private Camera _camera;

    private float _currentWidth = 0.1f;
    private Color _currentColor = Color.clear;

    private LineRenderer _lineRenderer;
    private List<LineRenderer> _lineRenderers = new ();

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawingNewLine();
        }

        if (Input.GetMouseButton(0))
        {
            ContinueDrawingLine();
        }
    }

    public void SetColor (Image image)
    {
        _currentColor = image.color;
        _lineRenderer.startColor = _currentColor;
        _lineRenderer.endColor = _currentColor;
        ColorChanged?.Invoke(_currentColor);
    }

    public void ClearBoard()
    {
        for (var index = 0; index < _lineRenderers.Count - 1; index++)
        {
            var lineRenderer = _lineRenderers[index];
            if (lineRenderer != null)
            {
                Destroy(lineRenderer.gameObject);
            }
        }
    }
    
    private void ContinueDrawingLine()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray))
        {
            return;
        }

        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _deep);
        var drawingPoint = _camera.ScreenToWorldPoint(mousePosition);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount-1,drawingPoint);
    }

    private void StartDrawingNewLine()
    {
        var line = new GameObject("Line");
        line.transform.SetParent(transform);
        line.transform.SetAsFirstSibling();
        _lineRenderer = line.AddComponent<LineRenderer>();

        _lineRenderer.positionCount = 0;
        _lineRenderer.material = _material;
        _lineRenderer.startWidth = _currentWidth;
        _lineRenderer.endWidth = _currentWidth;
        _lineRenderer.startColor = _currentColor;
        _lineRenderer.endColor = _currentColor;
        
        _lineRenderers.Add(_lineRenderer);
    }
}
