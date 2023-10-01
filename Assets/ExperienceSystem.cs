
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ExperienceSystem : MonoBehaviour
{
    public int experience = 0;
    public int level = 1;
    public int experienceToNextLevel = 100;
    public float experienceToNextLevelMultiplier = 1.1f;
    public int experienceToNextLevelAddition = 10;
    public GameObject levelUp;
    public GameObject levelUpMenu;
    public Slider levelBar;
    public GameObject fill;
    float lerpValue;

    public void LevelUp()
    {
        level++;
        experience = 0;
  
        experienceToNextLevel = (int)(experienceToNextLevel * experienceToNextLevelMultiplier) + experienceToNextLevelAddition;
        levelBar.maxValue = experienceToNextLevel;
        StartCoroutine(LevelUpAnim());
        
    }

    private void Update()
    {
        if (experience <= 0)
        {
            fill.SetActive(false);
        }
        else
        {
            fill.SetActive(true);
        }
        lerpValue = Mathf.Lerp(levelBar.value, experience, 25f *Time.deltaTime);
        levelBar.value = lerpValue;
    }
    public void addExperience(int experience)
    {
        this.experience += experience;
        if (this.experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    public void showMenu()
    {
        levelUpMenu.SetActive(true);
        setUpRandOptions();
    }

    public void chosenOption()
    {
        Time.timeScale = 1;
        levelUpMenu.SetActive(false);
    }

    IEnumerator LevelUpAnim()
    {
        levelUp.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        levelUp.SetActive(false);
        Time.timeScale = 0;
        showMenu();
    }



    [System.Serializable]
    public class RandomOption
    {
        public UnityEvent action;
        public Sprite icon;
        public string description;
    }

    public List<Button> randomButtons;
    public Player player;
    public List<RandomOption> randomOptions;

    /*void Start()
    {
        randomButton.onClick.AddListener(OnRandomButtonClick);

        // Initialize the list of random options
        randomOptions = new List<RandomOption>
        {
            *//*new RandomOption { action = () => player.addAttackDmg(5),icon , description = "Add attack damage" },
            // ... Add other options here*//*
        };
    }

    

    void OnRandomButtonClick()
    {
        int randomIndex = UnityEngine.Random.Range(0, randomOptions.Count);
        RandomOption selectedOption = randomOptions[randomIndex];
        selectedOption.action();

        // Use selectedOption.icon and selectedOption.description as needed
    }
*/

    

    public void setUpRandOptions()
    {
        foreach (Button button in randomButtons)
        {
            int randomIndex = UnityEngine.Random.Range(0, randomOptions.Count);
            RandomOption selectedOption = randomOptions[randomIndex];
            button.onClick.AddListener(selectedOption.action.Invoke);
            button.GetComponentInChildren<SpriteRenderer>().sprite = selectedOption.icon;
            button.GetComponentInChildren<TextMeshProUGUI>().text = selectedOption.description;
        }
    }

    private void Start()
    {
        setUpRandOptions();
    }

}
