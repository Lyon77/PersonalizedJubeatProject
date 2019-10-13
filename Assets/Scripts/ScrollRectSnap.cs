using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{
    public RectTransform panel; //ScrollPanel
    public RectTransform center;

    private Button[] bttn;
    private float[] distance;
    private float[] distReposition;
    private bool dragging = false;
    private int bttnDistance;
    private int minButtonNum;
    

    void Start() {
        //this is to get the Button list because the amount of buttons is unknown
        bttn = panel.GetComponentsInChildren<Button>();

        int bttnLength = bttn.Length;
        distance = new float[bttnLength];
        distReposition = new float[bttnLength];

        //Get Distance between buttons
        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    void Update() {
        for (int i = 0; i < bttn.Length; i++) {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            if (distReposition[i] > 11f / 5f * bttn.Length) {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                //loop buttons
                Vector2 newAnchor = new Vector2(curX + bttn.Length * bttnDistance, curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchor;
            }

            if (distReposition[i] < -11f / 5f * bttn.Length) {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                //loop buttons
                Vector2 newAnchor = new Vector2(curX - bttn.Length * bttnDistance, curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchor;
            }
        }

        float minDistance = Mathf.Min(distance);

        for (int i = 0; i < bttn.Length; i++) {
            if (minDistance == distance[i]) {
                minButtonNum = i;
                break;
            }
        }

        //check if we are dragging
        if (!dragging) {
            //MoveToButton(minButtonNum * -bttnDistance);
            MoveToButton(-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
        }
    }

    private void MoveToButton(float position) {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPos = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPos;
    }

    public void StartDrag() {
        dragging = true;
    }

    public void EndDrag() {
        dragging = false;
    }
}
