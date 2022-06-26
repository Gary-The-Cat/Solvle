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

    }


}
