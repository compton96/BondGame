using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateArenaCore : MonoBehaviour
{
    public AnimationCurve animation;
    public AnimationCurve waveCurve;
    public float animationSpeed;
    public float waveLength;
    public Vector2 waveRange;

    private float waveStartTime;

    public bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 1);
        waveStartTime = Time.time - waveLength;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, animation.Evaluate(((Time.time * animationSpeed) % 1) ) * 100);

        if(trigger)
        {
            trigger = false;
            waveStartTime = Time.time;
        }

        if (Time.time < waveStartTime + waveLength)
        {
            float waveVal = waveCurve.Evaluate((Time.time - waveStartTime) / waveLength);
            waveVal = (waveVal * (waveRange.y - waveRange.x)) + waveRange.x;

            GetComponent<Renderer>().sharedMaterial.SetFloat("_WaveOffset", waveVal);
        }
    }
}
