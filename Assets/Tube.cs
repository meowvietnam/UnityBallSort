using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tube : MonoBehaviour ,IPointerDownHandler
{
    // đây là tube 5 ball
    public GameObject listPosGO;
    public GameObject listBallGO;
    private Vector3 pointStartMove => listPosGO.transform.GetChild(4).transform.position + new Vector3(0,1f,0);
    public bool isFullTube => balls[4] != null;
    public List<GameObject> balls = new List<GameObject>(5);
    void Awake()
    {
        AwakeInitTube();
    }
    void AwakeInitTube()
    {
        for (int i = 0; i < 5; i++)
        {
            balls.Add(null);
        }
    }    
    public void OnPointerDown(PointerEventData eventData)
    {
       if(GameManager.instance.currentTube == null )
       {
            if(GetBall() != null)
            {
                GameManager.instance.currentTube = this;
                BallMoveUp(GetBall(), 0.2f);
            }    
          
       }    
       else
       {
            GameObject currentBall = GameManager.instance.currentTube.GetBall();
            if (GameManager.instance.currentTube == this || isFullTube || (GetBall() != null && currentBall.GetComponent<Image>().sprite != this.GetBall().GetComponent<Image>().sprite))
            {


                BallMoveDownReSet(currentBall, 0.2f);
            }
            else
            {
                BallMovePath(currentBall, currentBall.transform.position, pointStartMove,0.2f);
            }    
        }    
    }
    void BallMoveUp(GameObject ball,float time)
    {
        ball.transform.DOMove(pointStartMove, time);

    }
    void BallMoveDownReSet(GameObject ball, float time)
    {
        ball.transform.DOMove(GameManager.instance.currentTube.listPosGO.transform.GetChild(GameManager.instance.currentTube.balls.IndexOf(ball)).transform.position, time).OnComplete(()=>{

            GameManager.instance.currentTube = null;
        });

    }
    void BallMovePath(GameObject ball,Vector3 startPoint, Vector3 endPoint, float time)
    {
        Vector3[] waypoints = new Vector3[5];
        Vector3 midPoint = (startPoint + endPoint) / 2;
        Vector3 midPointLeft = (midPoint + startPoint) / 2;
        Vector3 midPointRight = (midPoint + endPoint) / 2;

        waypoints[0] = startPoint;
        waypoints[1] = midPointLeft + new Vector3(0,1f,0);
        waypoints[2] = midPoint + new Vector3(0, 1f, 0);
        waypoints[3] = midPointRight + new Vector3(0, 1f, 0);
        waypoints[4] = endPoint;


        //DoPath đi theo đường cong
        ball.transform.DOPath(waypoints, time/2, PathType.CatmullRom, PathMode.TopDown2D).OnComplete(()=> {

            ball.transform.DOMove(listPosGO.transform.GetChild(GetIndexBallNull()).transform.position, time / 2);
            ball.transform.SetParent(this.listBallGO.transform);
            GameManager.instance.currentTube.InitTube();
            this.InitTube();

            if(isFullTube)
            {
                // CheckWin
                Debug.Log("CheckWin");
                GameManager.instance.CheckWin();

            } 
            GameManager.instance.currentTube = null;


        });
        //SetEase(Ease.InOutQuad) là một phương thức trong DOTween được sử dụng để thiết lập hiệu ứng easing (làm mượt) cho chuyển động

    }


    public void InitTube()
    {
        for (int i = 0; i < listBallGO.transform.childCount; i++)
        {
            balls[i] = listBallGO.transform.GetChild(i).gameObject;
        }
        if(balls.Count > listBallGO.transform.childCount)
        {
            for (int i = balls.Count - 1; i > listBallGO.transform.childCount-1; i--)
            {
                balls[i] = null;
            }
        }    
    }    
    GameObject GetBall()
    {
        for(int i = balls.Count - 1;i >= 0;i--)
        {
            if (balls[i] != null)
            {
                return balls[i];
            }    
        }
        return null;
    }    
    int GetIndexBallNull()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] == null)
            {
                return i;
            }
        }
        return -1;
    }    

    
}
