using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject smallCirclePrefab;
    List<Rigidbody2D> allSmallCircles = new List<Rigidbody2D>();
    int level;
    public int allCirclesCount;
    float speed = 20.0f;
    float firstCircleYPos = -3.0f;
    float scaleNumber = 1.0f;
    float smallDiam;
    float tolerance = 1.24f;
    UIManager uim;
    [SerializeField] TextMeshProUGUI levelTxt;
    Transform bigCircleCollider;

    void Start()
    {
        uim = GameObject.FindObjectOfType<UIManager>();
        bigCircleCollider = GameObject.Find("Big Circle Collider").transform;
        levelControl();
        AddingCircles();
    }

    private void AddingCircles()
    {
        allCirclesCount = level * 3;

        for (int i = 0; i < allCirclesCount; i++)
        {
            GameObject newCircle = Instantiate(smallCirclePrefab);
            if (i == 0)
            {
                smallDiam = newCircle.GetComponent<CircleCollider2D>().bounds.size.x;
                float bigDiam = bigCircleCollider.GetComponent<CircleCollider2D>().bounds.size.x;
                float radian = Mathf.PI / (allCirclesCount * tolerance);
                float tangent = Mathf.Tan(radian);

                if (smallDiam > (bigDiam * tangent))
                {
                    scaleNumber = bigDiam * tangent / smallDiam;
                    float newScale = newCircle.transform.localScale.x * scaleNumber;
                    newCircle.transform.localScale = new Vector3(newScale, newScale, 1);
                }
                newCircle.transform.position = new Vector2(0, firstCircleYPos);
            }
            else
            {
                newCircle.transform.localScale = allSmallCircles[0].transform.localScale;
                newCircle.transform.position = new Vector2(0, allSmallCircles[i - 1].transform.position.y - (smallDiam * scaleNumber * 1.5f));
            }
            newCircle.GetComponentInChildren<TextMeshProUGUI>().text = (allCirclesCount - i).ToString();
            allSmallCircles.Add(newCircle.GetComponent<Rigidbody2D>());
        }
    }

    private void levelControl()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt("level", level);
        }
        levelTxt.text = level.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && allCirclesCount > 0)
        {
            allSmallCircles[0].velocity = Vector2.Lerp(allSmallCircles[0].transform.position, Vector2.up, 1) * speed;
            allSmallCircles.RemoveAt(0);
        }
        else if (allSmallCircles.Count <= 0)
        {
            LevelPassed();
        }
    }

    private void LevelPassed()
    {
        uim.ShowLevelPassed();
        this.enabled = false;
    }

    public void Restart()
    {
        uim.ShowRestartPanel();
        this.enabled = false;
    }

    public void UpdateSmallCircles()
    {
        for (int i = 0; i < allSmallCircles.Count; i++)
        {
            if (i == 0)
            {
                allSmallCircles[i].transform.position = Vector2.Lerp(allSmallCircles[i].transform.position, new Vector2(0, firstCircleYPos), 1);
            }
            else
            {
                allSmallCircles[i].transform.position = Vector2.Lerp(allSmallCircles[i].transform.position, new Vector2(0, allSmallCircles[i - 1].transform.position.y - smallDiam * scaleNumber * 1.5f), 1);
            }
        }
    }
}
