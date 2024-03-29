﻿using Negr.ClassApp;
using Negr.PageApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Negr.Windows
{
    /// <summary>
    /// Логика взаимодействия для RecipeDetailsWindow.xaml
    /// </summary>
    public partial class RecipeDetails : Page
    {
        Users user;
        public RecipeDetails(Recipes recipe, Users user)
        {
            InitializeComponent();
            DataContext = recipe;
            this.user = user;

            foreach (var ingredient in recipe.Ingredients.ToList())
            {
                lvIngredients.Items.Add(ingredient);
            }
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage(user));
        }
    }
}
