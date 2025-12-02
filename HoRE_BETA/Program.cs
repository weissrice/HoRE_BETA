using System;
using System.Collections.Generic;
using System.Threading;
using HoRE_BETA;

namespace HoRE_BETA
{
    public interface Choice
    {
        byte getChoice();
    }

    public class StringFormatException : Exception { public StringFormatException(string message) : base(message) { } }
    public class InvalidInputException : Exception { public InvalidInputException(string message) : base(message) { } }
    public class IndexOutOfRangeException : Exception { public IndexOutOfRangeException(string message) : base(message) { } }

    public class CharacterInformation
    {
        public struct CharacterInfo
        {
            private string PlayerName, FarmName, Gender, HairStyle, HairColor, EyeColor, Pet, FacialHair, BodyType;
            private short Age;
            public string playerName { get { return PlayerName; } set { PlayerName = value; } }
            public string farmName { get { return FarmName; } set { FarmName = value; } }
            public string gender { get { return Gender; } set { Gender = value; } }
            public string hairStyle { get { return HairStyle; } set { HairStyle = value; } }
            public string hairColor { get { return HairColor; } set { HairColor = value; } }
            public string eyeColor { get { return EyeColor; } set { EyeColor = value; } }
            public string pet { get { return Pet; } set { Pet = value; } }
            public string facialHair { get { return FacialHair; } set { FacialHair = value; } }
            public short age { get { return Age; } set { Age = value; } }
            public string bodyType { get { return BodyType; } set { BodyType = value; } }
        }
        public struct CharacterClothes
        {
            private string ShirtType, ShirtColor, PantsType, PantsColor, ShoesType, ShoesColor, Accessory, Hat, HatColor;
            public string shirtType { get { return ShirtType; } set { ShirtType = value; } }
            public string shirtColor { get { return ShirtColor; } set { ShirtColor = value; } }
            public string pantsType { get { return PantsType; } set { PantsType = value; } }
            public string pantsColor { get { return PantsColor; } set { PantsColor = value; } }
            public string shoesType { get { return ShoesType; } set { ShoesType = value; } }
            public string shoesColor { get { return ShoesColor; } set { ShoesColor = value; } }
            public string accessories { get { return Accessory; } set { Accessory = value; } }
            public string hat { get { return Hat; } set { Hat = value; } }
            public string hatColor { get { return HatColor; } set { HatColor = value; } }
        }
        public struct CharacterSkill
        {
            private short WoodChopping, Fishing, Farming, Crafting, Foraging, Mining, Combat;
            public CharacterSkill()
            {
                this.WoodChopping = 1;
                this.Fishing = 1;
                this.Farming = 1;
                this.Foraging = 1;
                this.Crafting = 1;
                this.Mining = 1;
                this.Combat = 1;
            }
            public short woodChopping { get { return this.WoodChopping; } set { this.WoodChopping = value; } }
            public short fishing { get { return this.Fishing; } set { this.Fishing = value; } }
            public short farming { get { return this.Farming; } set { this.Farming = value; } }
            public short crafting { get { return this.Crafting; } set { this.Crafting = value; } }
            public short foraging { get { return this.Foraging; } set { this.Foraging = value; } }
            public short mining { get { return this.Mining; } set { this.Mining = value; } }
            public short combat { get { return this.Combat; } set { this.Combat = value; } }
        }
        public int Id;
        public DateTime CreatedDate;

        public CharacterInfo info;
        public CharacterClothes clothes;
        public CharacterSkill skill;

        private static readonly List<CharacterInformation> characters = new();
    }

    public abstract class CharacterSummary : CharacterInformation
    {
        public abstract void showSummary(ref CharacterInfo charInfo);
        public abstract void showSummary(ref CharacterClothes charClothes);
        public abstract void showSummary(ref CharacterSkill charSkill);
    }

    public class CharacterDisplay : CharacterSummary
    {
        public override void showSummary(ref CharacterInfo charInfo)
        {
            Console.WriteLine($"=== Character Information ===");
            Console.WriteLine($"Name: {charInfo.playerName}");
            Console.WriteLine($"Farm: {charInfo.farmName}");
            Console.WriteLine($"Age: {charInfo.age}");
            Console.WriteLine($"Gender: {charInfo.gender}");
            Console.WriteLine($"Body Type: {charInfo.bodyType}");
        }

