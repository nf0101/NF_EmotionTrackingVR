/*
 * @author nf0101
 */


using Metaface.Debug;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OVRFaceExpressions.FaceExpression;

public class TestExpr : MonoBehaviour
{
    [SerializeField]
    private OVRFaceExpressions faceExpressions;

    [SerializeField]
    private NFFaceExpressions nffe;

    [SerializeField]
    private Transform faceWeightLayout;

    public Text value;
    public TMP_Text espressione;
    public Text value2t;
    
    private Dictionary<OVRFaceExpressions.FaceExpression, bool> expressionsList = new Dictionary<OVRFaceExpressions.FaceExpression, bool>();
    float weight;
    OVRFaceExpressions.FaceExpression[] ExprArray = (OVRFaceExpressions.FaceExpression[]) Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression));
    string expressionString;
    // Start is called before the first frame update
    void Start()
    {
        foreach (OVRFaceExpressions.FaceExpression e in Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression))){
            expressionsList.Add(e, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression)).Length; i++)
        {
            if (faceExpressions.GetWeight(ExprArray[i]) > 0.25)
            {
                expressionsList[ExprArray[i]] = true;
            }
            else
            {
                expressionsList[ExprArray[i]] = false;
            }

            if (expressionsList[CheekRaiserL] && expressionsList[CheekRaiserL] && expressionsList[LipCornerPullerL] && expressionsList[LipCornerPullerR])
            {
                espressione.text = "Felice/Gioia";
            }
            else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] && expressionsList[UpperLidRaiserL] &&
                expressionsList[UpperLidRaiserR] && expressionsList[JawDrop])
            {
                espressione.text = "Sorpresa";
            }

            else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && expressionsList[LipCornerDepressorL] &&
                expressionsList[LipCornerDepressorR])
            {
                espressione.text = "Triste/Tristezza";
            }

            else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] && expressionsList[BrowLowererL]
                && expressionsList[BrowLowererR] && expressionsList[BrowLowererR] && expressionsList[UpperLidRaiserL] && expressionsList[UpperLidRaiserR] && expressionsList[LidTightenerL] &&
                expressionsList[LidTightenerR] && expressionsList[LipStretcherL] && expressionsList[LipStretcherR])
            {
                espressione.text = "Paura";
            }

            else if (expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && expressionsList[BrowLowererR] && expressionsList[UpperLidRaiserL] && 
                expressionsList[UpperLidRaiserR] && expressionsList[LidTightenerL] && expressionsList[LidTightenerR] && expressionsList[LipTightenerL] && expressionsList[LipTightenerR])
            {
                espressione.text = "Rabbia";
            }

            else if (expressionsList[NoseWrinklerL] && expressionsList[NoseWrinklerR] && expressionsList[LipCornerDepressorL] && expressionsList[LipCornerDepressorR] &&
                expressionsList[LowerLipDepressorL] && expressionsList[LowerLipDepressorR])
            {
                espressione.text = "Disgusto";
            }

            else if ((expressionsList[LipCornerPullerL] || expressionsList[LipCornerPullerR]) && (expressionsList[OuterBrowRaiserL] || expressionsList[OuterBrowRaiserR]))
            {
                espressione.text = "Disprezzo";
            }
            else
            {
                espressione.text = "Neutro";
            }
        } 
    }

    public string DetectExpression()
    {
        string expression = "Neutro";

        for (int i = 0; i <= Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression)).Length; i++)
        {
            if (faceExpressions.GetWeight(ExprArray[i]) > 0.25)
            {
                expressionsList[ExprArray[i]] = true;
            }
            else
            {
                expressionsList[ExprArray[i]] = false;
            }

            if (expressionsList[CheekRaiserL] && expressionsList[CheekRaiserL] && expressionsList[LipCornerPullerL] && expressionsList[LipCornerPullerR])
            {
                expression = "Felice/Gioia";
            }
            else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] && expressionsList[UpperLidRaiserL] &&
                expressionsList[UpperLidRaiserR] && expressionsList[JawDrop])
            {
                expression = "Sorpresa";
            }

            else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && expressionsList[LipCornerDepressorL] &&
                expressionsList[LipCornerDepressorR])
            {
                expression = "Triste/Tristezza";
            }

            else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] && expressionsList[BrowLowererL]
                && expressionsList[BrowLowererR] && expressionsList[BrowLowererR] && expressionsList[UpperLidRaiserL] && expressionsList[UpperLidRaiserR] && expressionsList[LidTightenerL] &&
                expressionsList[LidTightenerR] && expressionsList[LipStretcherL] && expressionsList[LipStretcherR])
            {
                expression = "Paura";
            }

            else if (expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && expressionsList[BrowLowererR] && expressionsList[UpperLidRaiserL] &&
                expressionsList[UpperLidRaiserR] && expressionsList[LidTightenerL] && expressionsList[LidTightenerR] && expressionsList[LipTightenerL] && expressionsList[LipTightenerR])
            {
                expression = "Rabbia";
            }

            else if (expressionsList[NoseWrinklerL] && expressionsList[NoseWrinklerR] && expressionsList[LipCornerDepressorL] && expressionsList[LipCornerDepressorR] &&
                expressionsList[LowerLipDepressorL] && expressionsList[LowerLipDepressorR])
            {
                expression = "Disgusto";
            }

            else if ((expressionsList[LipCornerPullerL] || expressionsList[LipCornerPullerR]) && (expressionsList[OuterBrowRaiserL] || expressionsList[OuterBrowRaiserR]))
            {
                expression = "Disprezzo";
            }
            else
            {
                expression = "Neutro";
            }

        }
        return expression;
    }

    //void Update()
    //{
    //    //test1();
    //    nffe.DetectExpression(ref expressionString);
    //    espressione.SetText(expressionString);
    //}
}
