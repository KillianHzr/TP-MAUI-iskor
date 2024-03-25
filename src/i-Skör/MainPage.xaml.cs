using i_Skör.Views;

namespace i_Skör
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnAjouterEquipeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AjoutEquipeView());
        }

        private async void OnAjouterJoueurClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AjoutJoueurView());
        }
    }
}