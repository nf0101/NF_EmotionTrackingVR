/*
 * @author nf0101
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VRButtonsController : MonoBehaviour
{
    public UnityEvent onPress;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    SceneController controller;
    public GameObject eventSys;
    TestExpr testExpr;
    static bool isActive;
    public GameObject ResetButton;
    string defaultString = "Try to make this expression: ";
    // Start is called before the first frame update
    void Start()
    {
        controller = eventSys.GetComponent<SceneController>();
        isPressed = false;
        isActive = true;
        testExpr = eventSys.GetComponent<TestExpr>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("HappyScene2");
    }

    IEnumerator WaitButton()
    {
        yield return new WaitForSeconds(2);

        if (gameObject != ResetButton)
        {
            foreach (GameObject button in GameObject.FindGameObjectsWithTag("Button"))
            {
                button.SetActive(false);
            }
            ResetButton.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        
            if (!isPressed)
            {

                onPress.Invoke();
                isPressed = true;
            }
            if (other.tag.Equals("Hands"))
            { 
            if (transform.parent.name.Equals("ResetButton"))
                {
                    gameObject.transform.localPosition = new Vector3(0, 0.02f, -0.0579f);
                    StartCoroutine(WaitAndLoadScene());
            }
            if (isActive)
            {
                gameObject.transform.localPosition = new Vector3(0, 0.02f, -0.0579f);
               
                switch (transform.parent.name)
                {
                    case "HappyButton":
                        testExpr.expressionChoosedString = defaultString + "Happiness";
                        testExpr.espressioneString = "Happiness";
                        testExpr.expressionChoosed = 1;    
                        break;

                    case "AngryButton":
                        testExpr.expressionChoosedString = defaultString + "Anger";
                        testExpr.espressioneString = "Anger";
                        testExpr.expressionChoosed = 5;
                        break;

                    case "SadButton":
                        testExpr.expressionChoosedString = defaultString + "Sadness";
                        testExpr.espressioneString ="Sadness";
                        testExpr.expressionChoosed = 3;
                        break;

                    case "ScareButton":
                        testExpr.expressionChoosedString = defaultString + "Fear";
                        testExpr.espressioneString = "Fear";
                        testExpr.expressionChoosed = 4;
                        break;

                    case "SurpriseButton":
                        testExpr.expressionChoosedString = defaultString + "Surprise";
                        testExpr.espressioneString = "Surprise";
                        testExpr.expressionChoosed = 2;
                        break;

                    case "DisgustButton":
                        testExpr.expressionChoosedString = defaultString + "Disgust";
                        testExpr.espressioneString = "Disgust";
                        testExpr.expressionChoosed = 6;
                        break;

                    case "ContemptButton":
                        testExpr.expressionChoosedString = defaultString + "Contempt";
                        testExpr.espressioneString = "Contempt";
                        testExpr.expressionChoosed = 7;
                        break;


                }
                testExpr.timer = true;
                isActive = false;
                StartCoroutine(WaitButton());
            }

        }

    }
}
