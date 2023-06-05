using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using System;

public class Timer : MonoBehaviour
{
    public Text txtTimer;
    public Text txtRanks;
    public int second = 5;
    public int game_second = 30;
    private int flag = 1;
    public Transform frontRight;
    public Transform frontRight_police;
    public Transform frontRight_old;
/*    public Transform point;
    public Transform point2;*/
    public Transform[] pointlist;
    private bool[] point_used_list = {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
    //Vector3 test_point = new Vector3(380, 2, 121);
    //bool point_1 = true;
    public static int user = 0;

    private int score = 0;

    public TextAsset textTxt;

    private static SortedDictionary<string, int> ranks = new SortedDictionary<string, int>();
    private static List<int> ranks_list = new List<int>();



    private float nextTime = 1;//下次修改时间

    private void Update()
    {
        Timer1();
        //Debug.Log(textTxt.text);
        Debug.Log(textTxt.text);
        Debug.Log("Count : " + ranks_list.Count.ToString());

        string tmp = "Ranks:";
        ranks_list.Sort((x, y) => -x.CompareTo(y));
        if (ranks_list.Count >= 1)
        {
            tmp = "Ranks:\n第一名 : " + ranks_list[0].ToString();
        }

        if (ranks_list.Count >= 2)
        {
            tmp = "Ranks:\n第一名 : " + ranks_list[0].ToString() + "\n第二名 : " + ranks_list[1].ToString();
        }

        if (ranks_list.Count >= 3)
        {
            tmp = "Ranks:\n第一名 : " + ranks_list[0].ToString() + "\n第二名 : " + ranks_list[1].ToString() + "\n第三名 : " + ranks_list[2].ToString();
        }

        txtRanks.text = string.Format(tmp);
        //Timer2();
    }

    private void FixedUpdate()
    {
        //foreach (Transform point in pointlist)
        for (int i = 0; i < 21; i++)
        {
            float f = Vector3.Distance(frontRight.position, pointlist[i].position);
            float f_police = Vector3.Distance(frontRight_police.position, pointlist[i].position);
            float f_old = Vector3.Distance(frontRight_old.position, pointlist[i].position);

            if ((f < 20 || f_old < 20 || f_police < 20) && flag == 2 && point_used_list[i])
            {           
                point_used_list[i] = false;
                second += 30;
                score += 1;
                Debug.Log("score:" + score);
            }
        }
           
    }

    //第一种方法///
    private void Timer1()
    {
        //Debug.Log("Timer1 被调用");

        //取消调用(倒计时为0时)[第三种方法时使用]
        //if (second == 0)
        //{
        //    CancelInvoke("Timer2");
        //}
        if (second >= 0)
        {
            if (second <= 10)
            {
                txtTimer.color = Color.red;
            }
            else
            {
                txtTimer.color = Color.black;
            }

            if (Time.time >= nextTime)
            {
                txtTimer.color = Color.black;
                second--;
                if (flag == 1)
                {
                    txtTimer.text = string.Format("Car Selecting : {0:d2}:{1:d2}", second / 60, second % 60);
                }
                else
                {
                    txtTimer.text = string.Format("Time : {0:d2}:{1:d2}  Score : {2:d2}", second / 60, second % 60, score);
                }

                nextTime = Time.time + 1;   //变成当前时间的后一秒
            }

            if (second == 0)
            {

                //取消调用(倒计时为0时)[第三种方法时使用]
                if (flag == 1)
                {
                    second += game_second;

                    flag = 2;
                    nextTime = 1;
                }
                else
                {
                    user++;
                    ranks_list.Add(score);
                    ranks.Add(user.ToString(), score);
                    //recordRanks(user, score);


                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    //第二种方法///
    private float totalTime;
    private void Timer2()
    {
        //Debug.Log("Timer2 被调用");

        //取消调用(倒计时为0时)[第三种方法时使用]
        if (game_second == 0)
        {
            SceneManager.LoadScene(0);
        }

        if (game_second > 0)
        {
            //Debug.Log("进入");
            //累加每帧间隔
            totalTime = 0;
            if (game_second >= 1)
            {
                if (second <= 10)
                {
                    txtTimer.color = Color.red;
                }
                //Debug.Log("开始计时");
                game_second--;
                txtTimer.text = string.Format("Time : {0:d2}:{1:d2}", game_second / 60, game_second % 60);
                //Debug.Log("计时结束");
                totalTime = Time.time + 1;
            }
        }
    }

    private void recordRanks(int user, int score)
    {
        string path = Application.dataPath + "/RCK 2.3/Scripts/ranks.txt";
        Debug.Log(path);

        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);  //追加流，权限设置为可写
        string content = "\n" + user.ToString() + " : " + score.ToString();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        fs.Write(bytes, 0, bytes.Length);
        fs.Flush();
        fs.Close();
    }
}
