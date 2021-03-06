using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShaderFloatAnimator 
{
    public float Value
    { 
        get
        {
            return _value;
        }

        set
        {
            _value = value;
            if(_material)
            {
                _material.SetFloat(Name,_value);
            }
            else
            {
                Shader.SetGlobalFloat(Name, _value);
            }
        }
    }

    public string Name { get; private set; }

    private Material _material;
    private float _value;

    public ShaderFloatAnimator(string name)
    {
        _material = null;
        _value = Shader.GetGlobalFloat(name);
        Name = name;
    }

    public ShaderFloatAnimator(string name, Material material)
    {
        _material = material;
        _value = material.GetFloat(name);
        Name = name;
    }

}
