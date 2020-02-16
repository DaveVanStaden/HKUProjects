using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExeOpener : MonoBehaviour
{
    [SerializeField] private Image gameImage;
    [SerializeField] private Text gameText;
    [SerializeField] private Text titleText;
    [SerializeField] private List<Sprite> images;
    [SerializeField] private GameObject boxHolder;
    private enum Games
    {
        LetterGame,
        MazeBall,
        PackageGame,
        OtterGame,
        BaloonGame,
        DeliveryGame,
        MinerGame,
        None
    };

    private Games games;

    // Start is called before the first frame update
    void Start()
    {
        //OtterGame();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EnableObject()
    {
        boxHolder.SetActive(true);
    }
    /// <summary>
    /// Below you find a void for all games. 
    /// here the game gets assigned from an enum, then the sprite for a image gets set and the text to pop up once you click on a button.
    /// </summary>
    public void OtterGame()
    {
        this.games = Games.OtterGame;
        gameImage.sprite = images[3];
        gameText.text = "During this exercise, the client can control the otter by using the pen to touch the screen. The goal is to collect as many fish as possible. Points can also be earned by jumping through the hoops above the water or collecting the gems floating in the air. Jumping is done by lifting the controller from the screen for a moment. Colliding with the puffer fish will reduce the players points. The grip that is used is the pencil grip.\n \nThis is an exercise for people with reduced stability, strength and endurance of the fingers, hand and wrist.\n \nCognitive goals that can be practiced with this exercise: timing and error recognition.";
        titleText.text = "OtterGame";
    } 
    public void LetterGame()
    {
        this.games = Games.LetterGame;
        gameImage.sprite = images[1];
        gameText.text = "This exercise is similar to the old-fashioned game Hangman. Different letters pass by on the screen and they can select the letters by using the controller to click on them and eventually guess the word by clicking all letters. The grip that is used is the ball grip.\n \nThis exercise is developed for people with reduced strength in their fingers, coordination problems and reduced endurance of the hand and wrist.\n \nCognitive goals that can be trained with the exercise are: Timing, error recognition, overview, hand / eye coordination.";
        titleText.text = "LetterGame";
    }
    public void BaloonGame()
    {
        this.games = Games.BaloonGame;
        gameImage.sprite = images[0];
        gameText.text = "During this exercise, animals will float by, hanging on a balloon. The goal is for the client to sort the animals according to the correct landscape: land animals need to fall on the land, water animals  in the water. Some animals can live in both environments. An animal will start falling when the client punctures a balloon. The tweezers grip or pencil grip can both be trained with this exercise.\n \nCoordination, strength and reach can be trained through this exercise. The exercise mainly targets people with problems in fine motor skills.\n \nCognitive goals that can be practiced: Timing, making connections, hand / eye coordination, overview and possibly selective stimulus processing";
        titleText.text = "BalloonGame";
    }
    public void PackageGame()
    {
        this.games = Games.PackageGame;
        gameImage.sprite = images[4];
        gameText.text = "The goal of this exercise is for the client to use the correct stamp on the corresponding package. The grip that is used is the cylinder grip. \n \nThe exercise is created for people with strength and coordination problems, both in the wrist and in the hand.\n \nCognitive goals that can be trained with this exercise are: hand / eye coordination, timing, planning / sorting.";
        titleText.text = "PackageGame";
    }
    public void MazeGame()
    {
        this.games = Games.MazeBall;
        gameImage.sprite = images[2];
        gameText.text = "During this exercise, the goal is to reach the end of a maze and collect the treasure. To control the knight, the client must roll the ball-controller on the shield.\n \nThis exercise is developed for people with wrist problems.Ulnar and radial deviation is practiced and endurance and stability of the wrist are also trained.\n \nCognitive goals that can be practiced are timing, overview, making connections, problem - solving abilities.";
        titleText.text = "MazeGame";
    }
    public void DeliveryGame()
    {
        this.games = Games.DeliveryGame;
        gameImage.sprite = images[5];
        gameText.text = "During this exercise, the client must deliver as many meals as possible to the correct tables, during a certain (adjustable) time limit. The client practices the tweezer grip or the pencil grip during this exercise. By displaying results within time, the exercise is similar to the nine hole peg test.\n \nThis exercise can be used for clients that have problems with fine motor complaints, such as coordination and power reduction.\n \nCognitive goals that can be practiced are: hand / eye coordination, overview, timing, making connections.";
        titleText.text = "DeliveryGame";
    }
    public void MinerGame()
    {
        this.games = Games.MinerGame;
        gameImage.sprite = images[6];
        gameText.text = "";
        titleText.text = "MinerGame";
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    //when you click the start button that pops up once you select a game, the game starts up with a EXE file by process.start .
        public void StartButton()
    {
        switch(this.games)
        {
            case Games.None:
                break;
            case Games.LetterGame:
                Process.Start("..\\Letter\\Letter Game.exe");
                break;
            case Games.BaloonGame:
                Process.Start("..\\Baloon\\BalloonGame.exe");
                break;
            case Games.MazeBall:
                Process.Start("..\\Maze\\MazeBall.exe");
                break;
            case Games.OtterGame:
                Process.Start("..\\Otter\\Otter Game.exe");
                break;
            case Games.PackageGame:
                Process.Start("..\\Package\\PackageGame.exe");
                break;
            case Games.DeliveryGame:
                Process.Start("..\\Cook\\Cook Pin.exe");
                break;
            case Games.MinerGame:
                Process.Start("..\\Elevator\\MineElevator.exe");
                break;
        }
    }
}
