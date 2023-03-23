using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    GameObject controller;
    SpriteRenderer renderer;
    public Sprite[] faces;
    public Sprite back;
    public int currentFace;

    public void Awake() {
        // Initialize gameobjects and components
        controller = GameObject.Find("GameController");
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown() {
        // If cards is currently flipped
        if (renderer.sprite == back) {
            if (controller.GetComponent<GameController>().TwoCardsSelected()) {
                renderer.sprite = faces[currentFace];
                controller.GetComponent<GameController>().AddCards(this.gameObject);
            }
        }
    }

    public void ShowBack() {
        // Show back side of card
        renderer.sprite = back;
    }
}
