using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManDialogue : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> dialogues;
    [SerializeField] private float activeDialogueTime;
    [SerializeField] private float timeBetweenDialogue;
    private float _currentTime;
    private float _currentTime2;
    private List<Sprite> possibleDialogues;
    private int chosenSprite;
    private int prevSprite;

    private void Start()
    {
        possibleDialogues = dialogues;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        _currentTime2 += Time.deltaTime;
        if (_currentTime >= timeBetweenDialogue)
        {
            randomDialogue();
            _currentTime = 0;
            _currentTime2 = 0;
        }
        if(_currentTime2 >= activeDialogueTime)
        {
            spriteRenderer.sprite = null;
        }
    }

    private void randomDialogue()
    {
        chosenSprite = Random.Range(0, possibleDialogues.Count-1);
        if(prevSprite == chosenSprite)
        {
            if(chosenSprite == possibleDialogues.Count - 1)
            {
                chosenSprite = 0;
            }
            else
            {
                chosenSprite++;
            }
        }
        spriteRenderer.sprite = dialogues[chosenSprite];
        prevSprite = chosenSprite;
    }

}
