using i_Skör.Views;

namespace i_Skör
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAjouterEquipeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AjoutEquipeView());
        }

        private async void OnAjouterJoueurClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AjoutJoueurView());
        }

        private async void OnAjouterPartieClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AjoutPartieView());
        }

        private async void OnVoirClassementClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClassementView());
        }
    }
}