        public override void showSummary(ref CharacterClothes charClothes)
        {
            Console.WriteLine($"=== Character Clothing ===");
            Console.WriteLine($"Shirt: {charClothes.shirtType} ({charClothes.shirtColor})");
            Console.WriteLine($"Pants: {charClothes.pantsType} ({charClothes.pantsColor})");
            Console.WriteLine($"Shoes: {charClothes.shoesType} ({charClothes.shoesColor})");
        }

        public override void showSummary(ref CharacterSkill charSkill)
        {
            Console.WriteLine($"=== Character Skills ===");
            Console.WriteLine($"Farming: {charSkill.farming}");
            Console.WriteLine($"Combat: {charSkill.combat}");
            Console.WriteLine($"Mining: {charSkill.mining}");
        }
    }

}
    public class CharacterCreatorInfo : Choice
    {
        private bool isMale = false;
        private bool isFemale = false;

        public byte getChoice()
        {
            var key = Console.ReadKey(true);
            return (byte)(key.KeyChar - '0');
        }

        private char getCharChoice()
        {
            var key = Console.ReadKey(true);
            return char.ToUpper(key.KeyChar);
        }

        public void SetPlayerName(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.Write("Enter Player Name: ");
                    character.PlayerName = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(character.PlayerName))
                        throw new FormatException("Name can't be blank.");
                    else if (character.PlayerName.Length <= 2)
                        throw new InvalidInputException("Name can't be that short.");

                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseGender(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your Gender: [M]ale / [F]emale");
                    char genderChoice = getCharChoice();

                    if (genderChoice == 'M')
                    {
                        character.Gender = "Male";
                        isMale = true;
                        isFemale = false;
                        return;
                    }
                    else if (genderChoice == 'F')
                    {
                        character.Gender = "Female";
                        isFemale = true;
                        isMale = false;
                        return;
                    }
                    else
                    {
                        throw new FormatException("Must be M or F.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void SetFarmName(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.Write("Enter Farm name: ");
                    character.FarmName = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(character.FarmName))
                        throw new FormatException("Name can't be blank.");
                    else if (character.FarmName.Length <= 2)
                        throw new InvalidInputException("Name can't be that short.");

                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void SetFarmerAge(ref Character character)
        {
            while (true)
            {
                try
                {
                    short age;

                    Console.Clear();
                    Console.Write("Enter Farmer Age (18-80): ");
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                        throw new FormatException("Age can't be blank.");
                    else if (!short.TryParse(input, out age))
                        throw new FormatException("Age must be a number.");
                    else if (age > 80)
                        throw new InvalidInputException("Farmer Age is too high.");
                    else if (age < 18)
                        throw new InvalidInputException("Farmer must be at least 18 years old.");

                    

                    character.Age = age;
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void SetBodyType(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your Body Type:");
                    Console.WriteLine("[1] Chubby");
                    Console.WriteLine("[2] Slim");
                    Console.WriteLine("[3] Muscular");

                    byte choice = getChoice();
                    if (choice < 1 || choice > 3)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.BodyType = choice switch
                    {
                        1 => "Chubby",
                        2 => "Slim",
                        3 => "Muscular",
                        _ => "Average"
                    };
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void SetPet(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your preferred pet:");
                    Console.WriteLine("[1] Cat");
                    Console.WriteLine("[2] Dog");
                    Console.WriteLine("[3] Bird");

                    byte choice = getChoice();
                    if (choice < 1 || choice > 3)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.Pet = choice switch
                    {
                        1 => "Cat",
                        2 => "Dog",
                        3 => "Bird",
                        _ => "None"
                    };
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseHairStyle(ref Character character)
        {
            string[] mHair = { "Bald", "Buzz Cut", "Mohawk", "Ponytail", "Ivy League", "Mop-Top", "Pompadour", "Undercut", "Low Fade" };
            string[] fHair = { "Bald", "Pixie Cut", "Bob Cut", "Wolf Cut", "Long Straight", "Long Wavy", "Ponytails", "Pigtails", "Twintails" };

            while (true)
            {
                Console.Clear();
                try
                {
                    string[] hairList = isMale ? mHair : fHair;

                    Console.WriteLine("Choose your hair style:");
                    for (int i = 0; i < hairList.Length; i++)
                        Console.WriteLine($"[{i + 1}] {hairList[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > hairList.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.HairStyle = hairList[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseHairColor(ref Character character)
        {
            string[] hcolor = { "Black", "Red", "Blue", "Brunette", "Silver", "Blonde", "Auburn", "Gray", "Pink" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose Hair Color:");
                    for (int i = 0; i < hcolor.Length; i++)
                        Console.WriteLine($"[{i + 1}] {hcolor[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > hcolor.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.HairColor = hcolor[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseEyeColor(ref Character character)
        {
            string[] eColor = { "Heterochromia", "Amber", "Blue", "Green", "Brown", "Hazel", "Gray" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose Eye Color:");
                    for (int i = 0; i < eColor.Length; i++)
                        Console.WriteLine($"[{i + 1}] {eColor[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > eColor.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.EyeColor = eColor[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseFacialHair(ref Character character)
        {
            if (!isMale)
            {
                character.FacialHair = "None";
                return;
            }

            string[] fHair = { "Chevron Moustache", "Toothbrush Moustache", "Side Burns", "Anchor Beard", "Full Beard", "Goatee", "Ducktail", "Designer Stubble", "None" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose facial hair:");
                    for (int i = 0; i < fHair.Length; i++)
                        Console.WriteLine($"[{i + 1}] {fHair[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > fHair.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.FacialHair = fHair[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }
    }

    public class CharacterCreatorClothing : Choice
    {
        private string[] color = { "Black", "Blue", "Brown", "Green", "Red", "Yellow", "Orange", "White", "Purple" };
        private bool isFullClothes = false;

        public byte getChoice()
        {
            var key = Console.ReadKey(true);
            return (byte)(key.KeyChar - '0');
        }

        public void ChooseShirtType(ref Character character)
        {
            string[] uClothes = { "T-Shirt", "Polo Shirt", "Flannel Shirt", "Tank Top", "Crop Top", "Work Vest", "Field Coat", "Denim Jacket", "Overalls" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your upper clothing:");
                    for (int i = 0; i < uClothes.Length; i++)
                        Console.WriteLine($"[{i + 1}] {uClothes[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > uClothes.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.ShirtType = uClothes[choice - 1];
                    if (choice == 9) isFullClothes = true;
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseShirtColor(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose upper clothing color:");
                    for (int i = 0; i < color.Length; i++)
                        Console.WriteLine($"[{i + 1}] {color[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > color.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.ShirtColor = color[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChoosePantsType(ref Character character)
        {
            if (isFullClothes)
            {
                character.PantsType = "Overalls";
                return;
            }

            string[] lClothes = { "Shorts", "Jeans", "Cargo Pants", "Cargo Shorts", "Farm Chaps", "Jorts", "Farm Skirt", "Long Farm Skirt", "Farm Leggings" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your lower clothing:");
                    for (int i = 0; i < lClothes.Length; i++)
                        Console.WriteLine($"[{i + 1}] {lClothes[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > lClothes.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.PantsType = lClothes[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChoosePantsColor(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose lower clothing color:");
                    for (int i = 0; i < color.Length; i++)
                        Console.WriteLine($"[{i + 1}] {color[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > color.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.PantsColor = color[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseShoesType(ref Character character)
        {
            string[] footwearList = { "Slippers", "Loafers", "Sandals", "Work Boots", "Cowboy Boots", "Sneakers" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your Footwear:");
                    for (int i = 0; i < footwearList.Length; i++)
                        Console.WriteLine($"[{i + 1}] {footwearList[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > footwearList.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.ShoesType = footwearList[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseShoesColor(ref Character character)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose Footwear color:");
                    for (int i = 0; i < color.Length; i++)
                        Console.WriteLine($"[{i + 1}] {color[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > color.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.ShoesColor = color[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseAccessory(ref Character character)
        {
            string[] accessoriesList = { "Glasses", "Sun Glasses", "Necklace", "Work Gloves", "Choker", "Pendant", "Bracelet", "Earrings", "None" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your Accessory:");
                    for (int i = 0; i < accessoriesList.Length; i++)
                        Console.WriteLine($"[{i + 1}] {accessoriesList[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > accessoriesList.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.Accessory = accessoriesList[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseHat(ref Character character)
        {
            string[] hatList = { "Trucker Hat", "Baseball Cap", "Hard Hat", "Beanie", "Cowboy Hat", "Straw Hat", "Safari Hat", "None" };

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose your Hat:");
                    for (int i = 0; i < hatList.Length; i++)
                        Console.WriteLine($"[{i + 1}] {hatList[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > hatList.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.Hat = hatList[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }

        public void ChooseHatColor(ref Character character)
        {
            if (character.Hat == "None")
            {
                character.HatColor = "n/a";
                return;
            }

            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Choose Hat color:");
                    for (int i = 0; i < color.Length; i++)
                        Console.WriteLine($"[{i + 1}] {color[i]}");

                    byte choice = getChoice();
                    if (choice < 1 || choice > color.Length)
                        throw new HoRE_BETA.IndexOutOfRangeException("That is not included in the choices.");

                    character.HatColor = color[choice - 1];
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nInvalid! " + e.Message);
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
        }
    }

    public class CharacterCreatorSkills : Choice
    {
        public byte getChoice()
        {
            var key = Console.ReadKey(true);
            return (byte)(key.KeyChar - '0');
        }

        public void SetSkillAllocation(ref Character character)
        {
            switch (character.BodyType)
            {
                case "Chubby":
                    character.Fishing += 2;
                    character.Foraging += 3;
                    character.Farming += 1;
                    break;
                case "Muscular":
                    character.Mining += 2;
                    character.Combat += 3;
                    character.WoodChopping += 1;
                    break;
                case "Slim":
                    character.Farming += 2;
                    character.Crafting += 3;
                    character.Mining += 1;
                    break;
            }

            int skillPoints = 10;

            while (skillPoints > 0)
            {
                Console.Clear();
                Console.WriteLine("===== Skill Point Allocation =====");
                Console.WriteLine($"You have {skillPoints} skill points to invest.");
                Console.WriteLine("[1] Wood Chopping: " + character.WoodChopping);
                Console.WriteLine("[2] Fishing: " + character.Fishing);
                Console.WriteLine("[3] Farming: " + character.Farming);
                Console.WriteLine("[4] Crafting: " + character.Crafting);
                Console.WriteLine("[5] Foraging: " + character.Foraging);
                Console.WriteLine("[6] Mining: " + character.Mining);
                Console.WriteLine("[7] Combat: " + character.Combat);
                Console.Write("\nChoose skill to invest in (1-7): ");

                byte choice = getChoice();
                if (choice >= 1 && choice <= 7)
                {
                    Console.Write("How many points to invest? ");
                    string input = Console.ReadLine();

                    if (short.TryParse(input, out short points) && points > 0 && points <= skillPoints)
                    {
                        switch (choice)
                        {
                            case 1: character.WoodChopping += points; break;
                            case 2: character.Fishing += points; break;
                            case 3: character.Farming += points; break;
                            case 4: character.Crafting += points; break;
                            case 5: character.Foraging += points; break;
                            case 6: character.Mining += points; break;
                            case 7: character.Combat += points; break;
                        }
                        skillPoints -= points;
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of points!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                    Console.ReadKey();
                }
            }
        }
    }
public class Program
{
    private static CharacterCreatorInfo charInfoCreator = new CharacterCreatorInfo();
    private static CharacterCreatorClothing charClothesCreator = new CharacterCreatorClothing();
    private static CharacterCreatorSkills charSkillsCreator = new CharacterCreatorSkills();

    static void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("===== Harvests of the Ruined Earth =====");
        Console.WriteLine("========================================");
        Console.WriteLine("=           [1] New Game               =");
        Console.WriteLine("=           [2] Load Game              =");
        Console.WriteLine("=           [3] Campaign Mode          =");
        Console.WriteLine("=           [4] Credits                =");
        Console.WriteLine("=           [5] Exit Game              =");
        Console.WriteLine("========================================");
    }

    static void GoNewGame()
    {
        Console.Clear();
        Console.WriteLine("===== Character Creation =====");

        Character character = new Character();

        // Basic Information
        charInfoCreator.SetPlayerName(ref character);
        charInfoCreator.ChooseGender(ref character);
        charInfoCreator.SetFarmName(ref character);
        charInfoCreator.SetFarmerAge(ref character);
        charInfoCreator.SetBodyType(ref character);
        charInfoCreator.SetPet(ref character);

        // Appearance
        charInfoCreator.ChooseHairStyle(ref character);
        charInfoCreator.ChooseHairColor(ref character);
        charInfoCreator.ChooseFacialHair(ref character);
        charInfoCreator.ChooseEyeColor(ref character);

        // Clothing
        charClothesCreator.ChooseShirtType(ref character);
        charClothesCreator.ChooseShirtColor(ref character);
        charClothesCreator.ChoosePantsType(ref character);
        charClothesCreator.ChoosePantsColor(ref character);
        charClothesCreator.ChooseShoesType(ref character);
        charClothesCreator.ChooseShoesColor(ref character);
        charClothesCreator.ChooseHat(ref character);
        charClothesCreator.ChooseHatColor(ref character);
        charClothesCreator.ChooseAccessory(ref character);

        // Skills
        charSkillsCreator.SetSkillAllocation(ref character);

        // Set creation date
        character.CreatedDate = DateTime.Now;

        // Save to database
        if (DatabaseHelper.InsertCharacter(character))
        {
            Console.Clear();
            Console.WriteLine("===== Character Created Successfully =====");
            character.DisplayCharacter();

            Console.Write("\nPress any key to return to menu...");
            Console.ReadKey(true);
        }
        else
        {
            Console.WriteLine("Failed to save character to database!");
            Console.ReadKey();
        }
    }

    static void LoadCharacterMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Load Character =====");

            var characters = DatabaseHelper.LoadAllCharacters();

            if (characters.Count == 0)
            {
                Console.WriteLine("No saved characters found!");
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey(true);
                return;
            }

            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"Character [{i + 1}] {characters[i].PlayerName} - {characters[i].FarmName} (Created: {characters[i].CreatedDate:MM/dd/yyyy})");
                Console.WriteLine("====================================");
            }

            Console.WriteLine($"[1] View Character Details");
            Console.WriteLine($"[2] Delete Character");
            Console.WriteLine($"[3] Return to Main Menu");
            Console.Write("\nChoose an option: ");

            if (byte.TryParse(Console.ReadLine(), out byte choice))
            {
                if (choice == 1)
                {
                    ViewCharacterDetails(characters);
                }
                else if (choice == 2)
                {
                    DeleteCharacter(characters);
                }
                else if (choice == 3)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                    Console.ReadKey(true);
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
                Console.ReadKey(true);
            }
        }
    }

    static void ViewCharacterDetails(List<Character> characters)
    {
        Console.Write("Enter character number to view details: ");
        if (int.TryParse(Console.ReadLine(), out int charNum) && charNum >= 1 && charNum <= characters.Count)
        {
            Console.Clear();

            Console.WriteLine("Choose display option:");
            Console.WriteLine("[1] Full details (with skills)");
            Console.WriteLine("[2] Profile without skills");
            Console.WriteLine("[3] Brief summary");

            if (byte.TryParse(Console.ReadLine(), out byte displayChoice))
            {
                var character = characters[charNum - 1];

                switch (displayChoice)
                {
                    case 1:
                        character.DisplayCharacter(); // Shows everything
                        break;
                    case 2:
                        character.DisplayCharacter(false); // Shows everything EXCEPT skills
                        break;
                    case 3:
                        character.DisplayCharacter("brief"); // Just name and farm
                        break;
                    default:
                        character.DisplayCharacter();
                        break;
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        else
        {
            Console.WriteLine("Invalid character number!");
            Console.ReadKey(true);
        }
    }

    static void DeleteCharacter(List<Character> characters)
    {
        Console.Write("Enter character number to delete: ");
        if (int.TryParse(Console.ReadLine(), out int charNum) && charNum >= 1 && charNum <= characters.Count)
        {
            var characterToDelete = characters[charNum - 1];
            Console.Write($"Are you sure you want to delete {characterToDelete.PlayerName}? (Y/N): ");
            string confirm = Console.ReadLine()?.ToUpper();

            if (confirm == "Y" || confirm == "YES")
            {
                if (DatabaseHelper.DeleteCharacter(characterToDelete.Id))
                {
                    Console.WriteLine("Character deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to delete character!");
                }
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
            Console.ReadKey(true);
        }
        else
        {
            Console.WriteLine("Invalid character number!");
            Console.ReadKey(true);
        }
    }

    static void PlayWithCharacter(Character character)
    {
        Console.Clear();
        Console.WriteLine($"===== Playing as {character.PlayerName} =====");
        character.DisplayCharacter();

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey(true);
    }

    static void ShowCampaign()
    {
        Console.Clear();
        Console.WriteLine("===== Campaign Mode =====");
        string lore = "Due to a Nuclear Powerplant meltdown, the farm's water source has been severely contaminated." +
            "Due to this, some critters\nhave mutated into something harmful for the farm and for people." +
            "Your farm has become an experiment site for pesticides." +
            "\nMost farmers left the once fertile fields of Greenfields, Now a hollow shell of its former glory, a few stubborn \nresidents remain in the \"no-grow zones\"." +
            "It is up to you to cooperate with the experiments and develop them further to \ntake control of the once fertile land of Greenfields." +
            "\r\n\r\nAfter the meltdown, Greenfields shifted into a place where every task felt like a challenge from the land itself." +
            "\nThe forests grew thicker and darker, forcing you to clear paths just to move between what's left of the farms." +
            "\nStrange creatures lurk in the shadows now, some skittish, others hostile enough to charge at anything that moves." +
            "\nBecause of this, everyone carries something for protection, even during simple chores." +
            "\nThe residents have learned that staying prepared is the only way to avoid becoming another cautionary tale." +
            "\r\n\r\nThe rivers and lakes didn't escape mutation either, filled with odd-looking fish that behave unpredictably." +
            "\nLocals still brave the waters, hoping to catch something edible, though nobody ever knows what might come up with the \nline." +
            " Beneath the ground, old mining tunnels glow faintly from minerals twisted by radiation, valuable but dangerous to extract." +
            " Those who venture down there return with materials that help craft tools needed to survive the changing \nenvironment." +
            " Little by little, people adapt, shaping whatever resources remain into their lifeline.\n";

        for (int i = 0; i < lore.Length; i++)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                Console.Write(lore.Substring(i));
                break;
            }
            Console.Write(lore[i]);
            Thread.Sleep(50);
        }
        Console.WriteLine("\nPress any key to go back...");
        Console.ReadKey(true);
    }

    static void ShowCredits()
    {
        Console.Clear();
        Console.WriteLine("=====================");
        Console.WriteLine("====== Credits ======");
        Console.WriteLine("=====================");
        string credits = "\n===== Instructor =====\nMr.Lorenz Christopher Afan\n" +
                         "\n===== Programmers =====\nJanssen San Diego\nJenna Palisoc\nJonel Lucas\n" +
                         "\n===== Documentation =====\nJanssen San Diego\nJenna Palisoc\nJonel Lucas\n" +
                         "\n===== Special Mention =====\nDanielo Carretero\nAlvin Chavez\nBryan Sese\nJester Rubia\n";

        for (int i = 0; i < credits.Length; i++)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                Console.Write(credits.Substring(i));
                break;
            }
            Console.Write(credits[i]);
            Thread.Sleep(40);
        }
        Console.Write("\nPress any key to go back...");
        Console.ReadKey(true);
    }

    static byte GetChoice()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        return (byte)(key.KeyChar - '0');
    }

    public static void Main(string[] args)
    {
        if (!DatabaseHelper.InitializeDatabase())
        {
            Console.WriteLine("Failed to connect to database. Exiting...");
            Console.ReadKey();
            return;
        }

        Console.CursorVisible = false;

        try
        {
            while (true)
            {
                ShowMainMenu();
                byte c = GetChoice();
                Console.WriteLine();

                switch (c)
                {
                    case 1: GoNewGame(); break;
                    case 2: LoadCharacterMenu(); break;
                    case 3: ShowCampaign(); break;
                    case 4: ShowCredits(); break;
                    case 5:
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure? \n[1] Yes \n[2] No");
                            c = GetChoice();
                            if (c == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("===== Program Terminated =====");
                                DatabaseHelper.CloseDatabase();
                                return;
                            }
                            else if (c == 2) { break; }
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        finally
        {
            DatabaseHelper.CloseDatabase();
        }
    }
}