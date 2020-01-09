using System;


namespace DojoDachi.Models {
    public class Dachi {
        public int happiness;
        public int fullness;
        public int energy;
        public int meals;
        Random r = new Random();
        public Dachi() {
            happiness = 20;
            fullness = 20;
            energy = 50;
            meals = 3;
        }
        public string Feed() {
            int TwentyFive = r.Next(1,5);
            if(meals > 0) {
                meals -= 1;
                if(r.Next(1,5) != TwentyFive) {
                    int more = r.Next(5, 11);
                    fullness += more;
                    return "You just fed your DojoDachi. ";
                } else {
                    return "You didn't get full.. -1 meal.";
                }
            } else {
                return "Not enough meals!";
            }
        }
        public string Play() {
            int TwentyFive = r.Next(1,5);
            energy -= 5;
            if(r.Next(1,5) != TwentyFive) {
                int hap = r.Next(5, 11);
                happiness += hap;
                return $"You just played! +{hap} happiness";
            } else {
                return "Whoops! You got unlucky! -5 energy.";
            }
        }
        public string Work() {
            energy -= 5;
            int mealCount = r.Next(1, 4);
            meals += mealCount;
            return $"Nice, what a shift. -5 Energy, +{mealCount} meals.";
        }
        public string Sleep() {
            energy += 15;
            fullness += 5;
            happiness += 5;
            return $"Good nap. Energy, fullness, and happiness gained!(15,5,5)";
        }
        
    }
}