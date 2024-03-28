using Microsoft.Maui.Controls;
using i_Skör.ViewModels;
using i_Skör.Models;
using System;
using System.Linq;
using i_Skör.Services;


namespace i_Skör.Views;

public partial class ClassementView : ContentPage
{
	public ClassementView()
	{
		InitializeComponent();
        BindingContext = new ClassementViewModel();
    }
}