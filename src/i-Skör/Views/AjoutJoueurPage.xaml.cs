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

        private async void OnAjouterJoueurClicked(object sender, EventArgs e)
        {
            string nom = NomJoueurEntry.Text;
            string pseudo = PseudoJoueurEntry.Text;
            Equipe equipeSelectionnee = EquipePicker.SelectedItem as Equipe;

            if (equipeSelectionnee == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner une équipe.", "OK");
                return;
            }

            var (success, message) = viewModel.AjouterJoueur(nom, pseudo, equipeSelectionnee);

            if (success)
            {
                NomJoueurEntry.Text = string.Empty;
                PseudoJoueurEntry.Text = string.Empty;
                EquipePicker.SelectedItem = null;
            }

            await DisplayAlert(success ? "Succès" : "Erreur", message, "OK");
        }


        private async void OnModifierJoueurClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Joueur joueur)
            {
                string nouveauNom = await DisplayPromptAsync("Modifier Joueur", "Entrez le nouveau nom :", initialValue: joueur.Nom);
                string nouveauPseudo = await DisplayPromptAsync("Modifier Joueur", "Entrez le nouveau pseudo :", initialValue: joueur.Pseudo);
                Equipe equipeSelectionnee = EquipePicker.SelectedItem as Equipe;

                if (equipeSelectionnee == null)
                {
                    await DisplayAlert("Erreur", "Veuillez sélectionner une équipe.", "OK");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(nouveauNom) && !string.IsNullOrWhiteSpace(nouveauPseudo))
                {
                    viewModel.ModifierJoueur(joueur, nouveauNom, nouveauPseudo, equipeSelectionnee);
                }
            }
        }


        private async void OnSupprimerJoueurClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Joueur joueur)
            {
                bool confirmDelete = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer ce joueur ?", "Oui", "Non");
                if (confirmDelete)
                {
                    viewModel.SupprimerJoueur(joueur);
                }
            }
        }


    }
}
