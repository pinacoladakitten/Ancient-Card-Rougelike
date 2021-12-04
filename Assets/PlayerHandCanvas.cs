using System.Collections;
using System.Collections.Generic;
using static AssScript;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHandCanvas : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab = null;
    [SerializeField] GameObject strikePrefab = null;

    // Our hand in game
    private List<GameObject> CardsHand = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        if(cardPrefab)
        {
            DeckToSpawn.Add(cardPrefab);
            DeckToSpawn.Add(strikePrefab);
            DeckToSpawn.Add(cardPrefab);
            DeckToSpawn.Add(strikePrefab);
            DeckToSpawn.Add(cardPrefab);
            //DeckToSpawn.Add(cardPrefab);
            //DeckToSpawn.Add(strikePrefab);
            //DeckToSpawn.Add(cardPrefab);
            //DeckToSpawn.Add(strikePrefab);
            //DeckToSpawn.Add(cardPrefab);

            HorizontalLayoutGroup layout = GetComponent<HorizontalLayoutGroup>();

            for (int i = 0; i < DeckToSpawn.Count; i++)
            {
                AddCardToHand(DeckToSpawn[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("ASS");

            if (cardPrefab != null)
            {
                AddCardToHand(cardPrefab);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("FARTO");

            if (cardPrefab != null)
            {
                RemoveCardFromHand(CardsHand[CardsHand.Count-1]);
            }
        }
    }

    void AddCardToHand(GameObject cardObj)
    {
        // Create the card
        GameObject card = Instantiate(cardObj, new Vector2(0, 0), Quaternion.Euler(Vector3.forward)) as GameObject;

        // Rebuild the layout of the canvas to sync with the cards
        RectTransform rect = GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

        // Set the card's parent
        card.transform.SetParent(transform, false);

        // Add card to our hand array
        CardsHand.Add(card);
    }

    void RemoveCardFromHand(GameObject cardObj)
    {
        // Remove the card from the array
        CardsHand.Remove(cardObj);
        Destroy(cardObj);
    }


}
