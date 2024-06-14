using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Prog6221Part3Poe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int clickedtimes;
        private string name =" ";
        private List<Recipe> recipes;
        private List<Recipe> filteredRecipes;
        private List<FoodGroup> foodGroups;

        public object Calories { get; private set; }
        public object FoodGroup { get; private set; }
        public object Ingredient { get; private set; }
        public object RecipesListBox { get; private set; }
        public object MainTextBox { get; private set; }
        public object FoodGroupChart { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            clickedtimes = 0;
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            name = MainTextBox.Text;
            NameLabel.Content = $", {name.ToString()}";
        }

        private void LoadData()
        {
            // Load recipes and food groups here
            // Example data
            recipes = new List<Recipe>
            {
                new Recipe { Name = "Pasta", Ingredients = new List<string> { "Noodles", "Tomato" }, FoodGroup = "Carbs", Calories = 400 },
                new Recipe { Name = "Salad", Ingredients = new List<string> { "Lettuce", "Tomato" }, FoodGroup = "Vegetables", Calories = 150 }
            };

            foodGroups = new List<FoodGroup>
            {
                new FoodGroup { Name = "Carbs" },
                new FoodGroup { Name = "Vegetables" }
            };

            FoodGroup.ItemsSource = foodGroups.Select(fg => fg.Name);
        }

        private void OnFilterRecipes(object sender, RoutedEventArgs e)
        {
            string ingredient = Ingredient.ToLower();
            string selectedFoodGroup = FoodGroup.SelectedItem?.ToString();
            bool isCaloriesValid = int.TryParse(Calories. ,out Calories maxCalories);

            filteredRecipes = recipes
                .Where(r => (string.IsNullOrEmpty(ingredient) || r.Ingredients.Any(i => i.ToLower().Contains(ingredient)))
                            && (string.IsNullOrEmpty(selectedFoodGroup) || r.FoodGroup == selectedFoodGroup)
                            && (!isCaloriesValid || r.Calories <= maxCalories))
                .ToList();

            RecipesListBox.ItemsSource = filteredRecipes;
        }

        private void OnAddToMenu(object sender, RoutedEventArgs e)
        {
            var selectedRecipes = RecipesListBox.SelectedItems.Cast<Recipe>().ToList();
            var foodGroupCounts = selectedRecipes
                .GroupBy(r => r.FoodGroup)
                .Select(g => new { FoodGroup = g.Key, Count = g.Count() })
                .ToList();

            ((PieSeries)FoodGroupChart.Series[0]).ItemsSource = foodGroupCounts;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
