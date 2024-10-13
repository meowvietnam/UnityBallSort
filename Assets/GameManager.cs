using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Tube> tubes = new List<Tube>();
    public static GameManager instance;
    public GameObject prefabTube;
    public GameObject prefabBall;
    public GameObject listRowGO;
    public GameObject prefabListTube;
    public Sprite[] spriteBalls;
    public LevelData[] level;
    public GameObject panelWin;
    public Button btnContinue;

    public int lvGame = 0;

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
       // listRowGO = GameObject.Find("ListTube");
       
    }
    void Start()
    {
        level[lvGame].CreateLevel();
        btnContinue.onClick.AddListener(EventButtonContinue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EventButtonContinue()
    {
        Debug.Log("NextLv");
        panelWin.SetActive(false);
        for (int i = 0; i < listRowGO.transform.childCount; i++)
        {
            Destroy(listRowGO.transform.GetChild(i).gameObject);

        }
        tubes.Clear();
        level[lvGame].CreateLevel();
        
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
        lvGame++;

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
