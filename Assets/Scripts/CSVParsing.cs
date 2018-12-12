using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class CSVParsing : MonoBehaviour
{
    public TextAsset csvFile;
    public enum weekEnum { Week1=0,Week2=5,Week3=10};
    public weekEnum week;
    public GameObject stepPrefab;
    public GameObject hrPrefab;
    private int weekSelectedValue;

    

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    private int[] stepScaleOptions = { 5, 8, 11, 15 };
    private float[] stepYAxisPosition = { 24.7f, 33.7f, 41.5f, 54.2f };

    List<string> dates = new List<string>();
    List<string> steps = new List<string>();
    List<string> hr = new List<string>();

    void Start()
    {
        readData();
        visualize(0, 0);
    }

    private void Update()
    {
    }
    // Read data from CSV file
    private void readData()
    {
        string[] records = csvFile.text.Split(lineSeperater);
      
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            dates.Add(fields[0]);
            steps.Add(fields[1]);
            hr.Add(fields[2]);
        }
    }

    public void visualize(int mode, int weekID)
    {
        Debug.Log(mode);
        GameObject[] trees;
        GameObject tempTree;
        trees = GameObject.FindGameObjectsWithTag("data");

        foreach (GameObject tree in trees)
        {
            Destroy(tree);
        }

        if (weekID == 0)
            weekSelectedValue = 0;
        else if (weekID == 1)
            weekSelectedValue = 5;
        else
            weekSelectedValue = 10;

        List<string> tempDates = dates.GetRange(weekSelectedValue, 5);
        List<string> tempSteps = steps.GetRange(weekSelectedValue, 5);
        List<string> tempHr = hr.GetRange(weekSelectedValue, 5);
        for (int i = 0; i < tempDates.Count; i++)
        {
          
            int tempStepScale = 0;
            float tempStepYAxisPosition = 0;
            int tempStep = int.Parse(tempSteps[i]);
            int tempHR_i = int.Parse(tempHr[i]);

            if((tempStep >2000 && tempStep <= 3500 && mode==0) || (tempHR_i > 60 && tempHR_i <= 75 && mode == 1))
            {
                tempStepScale = 5;
                tempStepYAxisPosition = 24.7f;

            }
            else if ((tempStep > 3500 && tempStep <= 5000 && mode == 0) || (tempHR_i > 75 && tempHR_i <= 90 && mode == 1))
            {
                tempStepScale = 8;
                tempStepYAxisPosition = 33.7f;
            }
            else if ((tempStep > 5000 && tempStep <= 6500 && mode == 0) || (tempHR_i > 90 && tempHR_i <= 105 && mode == 1))
            {
                tempStepScale = 11;
                tempStepYAxisPosition = 41.5f;
            }
            else if((tempStep > 6500 && mode == 0) || (tempHR_i > 105 && mode == 1))
            {
                tempStepScale = 15;
                tempStepYAxisPosition = 54.2f;
            }
         
            if (mode == 0)
                 tempTree = Instantiate(stepPrefab, new Vector3(-70F, tempStepYAxisPosition, (i * 40) - 35), Quaternion.identity);
            else
                 tempTree = Instantiate(hrPrefab, new Vector3(-70F, tempStepYAxisPosition, (i * 40) - 35), Quaternion.identity);

            tempTree.transform.localScale = new Vector3(tempStepScale, tempStepScale, tempStepScale);
        }

    }

}