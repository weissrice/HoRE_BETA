using System;
using System.Collections.Generic;

namespace HoRE_BETA
{
    public class Character
    {
        public int Id { get; set; }

        // Basic Info
        public string PlayerName { get; set; }
        public string FarmName { get; set; }
        public string Gender { get; set; }
        public short Age { get; set; }
        public string BodyType { get; set; }
        public string Pet { get; set; }

        // Appearance
        public string HairStyle { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string FacialHair { get; set; }

        // Clothing
        public string ShirtType { get; set; }
        public string ShirtColor { get; set; }
        public string PantsType { get; set; }
        public string PantsColor { get; set; }
        public string ShoesType { get; set; }
        public string ShoesColor { get; set; }
        public string Accessory { get; set; }
        public string Hat { get; set; }
        public string HatColor { get; set; }
        public short WoodChopping { get; set; } = 1;
        public short Fishing { get; set; } = 1;
        public short Farming { get; set; } = 1;
        public short Crafting { get; set; } = 1;
        public short Foraging { get; set; } = 1;
        public short Mining { get; set; } = 1;
        public short Combat { get; set; } = 1;

        public DateTime CreatedDate { get; set; }

        public void DisplayCharacter()
        {
            DisplayCharacter(true);
        }
        public void DisplayCharacter(bool showSkills)
        {
            Console.Clear();

            Console.WriteLine("===== CHARACTER PROFILE =====");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Player Name: {PlayerName}");
            Console.WriteLine($"Farm Name: {FarmName}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Body Type: {BodyType}");
            Console.WriteLine($"Preferred Pet: {Pet}");

            Console.WriteLine($"\n===== APPEARANCE =====");
            Console.WriteLine($"Hair: {HairStyle} ({HairColor})");
            if (!string.IsNullOrEmpty(FacialHair) && FacialHair != "None")
                Console.WriteLine($"Facial Hair: {FacialHair}");
            Console.WriteLine($"Eyes: {EyeColor}");

            Console.WriteLine($"\n===== CLOTHING =====");
            Console.WriteLine($"Shirt: {ShirtType} ({ShirtColor})");
            Console.WriteLine($"Pants: {PantsType} ({PantsColor})");
            Console.WriteLine($"Shoes: {ShoesType} ({ShoesColor})");
            Console.WriteLine($"Accessory: {Accessory}");
            if (Hat != "None")
                Console.WriteLine($"Hat: {Hat} ({HatColor})");

            // CONDITIONAL SKILLS SECTION
            if (showSkills)
            {
                Console.WriteLine($"\n===== SKILLS =====");
                Console.WriteLine($"Wood Chopping: {WoodChopping}");
                Console.WriteLine($"Fishing: {Fishing}");
                Console.WriteLine($"Farming: {Farming}");
                Console.WriteLine($"Crafting: {Crafting}");
                Console.WriteLine($"Foraging: {Foraging}");
                Console.WriteLine($"Mining: {Mining}");
                Console.WriteLine($"Combat: {Combat}");
            }

            Console.WriteLine($"\nCreated: {CreatedDate:yyyy-MM-dd HH:mm}");
            Console.WriteLine("=====================");
        }

        public void DisplayCharacter(string detailLevel)
        {
            if (detailLevel == "brief")
            {
                // Just show name and farm
                Console.WriteLine($"Character: {PlayerName} of {FarmName}");
            }
            else if (detailLevel == "skills-only")
            {
                // Just show skills
                Console.WriteLine($"===== {PlayerName}'s Skills =====");
                Console.WriteLine($"Farming: {Farming} | Combat: {Combat} | Mining: {Mining}");
                Console.Clear();
            }
            else
            {
                // Default to full display
                DisplayCharacter();
            }
        }
    }
}