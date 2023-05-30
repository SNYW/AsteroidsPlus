using System;
using Random = UnityEngine.Random;

[Serializable]
public class RangeFloat 
{
    public float min;
    public float max;
      
    public RangeFloat(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float RandomValue()
    {
        return Random.Range(min, max);
    }
}