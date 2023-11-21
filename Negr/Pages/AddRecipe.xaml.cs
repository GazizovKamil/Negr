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
            // Сбор данных о рецепте
            string recipeName = txtRecipeName.Text;
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                MessageBox.Show("Введите название рецепта.");
                return;
            }

            if (!int.TryParse(txtPreparationTime.Text, out int preparationTime) || preparationTime <= 0)
            {
                MessageBox.Show("Введите корректное время приготовления.");
                return;
            }

            string instructions = txtInstructions.Text;
            if (string.IsNullOrWhiteSpace(instructions))
            {
                MessageBox.Show("Введите инструкции по приготовлению.");
                return;
            }

            // Создание объекта рецепта
            Recipes newRecipe = new Recipes
            {
                Name = recipeName,
                PreparationTime = preparationTime,
                Instructions = instructions,
                UserID = user.UserID, // Предполагается, что у вас есть объект user с его ID
                ImagePath = "",
            };

            // Добавление ингредиентов к рецепту
            foreach (Ingredients ingredient in lvIngredients.Items)
            {
                newRecipe.Ingredients.Add(ingredient);
            }

            try
            {
                Requests.Requests.SaveRecipeToDatabase(newRecipe);
                MessageBox.Show("Рецепт успешно сохранен.");
                this.NavigationService?.Navigate(new MainPage(user));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                string validationErrors = "";
                foreach (var validationError in dbEx.EntityValidationErrors)
                {
                    foreach (var error in validationError.ValidationErrors)
                    {
                        validationErrors += $"Property: {error.PropertyName} Error: {error.ErrorMessage}\n";
                    }
                }
                MessageBox.Show("Произошла ошибка при сохранении рецепта:\n" + validationErrors);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении рецепта: " + ex.Message);
            }
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredientName = txtIngredientName.Text;
            if (string.IsNullOrWhiteSpace(ingredientName))
            {
                MessageBox.Show("Пожалуйста, введите название ингредиента.");
                return;
            }

            if (!int.TryParse(txtIngredientQuantity.Text, out int ingredientQuantity) || ingredientQuantity <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество.");
                return;
            }

            Ingredients ingredient = new Ingredients
            {
                Name = ingredientName,
                Quantity = ingredientQuantity
            };

            lvIngredients.Items.Add(ingredient);

            // Обновление ListView
            lvIngredients.Items.Refresh();

            // Очистка полей ввода
            txtIngredientName.Clear();
            txtIngredientQuantity.Clear();
        }
    }
}
