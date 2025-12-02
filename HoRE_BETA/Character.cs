using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoRE_BETA
{
    public class Character
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string FarmName { get; set; }
        public string Gender { get; set; }
        public string HairStyle { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string ShirtType { get; set; }
        public string ShirtColor { get; set; }
        public string PantsType { get; set; }
        public string PantsColor { get; set; }
        public string ShoesType { get; set; }
        public string ShoesColor { get; set; }
        public string Accessory { get; set; }
        public int WoodChopping { get; set; }
        public int Fishing { get; set; }
        public int Harvesting { get; set; }
        public int Crafting { get; set; }
        public int Foraging { get; set; }
        public int Mining { get; set; }
        public int Combat { get; set; }
        public DateTime CreatedDate { get; set; }

        public void DisplayCharacter()
        {
            Console.WriteLine("===== CHARACTER PROFILE =====");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Player Name: {PlayerName}");
            Console.WriteLine($"Farm Name: {FarmName}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Appearance: {HairStyle} {HairColor} hair, {EyeColor} eyes");
            Console.WriteLine($"Clothing: {ShirtColor} {ShirtType}, {PantsColor} {PantsType}");
            Console.WriteLine($"Footwear: {ShoesColor} {ShoesType}");
            Console.WriteLine($"Accessory: {Accessory}");
            Console.WriteLine("\n===== SKILLS =====");
            Console.WriteLine($"Wood Chopping: {WoodChopping}");
            Console.WriteLine($"Fishing: {Fishing}");
            Console.WriteLine($"Harvesting: {Harvesting}");
            Console.WriteLine($"Crafting: {Crafting}");
            Console.WriteLine($"Foraging: {Foraging}");
            Console.WriteLine($"Mining: {Mining}");
            Console.WriteLine($"Combat: {Combat}");
            Console.WriteLine($"Created: {CreatedDate:yyyy-MM-dd HH:mm}");
            Console.WriteLine("=====================");
        }

        private static readonly List<Character> characters = new();
    }
}
