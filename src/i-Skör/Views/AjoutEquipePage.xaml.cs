    using i_Skör.Models;
    using i_Skör.ViewModels;
    using Microsoft.Maui.Controls;
    using System;

    namespace i_Skör.Views
    {
        public partial class AjoutEquipeView : ContentPage
        {
            private EquipeViewModel viewModel;
            public CollectionView EquipesCollectionView => EquipesCollection;

            public AjoutEquipeView()
            {
                InitializeComponent();
                viewModel = new EquipeViewModel(new Equipe());
                this.BindingContext = viewModel;
            }

        private void OnCreerEquipeClicked(object sender, EventArgs e)
        {
            var nomEquipe = NomEquipeEntry.Text;

            if (!string.IsNullOrWhiteSpace(nomEquipe))
            {
                viewModel.CreerNouvelleEquipe(nomEquipe);
                NomEquipeEntry.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Erreur", "Veuillez entrer un nom pour l'équipe.", "OK");
            }
        }

        private async void OnSupprimerEquipeClicked(object sender, EventArgs e)
            {
                if (sender is Button button && button.BindingContext is Equipe equipe)
                {
                    bool confirmDelete = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette équipe ?", "Oui", "Non");
                    if (confirmDelete)
                    {
                        viewModel.SupprimerEquipe(equipe);
                    }
                }
            }

        private async void OnModifierEquipeClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Equipe equipe)
            {
                string nouveauNom = await DisplayPromptAsync("Modifier Equipe", "Entrez le nouveau nom :", initialValue: equipe.Nom);

                if (!string.IsNullOrWhiteSpace(nouveauNom))
                {
                    viewModel.ModifierNomEquipe((equipe, nouveauNom));
                    viewModel.NomEquipe = nouveauNom; 
                }
            }
        }
    }
}
