using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QTEtest : MonoBehaviour
{

    bool QTEactive;
    public KeyCode[] sequence = new []
    {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D
    };
    KeyCode[] QTEsequence;
    public int QTElength;
    public TextMeshProUGUI debug;
    bool keyPressed;
    int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QTEactive = false;
        keyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!QTEactive && Input.GetKeyDown(KeyCode.Space))
        {
            QTEactive = true;
            KeyCode[] QTEsequence = QTE(QTElength);
            string result = string.Join(", ", QTEsequence);
            debug.text = result;
            count = 0;
        }

        if (QTEactive)
        {
            if(Input.anyKeyDown)
            {
                QTECheck();
            }
        }
        
    }

    KeyCode[] QTE(int length)
    {
        KeyCode[] newSeq = new KeyCode[length];

        for (int i = 0; i < length; i++)
        {
            var key = sequence[Random.Range(0, sequence.Length)];
            newSeq[i] = key;
        }

        return newSeq;
    }

    void QTECheck()
    {
        if (Input.GetKeyDown(QTEsequence[0]))
        {
            if (count == QTEsequence.Length - 1)
            {
                Debug.Log("Complete!");
                QTEactive = false;
            }
            else
            {
                Debug.Log("Success! " + count + " of " + QTElength);
                count++;
            }
        }
        else if (!Input.GetKeyDown(QTEsequence[0]))
        {
            Debug.Log("Failed.");
            QTEactive = false;
        }
    }
}
