using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDecoder
{
    public void parseJson(string jsonPath)
    {
        Debug.Log(jsonPath);
        TextAsset loadedFile = Resources.Load<TextAsset>(jsonPath);
        LevelWavesData account = JsonConvert.DeserializeObject<LevelWavesData>(loadedFile.text);

        Debug.Log(account.initialDelay);
        Debug.Log(account.weaves.Length);

        Debug.Log(account.weaves[0]);
        Debug.Log(account.weaves[1]);
    }
}
