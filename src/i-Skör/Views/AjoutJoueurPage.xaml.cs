using Microsoft.Maui.Controls;
using i_Skör.ViewModels;
using i_Skör.Models;
using System;


namespace i_Skör.Views
{
    public partial class AjoutJoueurView : ContentPage
    {
        JoueurViewModel viewModel;

        public AjoutJoueurView()
        {
            InitializeComponent();
            viewModel = new JoueurViewModel();
            this.BindingContext = viewModel;
        }

        private void OnAjouterJoueurClicked(object sender, EventArgs e)
        {
            string nom = NomJoueurEntry.Text;
            string pseudo = PseudoJoueurEntry.Text;

            viewModel.AjouterJoueur(nom, pseudo);

            // Réinitialiser les champs après l'ajout
            NomJoueurEntry.Text = string.Empty;
            PseudoJoueurEntry.Text = string.Empty;
        }
    }
}
