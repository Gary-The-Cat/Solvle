using DynamicData;
using Impulse.SharedFramework.Services;
using Impulse.SharedFramework.Services.Layout;
using Solvle.Domain;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Solvle.Application.SolvleUI;
public class SolvleViewModel : DocumentBase
{
    private Solver solver;

    private Dictionary<SolidColorBrush, Feedback> feedbackColour;

    private IDialogService dialogService;

    public SolvleViewModel(IDialogService dialogService)
    {
        DisplayName = "Wordle Solver";

        this.dialogService = dialogService;

        Colours = new List<SolidColorBrush>()
        {
            new SolidColorBrush(Color.FromRgb(0x75, 0xB3, 0x6E)),
            new SolidColorBrush(Color.FromRgb(0xCF, 0xBD, 0x61)),
            new SolidColorBrush(Color.FromRgb(0x83, 0x87, 0x89)),
        };

        feedbackColour = new Dictionary<SolidColorBrush, Feedback>()
        {
            { Colours[0], Feedback.RightLetterRightPlace },
            { Colours[1], Feedback.RightLetterWrongPlace },
            { Colours[2], Feedback.WrongLetter },
        };

        solver = new Solver(WordleRepo.Words);

        GuessLetterOneColour = Colours.Last();
        GuessLetterTwoColour = Colours.Last();
        GuessLetterThreeColour = Colours.Last();
        GuessLetterFourColour = Colours.Last();
        GuessLetterFiveColour = Colours.Last();


        // Pass them into our observableCollection
        PotentialWords = new ObservableCollection<string>();

        GuessList = new ObservableCollection<string>();
    }

    public List<SolidColorBrush> Colours { get; set; }
    
    public SolidColorBrush GuessLetterOneColour { get; set; } 
    public SolidColorBrush GuessLetterTwoColour { get; set; }
    public SolidColorBrush GuessLetterThreeColour { get; set; }
    public SolidColorBrush GuessLetterFourColour { get; set; }
    public SolidColorBrush GuessLetterFiveColour { get; set; }

    public string GuessLetterOne { get; set; }
    public string GuessLetterTwo { get; set; }
    public string GuessLetterThree { get; set; }
    public string GuessLetterFour { get; set; }
    public string GuessLetterFive { get; set; }

    public ObservableCollection <string> PotentialWords { get; set; }

    public ObservableCollection<string> GuessList { get; set; }

    public void Undo() 
    {
        // Undo the last guess
        solver.UndoLastGuess();

        //rerun refining the list with the shorter guess list
        //reprint refined list
        PotentialWords.Clear();
        PotentialWords.AddRange(solver.GetRefinedList());


        //reprint guessList
        if (GuessList.Any())
        {
            GuessList.Remove(GuessList.Last());
        }

    }
    public void Reset()
    {
        //Clear all guesses
        solver.ClearAllGuesses();

        //Clear potential words
        PotentialWords.Clear();
        

        //Clear guessList
        GuessList.Clear();


       
        
    }

    public void MakeGuess()
    {
        if (string.IsNullOrWhiteSpace(GuessLetterOne) ||
            string.IsNullOrWhiteSpace(GuessLetterTwo) ||
            string.IsNullOrWhiteSpace(GuessLetterThree) ||
            string.IsNullOrWhiteSpace(GuessLetterFour) ||
            string.IsNullOrWhiteSpace(GuessLetterFive))
        {
            dialogService.ShowError(
                "Warning",
                "Your guess needs 5 letters.");

            return;
        }

        // Get the letters for each of the guesses
        var letterOneChar = GuessLetterOne[0];
        var letterTwoChar = GuessLetterTwo[0];
        var letterThreeChar = GuessLetterThree[0];
        var letterFourChar = GuessLetterFour[0];
        var letterFiveChar = GuessLetterFive[0];

        var letterOne = new Letter(letterOneChar);
        var letterTwo = new Letter(letterTwoChar);
        var letterThree = new Letter(letterThreeChar);
        var letterFour = new Letter(letterFourChar);
        var letterFive = new Letter(letterFiveChar);

        // Create the 'LetterPosition's for each of our letters
        var letterOnePosition = new LetterPosition(0);
        var letterTwoPosition = new LetterPosition(1);
        var letterThreePosition = new LetterPosition(2);
        var letterFourPosition = new LetterPosition(3);
        var letterFivePosition = new LetterPosition(4);
       

        // Get the feedback for each of the guesses:
        var letterOneFeedback = feedbackColour[GuessLetterOneColour];
        var letterTwoFeedback = feedbackColour[GuessLetterTwoColour];
        var letterThreeFeedback = feedbackColour[GuessLetterThreeColour];
        var letterFourFeedback = feedbackColour[GuessLetterFourColour];
        var letterFiveFeedback = feedbackColour[GuessLetterFiveColour];

        // Make the guess letters

        var guessLetterOne = new GuessLetter(letterOne, letterOneFeedback, letterOnePosition);
        var guessLetterTwo = new GuessLetter(letterTwo, letterTwoFeedback, letterTwoPosition);
        var guessLetterThree = new GuessLetter(letterThree, letterThreeFeedback, letterThreePosition);
        var guessLetterFour = new GuessLetter(letterFour, letterFourFeedback, letterFourPosition);
        var guessLetterFive = new GuessLetter(letterFive, letterFiveFeedback, letterFivePosition);

        // Make the guess
        var guess = new Guess(guessLetterOne, guessLetterTwo, guessLetterThree, guessLetterFour, guessLetterFive);

        // Perform the guess
        solver.MakeGuess(guess);

        // Ask for the new list of refined words
        var potentialWords = solver.GetRefinedList();

        // Update our UI to show the list of refined words.
        PotentialWords.Clear();
        PotentialWords.AddRange(potentialWords);

        //update UI to show new guess in list

        GuessList.Add(guess.GetString());
    }
}
