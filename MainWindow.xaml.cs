using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Recipe_Application
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();
        private string userName;

        public MainWindow()
        {
            InitializeComponent();
            GreetUser();
        }

        private void GreetUser()
        {
            userName = Microsoft.VisualBasic.Interaction.InputBox("Hi there! What's your name?", "Enter Your Name");
            GreetingTextBlock.Text = $"Hello, {userName}! Here is Your Recipe";
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            int guests;
            if (!int.TryParse(GuestsTextBox.Text, out guests) || guests <= 0)
            {
                MessageBox.Show("Please enter a valid number of guests.");
                return;
            }

            Recipe recipe = new Recipe();
            recipe.Name = RecipeNameTextBox.Text;

            int ingredientCount;
            if (!int.TryParse(IngredientCountTextBox.Text, out ingredientCount) || ingredientCount <= 0)
            {
                MessageBox.Show("Please enter a valid number of ingredients.");
                return;
            }

            StringBuilder recipeDetails = new StringBuilder();
            recipeDetails.AppendLine($"Recipe for {recipe.Name}:");

            for (int i = 0; i < ingredientCount; i++)
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox($"Enter ingredient {i + 1} name:", "Enter Ingredient");
                double quantity;
                while (!double.TryParse(Microsoft.VisualBasic.Interaction.InputBox($"Enter quantity for {name}:", "Enter Quantity"), out quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.");
                }
                string unit = Microsoft.VisualBasic.Interaction.InputBox($"Enter unit for {name}:", "Enter Unit");
                double calories;
                while (!double.TryParse(Microsoft.VisualBasic.Interaction.InputBox($"Enter calories for {name}:", "Enter Calories"), out calories) || calories <= 0)
                {
                    MessageBox.Show("Please enter a valid number for calories.");
                }
                string foodGroup = Microsoft.VisualBasic.Interaction.InputBox($"Enter food group for {name}:", "Enter Food Group");

                recipe.Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup });

                // Append ingredient details to recipeDetails
                recipeDetails.AppendLine($"- {quantity} {unit} of {name} ({calories} calories, {foodGroup})");
            }

            int stepCount;
            if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox($"Enter the number of steps for {recipe.Name}:", "Enter Steps"), out stepCount) || stepCount <= 0)
            {
                MessageBox.Show("Please enter a valid number of steps.");
                return;
            }

            recipeDetails.AppendLine("\nSteps:");
            for (int i = 0; i < stepCount; i++)
            {
                string description = Microsoft.VisualBasic.Interaction.InputBox($"Enter description for step {i + 1}:", "Enter Step Description");
                recipe.Steps.Add(new RecipeStep { Description = description });

                // Append step description to recipeDetails
                recipeDetails.AppendLine($"{i + 1}. {description}");
            }

            recipes.Add(recipe);

            // Display the recipe details immediately
            RecipeDetailsTextBlock.Text += recipeDetails.ToString();

            // Clear input fields
            GuestsTextBox.Clear();
            RecipeNameTextBox.Clear();
            IngredientCountTextBox.Clear();

            MessageBoxResult result = MessageBox.Show("Do you want to enter another recipe?", "Another Recipe", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                // Optionally, display a message indicating the end of recipe input
                MessageBox.Show("Recipe entry complete.");
            }
        }
    }

    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }
    }

    class RecipeStep
    {
        public string Description { get; set; }
    }
}
