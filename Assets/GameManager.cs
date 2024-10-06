using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<Tube> tubes = new List<Tube>();
    public static GameManager instance;
    public GameObject prefabTube;
    public GameObject prefabBall;
    public GameObject listTubeGO;
    public Sprite[] spriteBalls;
    public LevelData[] level;
    public GameObject panelWin;


    public Tube currentTube;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }    
        else
        {
            Destroy(gameObject);
        }    
        listTubeGO = GameObject.Find("ListTube");
        panelWin = GameObject.Find("PanelWin");
        panelWin.gameObject.SetActive(false);
    }
    void Start()
    {
        level[0].CreateLevel(ref tubes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckWin()
    {
        if(IsWin())
        {
            StartCoroutine(CoroutineWin());
        }    
    } 
    IEnumerator CoroutineWin()
    {
        yield return new WaitForSeconds(0.5f);
        // Win
        panelWin.SetActive(true);

    }    
    bool IsWin()
    {
        for (int i = 0; i < tubes.Count; i++)
        {
            if (tubes[i].balls[0] == null)
            {
                continue;
            }    
            if (!tubes[i].isFullTube)
            {
                Debug.Log("Chưa Win");
                return false;
            }
            else 
            {
                Sprite ballCompare = tubes[i].balls[0].GetComponent<Image>().sprite;
                 
                for (int j = 1; j < tubes[i].balls.Count; j++)
                {
                    if (tubes[i].balls[j].GetComponent<Image>().sprite != ballCompare)
                    {
                        Debug.Log("Chưa Win");
                        return false;
                    }    
                }
               
            }    
             
        }
        Debug.Log("Win");

        return true;
    }    
}
