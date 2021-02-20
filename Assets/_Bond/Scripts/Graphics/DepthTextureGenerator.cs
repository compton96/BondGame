using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class DepthTextureGenerator : MonoBehaviour
{
    public bool apply;

    private RenderTexture _depthTexture;
    private Camera _camera;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(apply)
        {
            OnResolutionChange();
        }
        else
        {
            GetComponent<Camera>().forceIntoRenderTexture = false;
            GetComponent<Camera>().targetTexture = null;
        }
    }

    public void OnResolutionChange()
    {
        _camera = GetComponent<Camera>();

        Color background = _camera.backgroundColor;
        float farClippingPlane = _camera.farClipPlane;
        float priority = _camera.depth;
        _camera.CopyFrom(Camera.main);
        _camera.backgroundColor = background;
        _camera.clearFlags = CameraClearFlags.Color;
        _camera.farClipPlane = farClippingPlane;
        _camera.depth = priority;

        _depthTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 8);

        _camera.forceIntoRenderTexture = true;
        _camera.targetTexture = _depthTexture;

        Shader.SetGlobalTexture("_DepthTexture", _depthTexture);//TODO This may be removable soon
    }
}
