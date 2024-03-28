using i_Skör.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace i_Skör.Views
{
    public partial class AjoutPartieView : ContentPage
    {
        public AjoutPartieView()
        {
            InitializeComponent();
            BindingContext = new PartieViewModel();
        }
    }
}
