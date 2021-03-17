using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class CastleScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;
    public Text goMarkerToContinue;

    public GameObject TextContainer;

    bool skipScene = true;
    bool max = false;

    private bool didMarkerDisappear;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);

        refreshUI();

        MarkerManagerScript.S.Reset();
    }

    void refreshUI()
    {
        MarkerManagerScript.pastLocation = MarkerManagerScript.currentLocation;

        eraseUI();

        Text storyText = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Text;

        string text = "";

        if (max == false)
        {
            text = loadStoryChunkMax();
        }
        else
        {
            text = loadStoryChunk();
        }

        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            text = tags[0] + " - " + text;
        }

        storyText.text = text;
        storyText.transform.SetParent(TextContainer.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Vector3 locate = new Vector3(0f, 0f, 0f);

            Button choiceButton = Instantiate(buttonPrefab, locate, Quaternion.identity) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate
            {
                chooseStoryChoice(choice);
            });

        }
    }

    void eraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        foreach (Transform child in TextContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }

    // Update is called once per frame
    void Update()
    {

        if (this.transform.childCount == 0)
        {
            goMarkerToContinue.enabled = true;

            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    refreshUI();
                }
            }
        }

        else if (this.transform.childCount == 1)
        {
            goMarkerToContinue.enabled = false;
            if (MarkerManagerScript.goMarker)
            {
                if ((string)story.currentChoices[0].text == "Fight")
                {
                    skipScene = false;
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    refreshUI();
                    GameManagerScript.NextScene(skipScene);
                }
            }
        }


        else if (this.transform.childCount == 2)
        {
            goMarkerToContinue.enabled = false;

            switch (MarkerManagerScript.currentLocation)
            {
                case 1:
                case 4:
                case 7:
                    if (MarkerManagerScript.pastLocation != MarkerManagerScript.currentLocation)
                    {
                        story.ChooseChoiceIndex(0);
                        refreshUI();
                    }
                    else
                    {
                        if (MarkerManagerScript.palmMarker && didMarkerDisappear)
                        {
                            story.ChooseChoiceIndex(0);
                            refreshUI();
                        }
                        
                        if (!MarkerManagerScript.palmMarker) didMarkerDisappear = true;
                    }
                    break;
                
                case 3:
                case 6:
                case 9:
                    if (MarkerManagerScript.pastLocation != MarkerManagerScript.currentLocation)
                    {
                        story.ChooseChoiceIndex(1);
                        refreshUI();
                    }
                    else
                    {
                        if (MarkerManagerScript.palmMarker && didMarkerDisappear)
                        {
                            story.ChooseChoiceIndex(1);
                            refreshUI();
                        }
                        
                        if (!MarkerManagerScript.palmMarker) didMarkerDisappear = true;
                    }
                    break;
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                story.ChooseChoiceIndex(0);
                refreshUI();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                story.ChooseChoiceIndex(1);
                refreshUI();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                story.ChooseChoiceIndex(2);
                refreshUI();
            }
        }

    }

    string loadStoryChunkMax()
    {
        string text = "";

        if (story.canContinue) //if there is more content
        {
            text = story.ContinueMaximally(); //continue until next options or no content. will return string
        }

        max = true;
        return text;
    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue) //if there is more content
        {
            text = story.Continue(); //continue until next options or no content. will return string
        }

        return text;
    }
}

