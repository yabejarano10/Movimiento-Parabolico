using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Canvas parametersCanvas;
    public Canvas endCanvas;

    public InputField inputAngle;
    public InputField inputVelocity;
    public InputField inputDistance;

    public Text errorText;
    public Text endText;

    public Button startButton;
    public Button restartButton;

    public GameObject endPosition;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startParabolicMovement);
        restartButton.onClick.AddListener(restartSimulation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startParabolicMovement()
    {
        float angle;
        float velocity;
        double distance;
        if (String.IsNullOrEmpty(inputAngle.text) || String.IsNullOrEmpty(inputVelocity.text))
        {
            errorText.text = "Todos los campos son requeridos.";
            errorText.gameObject.SetActive(true);
            return;
        }
        if (!float.TryParse(inputAngle.text, out angle) || !float.TryParse(inputVelocity.text, out velocity) || !double.TryParse(inputDistance.text, out distance))
        {
            errorText.text = "Los campos deben ser numéricos.";
            errorText.gameObject.SetActive(true);
            return;
        }

        parametersCanvas.gameObject.SetActive(false);
        double angle2 = Math.PI * (2 * angle)/180.0;
        float  maxRange = (float)((Mathf.Pow(velocity, 2) * Math.Sin(angle2)) / 9.81);
        endPosition.transform.position = new Vector3(maxRange, 0, -10);

        double xVel, yVel;
        xVel = velocity * Math.Cos(Math.PI * angle / 180.0);
        yVel = velocity * Math.Sin(Math.PI * angle / 180.0);

        ball.transform.position = new Vector3(0, 0,-9);
        Rigidbody2D rigid = ball.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2((float)xVel, (float)yVel);
    
        //float plusFivePercent = (float)(maxRange + (maxRange * 0.05));
        //float minusFivePercent = (float)(maxRange - (maxRange * 0.05));

        //if(distance > plusFivePercent || distance < minusFivePercent)
        //{
        //    Debug.Log(distance);
        //    Debug.Log(plusFivePercent);
        //    Debug.Log(minusFivePercent);
        //    endText.text = "La estimación de la distancia fue incorrecta";
        //    endCanvas.gameObject.SetActive(true);
        //}
        //else
        //{
        //    endText.text = "La estimación de la distancia fue correcta!! Felicidades";
        //    endCanvas.gameObject.SetActive(true);
        //}
     
    }
    void restartSimulation()
    {
        endCanvas.gameObject.SetActive(false);
        parametersCanvas.gameObject.SetActive(true);
    }
}
