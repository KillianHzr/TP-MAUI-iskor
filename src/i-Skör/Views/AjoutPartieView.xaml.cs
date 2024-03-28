using Microsoft.Maui.Controls;
using i_Skör.ViewModels;
using i_Skör.Models;
using System;
using System.Linq;

namespace i_Skör.Views
{
    public partial class AjoutPartieView : ContentPage
    {
        PartieViewModel viewModel;

        public AjoutPartieView()
        {
            InitializeComponent();
            viewModel = new PartieViewModel();
            this.BindingContext = viewModel;
        }

        private async void OnAjouterPartieClicked(object sender, EventArgs e)
        {
            DateTime date = DatePartiePicker.Date;
            Equipe equipeA = EquipeAPicker.SelectedItem as Equipe;
            int scoreA;
            if (!int.TryParse(ScoreAPartieEntry.Text, out scoreA))
            {
                await DisplayAlert("Erreur", "Le score de l'équipe A doit être un nombre entier.", "OK");
                return;
            }
            Equipe equipeB = EquipeBPicker.SelectedItem as Equipe;
            int scoreB;
            if (!int.TryParse(ScoreBPartieEntry.Text, out scoreB))
            {
                await DisplayAlert("Erreur", "Le score de l'équipe B doit être un nombre entier.", "OK");
                return;
            }

            if (equipeA == null || equipeB == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner deux équipes différentes.", "OK");
                return;
            }

            if (equipeA == equipeB)
            {
                await DisplayAlert("Erreur", "Les deux équipes sélectionnées sont identiques.", "OK");
                return;
            }

            var (success, message) = viewModel.AjouterPartie(date, equipeA, scoreA, equipeB, scoreB);

            if (success)
            {
                DatePartiePicker.Date = DateTime.Now;
                EquipeAPicker.SelectedItem = null;
                ScoreAPartieEntry.Text = string.Empty;
                EquipeBPicker.SelectedItem = null;
                ScoreBPartieEntry.Text = string.Empty;
            }

            await DisplayAlert(success ? "Succès" : "Erreur", message, "OK");
        }

        private async void OnSupprimerPartieClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Partie partie)
            {
                bool confirmDelete = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette partie ?", "Oui", "Non");
                if (confirmDelete)
                {
                    viewModel.SupprimerPartie(partie);
                }
            }
        }

        private async void OnModifierPartieClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Partie partie)
            {
                DateTime nouvelleDate = partie.Date;
                string nouvelleDateString = partie.Date.ToString("dd/MM/yyyy");
                Equipe nouvelleEquipeA = partie.EquipeA;
                int nouveauScoreA = partie.ScoreA;
                Equipe nouvelleEquipeB = partie.EquipeB;
                int nouveauScoreB = partie.ScoreB;

                // Sélectionner une nouvelle date
                nouvelleDateString = await DisplayPromptAsync("Modifier Partie", "Entrez la nouvelle date (format jj/mm/aaaa) :", initialValue: nouvelleDateString);
                if (!DateTime.TryParseExact(nouvelleDateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out nouvelleDate))
                {
                    await DisplayAlert("Erreur", "La date entrée n'est pas valide.", "OK");
                    return;
                }

                // Sélectionner une nouvelle équipe A
                var equipeNoms = viewModel.Equipes.Select(eq => eq.Nom).ToArray();
                var equipeSelectionneeNom = nouvelleEquipeA?.Nom ?? ""; // Nom de l'équipe actuelle
                var indexSelectionne = Array.IndexOf(equipeNoms, equipeSelectionneeNom);
                var equipeSelectionneeNouveau = await DisplayActionSheet("Sélectionner une équipe A", "Annuler", null, equipeNoms);

                if (equipeSelectionneeNouveau == "Annuler")
                    return;

                nouvelleEquipeA = viewModel.Equipes.FirstOrDefault(eq => eq.Nom == equipeSelectionneeNouveau);

                if (nouvelleEquipeA == null)
                {
                    await DisplayAlert("Erreur", "Veuillez sélectionner une équipe A valide.", "OK");
                    return;
                }

                // Sélectionner un nouveau score pour l'équipe A
                string nouveauScoreAString = await DisplayPromptAsync("Modifier Partie", "Entrez le nouveau score de l'équipe A :", initialValue: partie.ScoreA.ToString());
                if (!int.TryParse(nouveauScoreAString, out nouveauScoreA))
                {
                    await DisplayAlert("Erreur", "Le score entré n'est pas valide.", "OK");
                    return;
                }

                // Sélectionner une nouvelle équipe B
                equipeSelectionneeNom = nouvelleEquipeB?.Nom ?? ""; // Nom de l'équipe actuelle
                indexSelectionne = Array.IndexOf(equipeNoms, equipeSelectionneeNom);
                equipeSelectionneeNouveau = await DisplayActionSheet("Sélectionner une équipe B", "Annuler", null, equipeNoms);

                if (equipeSelectionneeNouveau == "Annuler")
                    return;

                nouvelleEquipeB = viewModel.Equipes.FirstOrDefault(eq => eq.Nom == equipeSelectionneeNouveau);

                if (nouvelleEquipeB == null)
                {
                    await DisplayAlert("Erreur", "Veuillez sélectionner une équipe B valide.", "OK");
                    return;
                }

                // Sélectionner un nouveau score pour l'équipe B
                string nouveauScoreBString = await DisplayPromptAsync("Modifier Partie", "Entrez le nouveau score de l'équipe B :", initialValue: partie.ScoreB.ToString());
                if (!int.TryParse(nouveauScoreBString, out nouveauScoreB))
                {
                    await DisplayAlert("Erreur", "Le score entré n'est pas valide.", "OK");
                    return;
                }

                viewModel.ModifierPartie(partie, nouvelleDate, nouvelleEquipeA, nouveauScoreA, nouvelleEquipeB, nouveauScoreB);
            }
        }

    }
}
