using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solvle.Application.SolvleUI;
/// <summary>
/// Interaction logic for SolvleView.xaml
/// </summary>
public partial class SolvleView : UserControl
{
    private SolvleViewModel ViewModel => (SolvleViewModel)this.DataContext;

    public SolvleView()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.MakeGuess();

        //clear text boxes when solvle clicked
        GuessLetterOne.Clear();
        GuessLetterTwo.Clear();
        GuessLetterThree.Clear();
        GuessLetterFour.Clear();    
        GuessLetterFive.Clear();



        GuessLetterOne.Focus();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        //undo
        ViewModel.Undo();
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        //reset
        ViewModel.Reset();

        GuessLetterOne.Clear();
        GuessLetterTwo.Clear();
        GuessLetterThree.Clear();
        GuessLetterFour.Clear();
        GuessLetterFive.Clear();

        GuessLetterOne.Focus();

        ViewModel.GuessLetterOneColour = ViewModel.Colours[2];
        ViewModel.GuessLetterTwoColour = ViewModel.Colours[2];
        ViewModel.GuessLetterThreeColour = ViewModel.Colours[2];
        ViewModel.GuessLetterFourColour = ViewModel.Colours[2];
        ViewModel.GuessLetterFiveColour = ViewModel.Colours[2];
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterOneColour));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterTwoColour));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterThreeColour));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterFourColour));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterFiveColour));

    }

    private void TextBox_KeyUp(object sender, KeyEventArgs e)
    {

        if (e.Key == Key.Delete || e.Key == Key.Back)
        {
            return;
        }
        //move on to next text box when char entered
        if (sender == GuessLetterOne)
        {
            GuessLetterTwo.Focus();
        }

        if (sender == GuessLetterTwo)
        {
            GuessLetterThree.Focus();
        }
        
        if (sender == GuessLetterThree)
        {
            GuessLetterFour.Focus();
        }

        if (sender == GuessLetterFour)
        {
            GuessLetterFive.Focus();
        }

        if (sender == GuessLetterFive) 
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, null);

            }
        }
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        var buttonClicked = sender as Button;
        var myWord = buttonClicked.DataContext as string;
        ViewModel.GuessLetterOne = myWord[0].ToString();
        ViewModel.GuessLetterTwo = myWord[1].ToString();
        ViewModel.GuessLetterThree = myWord[2].ToString();
        ViewModel.GuessLetterFour = myWord[3].ToString();
        ViewModel.GuessLetterFive = myWord[4].ToString();


        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterOne));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterTwo));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterThree));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterFour));
        ViewModel.NotifyOfPropertyChange(nameof(ViewModel.GuessLetterFive));


    }
}
