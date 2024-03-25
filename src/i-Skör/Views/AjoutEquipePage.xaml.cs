using i_Skör.Models;
using i_Skör.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace i_Skör.Views
{
    public partial class AjoutEquipeView : ContentPage
    {
        private EquipeViewModel viewModel;

        public AjoutEquipeView()
        {
            InitializeComponent();
            viewModel = new EquipeViewModel(new Equipe());
            this.BindingContext = viewModel;
        }

        private void OnCreerEquipeClicked(object sender, EventArgs e)
        {
            var nomEquipe = NomEquipeEntry.Text;

            viewModel.CreerNouvelleEquipe(nomEquipe);

            NomEquipeEntry.Text = string.Empty;
        }
    }
}
