using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverController gameOverController;
    GameObject card;
    int cardCount = 16; // Change here if count of cards changes
    List<int> faces;
    GameObject[] flippedCards = { null, null };
    int possibleMatches = 0;
    int matchesFound = 0;

    void Awake() {
        // Initialize variables
        faces = new List<int>();
        card = GameObject.Find("Card");
        possibleMatches = cardCount / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        bool faceNotAdded = true;

        // Get a random face for the initial card
        int rngFace = Random.Range(0, 8);
        card.GetComponent<MainCard>().currentFace = rngFace;
        faces.Add(rngFace);

        // Get first card's position
        float initX = card.transform.position.x;
        float initY = card.transform.position.y;

        // Next card's position
        float currentX = initX + 4;
        float currentY = initY;

        for (int i = 1; i < cardCount; i++) {
            var tempCard = Instantiate(card, new Vector3(currentX, currentY, 0), Quaternion.identity);
            currentX += 4;

            while (faceNotAdded) {
                // Get random face
                rngFace = Random.Range(0, 8);

                // Counter to see how many of rngFace is in the list
                int finalCount = 0;

                // Loop through list
                for (int j = 0; j < faces.Count; j++) {
                    // If rngFace is present in list
                    if (faces[j] == rngFace) {
                        finalCount++;

                        // If there is more than two rngFace
                        if (finalCount >= 2) {
                            break;
                        }
                    }
                }

                // If less than 2 rngFace is in the list then add
                if (finalCount < 2) {
                    tempCard.GetComponent<MainCard>().currentFace = rngFace;
                    faces.Add(rngFace);
                    faceNotAdded = false;
                }
            }

            faceNotAdded = true;

            // If four cards are counted, move Y down, start from initX again
            if ((i + 1) % 4 == 0) {
                currentX = initX;
                currentY -= 2.5f;
            }
        }
    }

    public bool TwoCardsSelected() {
        // Return if two faces are turned or not
        return flippedCards[0] == null || flippedCards[1] == null;
    }

    public void AddCards(GameObject card) {
        // Go through the two faces
        if (flippedCards[0] == null) {
            flippedCards[0] = card;
        }
        else if (flippedCards[1] == null) {
            flippedCards[1] = card;
            StartCoroutine(CheckMatch());
        }
    }

    public void RemoveCards() {
        // Reset faces
        flippedCards[0] = null;
        flippedCards[1] = null;
    }

    public IEnumerator CheckMatch() {
        // If the faces do not match
        if (flippedCards[0].GetComponent<MainCard>().currentFace != flippedCards[1].GetComponent<MainCard>().currentFace)
        {
            yield return new WaitForSecondsRealtime(1f);

            flippedCards[0].GetComponent<MainCard>().ShowBack();
            flippedCards[1].GetComponent<MainCard>().ShowBack();
            RemoveCards();
        }
        else {
            // Else if the faces match
            yield return new WaitForSecondsRealtime(0f);
            RemoveCards();
            matchesFound++;

            if (matchesFound == possibleMatches)
            {
                gameOverController.Setup();
            }
        }
    }
}
