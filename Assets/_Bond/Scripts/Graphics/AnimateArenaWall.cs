using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateArenaWall : MonoBehaviour
{
    public AnimationCurve fadeCurve;
    public float fadeLength;

    private float fadeStartTime;

    public bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 1);
        fadeStartTime = Time.time - fadeLength;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger)
        {
            trigger = false;
            fadeStartTime = Time.time;
        }

        if (Time.time < fadeStartTime + fadeLength)
        {
            float waveVal = fadeCurve.Evaluate((Time.time - fadeStartTime) / fadeLength);

            GetComponent<Renderer>().sharedMaterial.SetFloat("_FadeinBreakpoint", waveVal);
        }
    }
}
