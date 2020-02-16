using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class WordAlgorithm : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] private List<GameObject> letterObjects;
    [SerializeField] private List<GameObject> StaticAlfabet;
    [SerializeField] private GameObject letterBox;
    [SerializeField] private GameObject CanvasObject;
    public List<GameObject> letterGuesser;
    private GameObject tempGameObject;
    private GameObject InstantiateHolder;


    [Header("WordRelated")]
    [SerializeField] private List<string> wordList;
    public string word;
    public char[] WordCharacter;

    [Header("UI")]
    [SerializeField] private Text letterText;
    [SerializeField] private Text lifeTally;
    [SerializeField] private Image[] RestartMessage;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite EmptyCircle;
    [SerializeField] private Sprite CrossedCirle;

    [Header("TimerRelated")]
    [SerializeField] private float spawnTimerLimit;
    private float spawnTimer;

    [Header("LifesAndSuch")]
    [SerializeField] private int lifes;
    [SerializeField] private int numberOfLifes;
    [SerializeField] private int WinningCount;
    private int WinningMax;
    public bool GameOver = false;

    [Header("Miscellaneous")]
    [SerializeField] private Vector2[] SpawnPoints;
    private Rigidbody2D letterRigidbody;
    private Transform LetterInObject;
    private CharacterScript charScript;
    private LetterTextSelector letterTextSelector;
    private Vector2 pos;
    private int NUM = 31;
    public List<string> ObjectsInScene;
    [SerializeField] private List<int> Coordinates;


    // In the start all classes are executed and variables are assigned.
    void Start()
    {
        this.ObjectsInScene = new List<string>();
        this.Coordinates = new List<int>();
        GenerateAWord();
        SpawnLetter();
        WinningMax = word.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //It checks for the timer to be past the time limit and if you aren't gameover, to make sure that after you lose, no more letters spawn. otherwise it spawns letters.
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTimerLimit && GameOver == false)
        {
            SpawnPlayletter();
            spawnTimer = 0;
        }
        //If your lifes fall bellow 0, gameover will be set to true causing no more letters to spawn and the gameover screen to apear.
        if (lifes <= 0 && GameOver == false)
        {
            RestartMessage[0].gameObject.SetActive(true);
            GameOver = true;
            print("Defeat");
        }
        //If you have the word finnished, then you win, causing the winning text to apear.
        if (WinningCount == WinningMax && GameOver == false)
        {
            RestartMessage[1].gameObject.SetActive(true);
            GameOver = true;
            print("Victory");
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RestartScene();
        }

        LifeVisualizer();

        print(WinningCount);
    }

    //this calculates the coördinate of the letters chosen for the word in the alphabet, by going through a list with all letters.
    private void GetIndexes(string str, int n)
    {
        for (int i = 0; i < n; i++)
        {

            var value = str[i] - 1 & NUM;
            Coordinates.Add(value);
        }
    }
    //A random word is picked from a word list.
    private void GenerateAWord()
    {
        int wordNumber = Random.Range(0, wordList.Count);
        word = wordList[wordNumber];
    }

    /// <summary>
    /// A temporary GameObject is created to instantiate a new letter, this letter is spawned next to the previous one to ensure the word having equal spacing..
    /// After making the var, the letter gets added to the list of guesser letters. after instantiating the box, the letter gets a text assigned.
    /// This is in the order of the word, after that it gets given a parrent to make the hierarchy not overflooded.
    /// then the letter is set to false untill guessed. 
    /// </summary>
    private void SpawnLetter()
    {
        for (int i = 0; i < word.Length; i++)
        {
            var letterbox = Instantiate(letterBox, new Vector2(800 + i * 75, 1035), Quaternion.identity);
            letterGuesser.Add(letterbox);
            var letterFromWord = Instantiate(letterText, letterbox.transform.position, Quaternion.identity);
            letterFromWord.text = word[i].ToString();
            letterFromWord.transform.SetParent(letterbox.transform);
            letterFromWord.transform.gameObject.SetActive(false);

            letterbox.transform.SetParent(CanvasObject.transform);

            letterbox.GetComponent<CharacterScript>().Character = word[i].ToString();

            //print(word[i]);
            string str = word[i].ToString();
            int n = str.Length;
            GetIndexes(str, n);
        }
    }

    /// <summary>
    /// A random letter is generated here. first a random spawn position is given.
    /// After that its checked if the latter is fake or true by another random number. If its below 3 its a good letter. 
    /// This means it will take a random letter from the list of available letters that arent guessed yet. then it will spawn a letter that is in the word.
    /// if its above 3 it will spawn a random letter from the standard letter list.
    /// </summary>
    private void SpawnPlayletter()
    {
        int speed = 2;
        int RandomPoint = Random.Range(0, SpawnPoints.Length);

        int wordLetterOrFake = Random.Range(0, 9);

        if (RandomPoint == 0 || RandomPoint == 1)
        {
            pos = new Vector2(Random.Range(SpawnPoints[0].x, SpawnPoints[1].x), Random.Range(SpawnPoints[0].y, SpawnPoints[1].y));
        }
        else if (RandomPoint == 2 || RandomPoint == 3)
        {
            pos = new Vector2(Random.Range(SpawnPoints[2].x, SpawnPoints[3].x), Random.Range(SpawnPoints[2].y, SpawnPoints[3].y));
        }
        else if (RandomPoint == 4 || RandomPoint == 5)
        {
            pos = new Vector2(Random.Range(SpawnPoints[4].x, SpawnPoints[5].x), Random.Range(SpawnPoints[4].y, SpawnPoints[5].y));
        }

        if (wordLetterOrFake <= 3)
        {
            int RandomCorrect = Random.Range(0, Coordinates.Count);

            try { InstantiateHolder = StaticAlfabet[Coordinates[RandomCorrect]]; }
            catch (System.Exception e)
            {
                print(e.Message);
            }
            if (!ObjectsInScene.Contains(InstantiateHolder.name) || ObjectsInScene == null)
            {
                tempGameObject = Instantiate(InstantiateHolder, pos, Quaternion.identity);
                ObjectsInScene.Add(tempGameObject.gameObject.transform.GetChild(0).name);
            }
        }
        else
        {
            InstantiateHolder = letterObjects[Random.Range(0, letterObjects.Count)];
            if (!ObjectsInScene.Contains(InstantiateHolder.name) || ObjectsInScene == null)
            {
                tempGameObject = Instantiate(InstantiateHolder, pos, Quaternion.identity);
                ObjectsInScene.Add(tempGameObject.gameObject.transform.GetChild(0).name);
            }

        }

        letterRigidbody = tempGameObject.GetComponent<Rigidbody2D>();
        letterRigidbody.velocity = new Vector2(1, 0) * speed;
    }
    /// <summary>
    /// First it will find the letter that was selected through a raycast that hit the object. tafter that it will check if the object name was inside the word.
    /// If it was it will go into the word and go through the available letters. 
    /// When it has found the right letter, it will set the letter to active and remove it from the Coordinates list to avoid needing to guess it again.
    /// Otherwise it will just remove one of your lifes.
    /// </summary>
    public void RevealLetter()
    {
        letterTextSelector = GetComponent<LetterSelector>().textSelector;

        if (word.ToUpper().Contains(letterTextSelector.gameObject.transform.GetChild(0).name))
        {
            for (int j = 0; j < letterGuesser.Count; j++)
            {
                charScript = letterGuesser[j].GetComponent<CharacterScript>();
                if (charScript.Character.ToUpper() == letterTextSelector.gameObject.transform.GetChild(0).name)
                {
                    LetterInObject = letterGuesser[j].transform.GetChild(0);
                    LetterInObject.transform.gameObject.SetActive(true);

                    for (int i = 0; i < letterObjects.Count; i++)
                    {
                        if (letterObjects[i].name == letterTextSelector.gameObject.transform.GetChild(0).name)
                        {
                            for (int k = 0; k < Coordinates.Count; k++)
                            {
                                string str = letterObjects[i].name;
                                int n = str.Length;
                                var value = str[0] - 1 & NUM;
                                Coordinates.Remove(value);
                            }
                            letterObjects.Remove(letterObjects[i]);
                        }
                    }
                }
            }
        }
        else
        {
            lifes -= 1;
            for (int i = 0; i < letterObjects.Count; i++)
            {
                if (letterObjects[i].name == letterTextSelector.gameObject.transform.GetChild(0).name)
                {
                    letterObjects.Remove(letterObjects[i]);
                }
            }
        }
    }

    public void AddPoint()
    {
        WinningCount += 1;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("LetterPrototype");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    //this is just for the visualisation of the lifes (the crosses that apear through boxes on the screen.
    private void LifeVisualizer()
    {
        if (lifes > numberOfLifes)
        {
            lifes = numberOfLifes;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lifes)
            {
                hearts[i].sprite = EmptyCircle;
            }
            else
            {
                hearts[i].sprite = CrossedCirle;
            }

            if (i < numberOfLifes)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}