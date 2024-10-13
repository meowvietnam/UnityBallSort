using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RowTube
{
    public List<string> tubeData = new List<string>();

}

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public List<RowTube> level = new List<RowTube>();

    
    public void CreateLevel()
    {
        for (int i = 0; i < level.Count; i++)
        {
            GameObject listTubeGO =  Instantiate(GameManager.instance.prefabListTube, GameManager.instance.listRowGO.transform);
            CreateTube(ref GameManager.instance.tubes,level[i].tubeData,listTubeGO);
        }
    }
   
    public void CreateTube(ref List<Tube> tubes , List<String> tubeData , GameObject listTubeGO)
    {
        for (int i = 0; i < tubeData.Count; i++)
        {
            Tube thisTube = Instantiate(GameManager.instance.prefabTube, listTubeGO.transform).GetComponent<Tube>();
            tubes.Add(thisTube);
            int[] idBalls = Array.ConvertAll(tubeData[i].Split(','), int.Parse);
            for (int j = 0; j < idBalls.Length; j++)
            {
                if (idBalls[j] == -1) continue;
                Instantiate(GameManager.instance.prefabBall, thisTube.listPosGO.transform.GetChild(j).transform.position,Quaternion.identity,thisTube.listBallGO.transform).GetComponent<Image>().sprite = GameManager.instance.spriteBalls[idBalls[j]];

            }
            thisTube.InitTube();
          

        }


    }
     


}
