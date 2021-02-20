using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class MonsterSkinBufferGenerator : MonoBehaviour
{
    public bool apply;

    private RenderTexture _monsterSkinBuffer;
    private Camera _camera;

    private void Start()
    {
        if (apply)
        {
            OnResolutionChange();
            RenderPipelineManager.beginCameraRendering += (context, camera) =>
            {
                if (camera == _camera)
                {
                    Shader.SetGlobalFloat("_MonsterSkinBufferStage", 1);
                }
                else
                {
                    Shader.SetGlobalFloat("_MonsterSkinBufferStage", 0);
                }
            };
        }
    }

    void OnEnable()
    {
        if (apply)
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

        // save settings that differ from the main camera
        Color background = _camera.backgroundColor;
        float priority = _camera.depth;
        int layerMask = _camera.cullingMask;

        // copy all settings from main camera
        _camera.CopyFrom(Camera.main);

        // restore settings that differ from the main camera
        _camera.backgroundColor = background;
        _camera.clearFlags = CameraClearFlags.Color;
        _camera.depth = priority;
        _camera.cullingMask = layerMask;

        _monsterSkinBuffer = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 8);

        _camera.forceIntoRenderTexture = true;
        _camera.targetTexture = _monsterSkinBuffer;

        Shader.SetGlobalTexture("_MonsterSkinBuffer", _monsterSkinBuffer);//TODO This may be removable soon
    }
}
