﻿using System;
using System.Collections.Generic;
using System.IO;
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
using Negr.ClassApp;
using Negr.Windows;
using QRCoder;
using System.Drawing;
using Negr.Pages;

namespace Negr.PageApp
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Users user;
        public List<Recipes> Recipes { get; set; }
        public List<Recipes> FilteredRecipes { get; set; }
        public MainPage(Users user)
        {
            InitializeComponent();
            this.user = user;

            Recipes = DBConection.dbContext.Recipes.Where(x=> x.UserID == this.user.UserID).ToList();

            foreach (var recipe in Recipes)
            {
                // Генерация QR-кода с адресом на сервер (замените "ServerAddress" на реальный адрес сервера)
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode("http://192.168.137.1:5001/recipe.html?recipeId=1", QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20);

                BitmapImage qrCodeAsImage = BitmapToImageSource(qrCodeAsBitmap);

                recipe.Image = qrCodeAsImage;
            }

            RecipeListView.DataContext = this;
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void ViewRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия на кнопку "Подробнее"
            Button button = (Button)sender;
            Recipes selectedRecipe = (Recipes)button.DataContext;

            // Открытие окна просмотра информации о рецепте и передача данных
            this.NavigationService.Navigate(new RecipeDetails(selectedRecipe, user));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower(); // Получаем текст поиска и приводим к нижнему регистру

            // Фильтруем рецепты, оставляем только те, которые содержат в названии введенный текст
            FilteredRecipes = Recipes.Where(recipe => recipe.Name.ToLower().Contains(searchText)).ToList();

            // Обновляем привязку данных ListView
            RecipeListView.ItemsSource = null;
            RecipeListView.ItemsSource = FilteredRecipes;
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Откройте окно для добавления нового рецепта
            this.NavigationService.Navigate(new AddRecipe(user));
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListView.SelectedItem != null)
            {
                // Получите выбранный рецепт
                Recipes selectedRecipe = RecipeListView.SelectedItem as Recipes;

                // Удалите связанные ингредиенты
                foreach (var ingredient in selectedRecipe.Ingredients.ToList())
                {
                    DBConection.dbContext.Ingredients.Remove(ingredient);
                }

                // Удалите рецепт из списка Recipes
                Recipes.Remove(selectedRecipe);

                // Затем удалите рецепт из таблицы Recipes
                DBConection.dbContext.Recipes.Remove(selectedRecipe);

                // Сохраните изменения
                DBConection.dbContext.SaveChanges();

                RecipeListView.Items.Refresh();
            }
        }
    }
}
