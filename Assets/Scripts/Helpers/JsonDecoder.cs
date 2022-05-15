using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDecoder
{
    public LevelWavesData parseWavesData(string jsonPath)
    {
        Debug.Log(jsonPath);
        TextAsset loadedFile = Resources.Load<TextAsset>(jsonPath);
        return JsonConvert.DeserializeObject<LevelWavesData>(loadedFile.text);        
    }
}
