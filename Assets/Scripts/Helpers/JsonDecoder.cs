using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDecoder
{
    public LevelGroupsData parseWavesData(string jsonPath)
    {        
        TextAsset loadedFile = Resources.Load<TextAsset>(jsonPath);
        LevelWavesData parsedData = JsonConvert.DeserializeObject<LevelWavesData>(loadedFile.text);
        LevelGroupsData data = new LevelGroupsData();

        data.delayBetweenWaves = parsedData.wavesDelay;
        data.initialDelay = parsedData.initialDelay;

        data.groups = new List<EnemyGroupData>();
        foreach(JSONEnemyGroup jSONEnemy in parsedData.weaves) {            
            EnemyGroupData groupData = new EnemyGroupData();
            groupData.positionX = jSONEnemy.x;
            groupData.positionY = jSONEnemy.y;
            groupData.type = jSONEnemy.name;
            groupData.movingDirection = jSONEnemy.direction;
            data.groups.Add(groupData);
        }

        return data;
    }
}
