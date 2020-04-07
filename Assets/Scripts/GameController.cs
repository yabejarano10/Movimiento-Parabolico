using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Canvas parametersCanvas;
    public Canvas endCanvas;
    public Canvas gameCanvas;
    public InputField inputAngle;
    public InputField inputVelocity;
    public InputField inputDistance;

    public Text errorText;
    public Text endText;

    public Button startButton;
    public Button restartButton;

    public GameObject endPosition;
    public GameObject ball;

    private float maxRange;
    private double distance;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startParabolicMovement);
        restartButton.onClick.AddListener(restartSimulation);
        maxRange = 0;
        distance = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startParabolicMovement()
    {
        float angle;
        float velocity;
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
        if(angle < 0 || angle > 90)
        {
            errorText.text = "El ángulo debe ser mayor a 0 y menos a 90.";
            errorText.gameObject.SetActive(true);
            return;
        }
        if(velocity < 0)
        {
            errorText.text = "La velocidad debe ser positiva.";
            errorText.gameObject.SetActive(true);
            return;
        }

        parametersCanvas.gameObject.SetActive(false);
        
        double angle2 = Math.PI * (2 * angle)/180.0;
        maxRange = getMaxRange(velocity, angle2);
        endPosition.transform.position = new Vector3(maxRange, 0, -10);

        double xVel, yVel;
        xVel = velocity * Math.Cos(Math.PI * angle / 180.0);
        yVel = velocity * Math.Sin(Math.PI * angle / 180.0);

        float timeInAir = (float)((2 * velocity * Math.Sin(Math.PI * angle / 180.0)) / 9.81);

        ball.SetActive(true);
        endPosition.SetActive(true);
        gameCanvas.gameObject.SetActive(true);
        ball.transform.position = new Vector3(0, 0,-9);
        Rigidbody2D rigid = ball.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2((float)xVel, (float)yVel);

        
    }
    void restartSimulation()
    {
        inputAngle.text = "";
        inputDistance.text = "";
        inputVelocity.text = "";
        endCanvas.gameObject.SetActive(false);
        parametersCanvas.gameObject.SetActive(true);
    }
    float getMaxRange(float vel, double angle)
    {
        return (float)((Mathf.Pow(vel, 2) * Math.Sin(angle)) / 9.81);
    }
    public void checkCorrectDistance()
    {
        ball.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
        float plusFivePercent = (float)(maxRange + (maxRange * 0.05));
        float minusFivePercent = (float)(maxRange - (maxRange * 0.05));

        if (distance > plusFivePercent || distance < minusFivePercent)
        {
            endText.text = "La estimación de la distancia fue incorrecta";
            endCanvas.gameObject.SetActive(true);
        }
        else
        {
            endText.text = "La estimación de la distancia fue correcta!! Felicidades";
            endCanvas.gameObject.SetActive(true);
        }

    }
}
