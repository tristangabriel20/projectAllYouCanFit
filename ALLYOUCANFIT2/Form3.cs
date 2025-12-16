using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ALLYOUCANFIT2
{
    public partial class Form3 : Form
    {
        private string goal;
        private double targetCalories;
        private double targetProtein;
        private double targetCarbs;

        private Food selectedFood;
        private List<Food> menu;

        public Form3(string goal, double calories, double protein, double carbs)
        {
            InitializeComponent();
            this.goal = goal.ToLower();
            targetCalories = calories;
            targetProtein = protein;
            targetCarbs = carbs;
        }

        // ================= FOOD CLASS =================
        public class Food
        {
            public string Name { get; set; }
            public double Calories { get; set; }
            public double Protein { get; set; }
            public double Carbs { get; set; }
            public double Price { get; set; }

            public double Score(double c, double p, double cb)
            {
                return Math.Abs(Calories - c)
                     + Math.Abs(Protein - p)
                     + Math.Abs(Carbs - cb);
            }

            public override string ToString()
            {
                return $"{Name} | {Calories} kcal | {Protein}g P | {Carbs}g C | ₱{Price}";
            }
        }

        // ================= MENU =================
        private List<Food> GetMenu()
        {
            if (goal == "lose")
            {
                return new List<Food>
                {
                    new Food { Name="Grilled Salmon & Asparagus", Calories=400, Protein=40, Carbs=20, Price=350 },
                    new Food { Name="Lean Turkey Chili", Calories=350, Protein=30, Carbs=25, Price=300 },
                    new Food { Name="Chicken Veggie Stir-fry", Calories=330, Protein=40, Carbs=30, Price=380 },
                    new Food { Name="Greek Yogurt Parfait", Calories=300, Protein=25, Carbs=35, Price=250 },
                    new Food { Name="Egg White Scramble", Calories=220, Protein=25, Carbs=10, Price=120 },
                    new Food { Name="Sinigang na Bangus", Calories=300, Protein=35, Carbs=20, Price=150 },
                    new Food { Name="Tofu and Eggplant Bistek", Calories=400, Protein=30, Carbs=30, Price=170 },
                    new Food { Name="Chicken Tinola", Calories=250, Protein=30, Carbs=50, Price=130 },
                    new Food { Name="Spinach & Calamansi Tuna Salad", Calories=330, Protein=35, Carbs=15, Price=140 },
                    new Food { Name=" Low-Carb-Adobo Bowl", Calories=430, Protein=40, Carbs=20, Price=160 }
                };
            }

            if (goal == "maintain")
            {
                return new List<Food>
                {
                    new Food { Name="Quinoa Chicken Bowl", Calories=530, Protein=50, Carbs=45, Price=250 },
                    new Food { Name="Turkey Sandwich", Calories=500, Protein=40, Carbs=50, Price=300 },
                    new Food { Name="Lentil Soup & Bread", Calories=450, Protein=30, Carbs=55, Price=190 },
                    new Food { Name="Salmon Salad", Calories=550, Protein=40, Carbs=60, Price=250 },
                    new Food { Name="Protein Oatmeal", Calories=500, Protein=35, Carbs=65, Price=150 },
                    new Food { Name="Ginisang Monggo (Mung Bean Stew)", Calories=550, Protein=30, Carbs=80, Price=160 },
                    new Food { Name="Beef Bistek with Brown Rice", Calories=530, Protein=40, Carbs=60, Price=150 },
                    new Food { Name="Chicken Pancit Bihon", Calories=500, Protein=35, Carbs=65, Price=170 },
                    new Food { Name="Tapsilog (Healthy Version)", Calories=550, Protein=40, Carbs=65, Price=100 },
                    new Food { Name="Chicken Afritada (Reduced Fat)", Calories=500, Protein=35, Carbs=65, Price=190 }
                };
            }

            // GAIN
            return new List<Food>
            {
                new Food { Name="Power Oatmeal", Calories=750, Protein=45, Carbs=90, Price=200 },
                new Food { Name="Chicken Rice Bowl", Calories=680, Protein=50, Carbs=85, Price=180 },
                new Food { Name="Mass Gainer Shake", Calories=800, Protein=50, Carbs=100, Price=120 },
                new Food { Name="Beef Sweet Potato", Calories=650, Protein=45, Carbs=70, Price=350 },
                new Food { Name="Salmon Pasta Alfredo", Calories=750, Protein=50, Carbs=80, Price=350 },
                new Food { Name="Loaded Silog Bowl", Calories=750, Protein=40, Carbs=90, Price=250 },
                new Food { Name="Protein Ube Smoothie", Calories=850, Protein=50, Carbs=100, Price=150 },
                new Food { Name="Chicken Curry with Coconut Cream", Calories=700, Protein=45, Carbs=80, Price=300 },
                new Food { Name="Beef Kaldereta with Extra Carbs", Calories=750, Protein=50, Carbs=90, Price=350 },
                new Food { Name="High-Protein Pandesal Sandwich", Calories=680, Protein=40, Carbs=80, Price=150 }
            };
        }

        // ================= LOAD & SORT =================
        private void LoadRecommendations()
        {
            menu = GetMenu();

            var rankedMeals = menu
                .OrderBy(f => f.Score(targetCalories, targetProtein, targetCarbs))
                .ToList();

            listBox1.Items.Clear();
            foreach (Food f in rankedMeals)
                listBox1.Items.Add(f);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadRecommendations();
        }

        // ================= CHECKOUT =================
        private void btn_proceed3_Click(object sender, EventArgs e)
        {
            Food selectedFood = listBox1.SelectedItem as Food;

            if (selectedFood == null)
            {
                MessageBox.Show("Please select a meal first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "You must be logged in to continue.\nDo you want to login first?",
                "Checkout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Form4 login = new Form4();
                login.Show();
                this.Hide();
            }
            else
            {
                paymentForm pay = new paymentForm(
                    selectedFood.Name,
                    selectedFood.Calories,
                    selectedFood.Protein,
                    selectedFood.Price
                );

                pay.Show();
                this.Hide();
            }
        }
    }
}