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

namespace Negr.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Page
    {
        Users user;
        public AddRecipe(Users user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage(user));
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredientName = txtIngredientName.Text;
            if (string.IsNullOrWhiteSpace(ingredientName))
            {
                MessageBox.Show("Please enter the ingredient name.");
                return;
            }

            if (int.TryParse(txtIngredientQuantity.Text, out int ingredientQuantity))
            {
                // Create a new ingredient and add it to the current recipe
                Ingredients ingredient = new Ingredients
                {
                    Name = ingredientName,
                    Quantity = ingredientQuantity
                };

                Recipes currentRecipe = new Recipes();
                if (lvIngredients.DataContext != null)
                {
                    currentRecipe = (Recipes)lvIngredients.DataContext;
                }

                currentRecipe.Ingredients.Add(ingredient);

                // Update the ListView
                lvIngredients.DataContext = currentRecipe;
                lvIngredients.Items.Refresh();

                // Clear ingredient input fields
                txtIngredientName.Clear();
                txtIngredientQuantity.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }
    }
}
