using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindUpdater : MonoBehaviour {

    public string WindDirection;
    private float lerp;
    public WindZone windObj;

    public void UpdateWind(string FromData,float speed,float turbulance) {
        WindDirection = FromData;
        //StartCoroutine("LerpToDirectionWind", GetWindDirectionFromData(FromData));
        transform.Rotate(0f, GetWindDirectionFromData(FromData), 0f, Space.Self);
        windObj.windMain = speed;
        windObj.windTurbulence = turbulance;


    }

    //public IEnumerator LerpToDirectionWind(float toDirection)
    //{
    //    lerp = 0;

    //    while (lerp < 1)
    //    {
    //        lerp += Time.deltaTime / 2;
    //        var lerpedRotation = Mathf.Lerp(transform.rotation.y, toDirection,lerp);
    //        transform.Rotate(0f, lerpedRotation, 0f, Space.Self);
    //        print(lerpedRotation);
    //    }
    //    yield return null;

    //}

    public float GetWindDirectionFromData(string DataWind)
    {
        if (DataWind == "N")
        {
            return 67.5f;
        }
        else if (DataWind == "NNE")
        {
            return 90f;
        }
        else if (DataWind == "NE")
        {
            return 112.5f;
        }
        else if (DataWind == "ENE")
        {
            return 135f;
        }
        else if (DataWind == "E")
        {
            return 157.5f;
        }
        else if (DataWind == "ESE")
        {
            return 180f;
        }
        else if (DataWind == "SE")
        {
            return 202.5f;
        }
        else if (DataWind == "SSE")
        {
            return 225f;
        }
        else if (DataWind == "S")
        {
            return 247.5f;
        }
        else if (DataWind == "SSW")
        {
            return 270f;
        }
        else if (DataWind == "SW")
        {
            return 292.5f;
        }
        else if (DataWind == "WSW")
        {
            return 315f;
        }
        else if (DataWind == "W")
        {
            return 337.5f;
        }
        else if (DataWind == "WNW")
        {
            return 360f;
        }
        else if (DataWind == "NW")
        {
            return 22.5f;
        }
        else if (DataWind == "NNW")
        {
            return 45f;
        }
        return 0f;
    }
}
