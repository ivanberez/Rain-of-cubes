using UnityEngine;

public class VisualizingBomb
{
    private readonly Color _defaultColor = Color.black;
    private readonly Color _changeAlpha = new Color(Color.black.r, Color.black.g, Color.black.b, 0);

    private MeshRenderer _renderer;

    public VisualizingBomb(MeshRenderer renderer)
    {
        _renderer = renderer;
        _renderer.material.color = _defaultColor;
    }

    public void ChangeAlpha(float time)
    {        
       _renderer.material.color = Color.Lerp(_defaultColor, _changeAlpha, time);
    }

    public void Standardize()
    {
        _renderer.material.color = _defaultColor;
    }
}
