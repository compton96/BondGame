using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShaderColorAnimator
{
    public Color Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
            if (_material)
            {
                _material.SetColor(Name, _value);
            }
            else
            {
                Shader.SetGlobalColor(Name, _value);
            }
        }
    }

    public string Name { get; private set; }

    private Material _material;
    private Color _value;

    public ShaderColorAnimator(string name)
    {
        _material = null;
        _value = Shader.GetGlobalColor(name);
        Name = name;
    }

    public ShaderColorAnimator(string name, Material material)
    {
        _material = material;
        _value = material.GetColor(name);
        Name = name;
    }

}
