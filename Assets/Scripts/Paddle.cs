using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Config parms
    [SerializeField] float cameraWidth;
    [SerializeField] float mouseXMin;
    [SerializeField] float mouseXMax;

    // cached references to objects
    bool isAutoPlayEnabled = false;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<GameSession>().IsAutoPlayEnabled())
        {
            isAutoPlayEnabled = true;
        }

        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), mouseXMin, mouseXMax);
        transform.position = paddlePos;
     }

    private float GetXPos()
    {
        if (isAutoPlayEnabled)
        {
            return ball.transform.position.x;
        }
        else
        {
            // cam is 6 units in height, aspect ration 4x3, width = 4/3*6
            return Input.mousePosition.x / Screen.width * cameraWidth;
        }
    }
}
