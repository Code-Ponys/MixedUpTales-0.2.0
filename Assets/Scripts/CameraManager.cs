using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;
using System;


public class CameraManager : MonoBehaviour {

    public Camera MainCam;
    public Field Field;

    public int max_x = 0;
    public int min_x = 0;
    public int max_y = 0;
    public int min_y = 0;

    GameObject F;

    // Use this for initialization
    void Start() {
        F = GameObject.Find("Field");
    }

    // Update is called once per frame
    void Update() {

    }
    public Vector3 GetCenter() {

        float center_x = (((min_x * -1) + max_x) / 2) + min_x;
        float center_y = (((min_y * -1) + max_y) / 2) + min_y;
        Vector3 result = new Vector3((float)center_x, (float)center_y, -10);
        return result;
    }

    public int GetCameraSize() {
        int size_x = max_x - min_x;
        int size_y = max_y - min_y;

        int size = Math.Max(size_x, size_y) + 8;
        return Math.Max(4, size / 2);
    }

    public void CenterCamera(int x, int y) {
        CalculateSize(x, y);
        MainCam.transform.position = GetCenter();
        MainCam.orthographicSize = GetCameraSize();
    }

    public void CalculateSize(int x, int y) {

        if (x > max_x) {
            max_x = x;
        }
        if (x < min_x) {
            min_x = x;
        }
        if (y > max_y) {
            max_y = y;
        }
        if (y < min_y) {
            min_y = y;
        }
    }
    public void RenewCameraPosition() {
        min_x = 0;
        max_x = 0;
        min_y = 0;
        max_y = 0;

        if (F.GetComponent<Field>().cardsOnField.Count == 0) {
            return;
        }

        foreach (GameObject Card in F.GetComponent<Field>().cardsOnField) {
            CalculateSize(Card.GetComponent<Card>().x, Card.GetComponent<Card>().y);
        }
    }
}
