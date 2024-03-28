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

        // TODO: Implementer les méthodes OnModifierPartieClicked et OnSupprimerPartieClicked
    }
}
