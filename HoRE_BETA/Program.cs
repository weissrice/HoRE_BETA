using System;
using MySql.Data.MySqlClient;
public class Program
{
    public static void connectToDatabase()
    {
        string connectionString = "Server=localhost;Database=character_project;User ID=root;Password=W31ssR1ce277353!;";
        using MySqlConnection connection = new MySqlConnection(connectionString);
        try 
        {
            connection.Open();
            Console.WriteLine("Connection Open.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to the database: {ex.Message}");
        }
        finally
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Connection Closed.");
            }
        }
    }

        static void showMainMenu()
    {
        Console.Clear();
        connectToDatabase();
        Console.WriteLine("\n===== Harvests of Ruined Earth =====");
        Console.WriteLine("=====================");
        Console.WriteLine("===== Main Menu =====");
        Console.WriteLine("[1] Start New Game");
        Console.WriteLine("[2] Campaign Mode");
        Console.WriteLine("[3] Credits");
        Console.WriteLine("[4] Exit");
        Console.WriteLine("=====================");
    }

    static void goNewGame()
    {
        Console.Clear();
        Console.WriteLine("===== Character Creation =====");

        // Get player name and farm name
        string playerName = "";
        while (string.IsNullOrWhiteSpace(playerName))
        {
            Console.Write("Enter player name: ");
            playerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Invalid name!");
            }
        }

        string farmName = "";
        while (string.IsNullOrWhiteSpace(farmName))
        {
            Console.Write("Enter farm name: ");
            farmName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(farmName))
            {
                Console.WriteLine("Invalid farm name!");
            }
        }

        // Character customization
        string gender = chooseGender();
        string hairStyle = chooseHairStyle();
        string hairColor = chooseHairColor();
        string eyeColor = chooseEyeColor();
        string shirtType = chooseShirtType();
        string shirtColor = chooseShirtColor();
        string pantsType = choosePantsType();
        string pantsColor = choosePantsColor();
        string shoesType = chooseShoesType();
        string shoesColor = chooseShoesColor();
        string accessory = chooseAccessory();

        // Skill point allocation
        int skillPoints = 10;
        int woodChopping = 0, fishing = 0, harvesting = 0, crafting = 0, foraging = 0, mining = 0, combat = 0;

        Console.Clear();
        Console.WriteLine("===== Skill Point Allocation =====");
        Console.WriteLine($"You have {skillPoints} skill points to invest.");

        while (skillPoints > 0)
        {
            Console.WriteLine($"Remaining points: {skillPoints}");
            Console.WriteLine("[1] Wood Chopping: " + woodChopping);
            Console.WriteLine("[2] Fishing: " + fishing);
            Console.WriteLine("[3] Harvesting: " + harvesting);
            Console.WriteLine("[4] Crafting: " + crafting);
            Console.WriteLine("[5] Foraging: " + foraging);
            Console.WriteLine("[6] Mining: " + mining);
            Console.WriteLine("[7] Combat: " + combat);
            Console.Write("Choose skill to invest in (1-7): ");

            byte choice = getChoice();
            if (choice >= 1 && choice <= 7)
            {
                Console.Write("How many points to invest? ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int points) && points > 0 && points <= skillPoints)
                {
                    switch (choice)
                    {
                        case 1: woodChopping += points; break;
                        case 2: fishing += points; break;
                        case 3: harvesting += points; break;
                        case 4: crafting += points; break;
                        case 5: foraging += points; break;
                        case 6: mining += points; break;
                        case 7: combat += points; break;
                    }
                    skillPoints -= points;
                }
                else
                {
                    Console.WriteLine("Invalid number of points!");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }
        }

        // Display final character
        Console.Clear();
        Console.WriteLine("===== Character Created Successfully =====");
        Console.WriteLine($"Player Name: {playerName}");
        Console.WriteLine($"Farm Name: {farmName}");
        Console.WriteLine($"Gender: {gender}");
        Console.WriteLine($"Hairstyle: {hairStyle}");
        Console.WriteLine($"Hair Color: {hairColor}");
        Console.WriteLine($"Eye Color: {eyeColor}");
        Console.WriteLine($"Shirt: {shirtType} ({shirtColor})");
        Console.WriteLine($"Pants: {pantsType} ({pantsColor})");
        Console.WriteLine($"Shoes: {shoesType} ({shoesColor})");
        Console.WriteLine($"Accessory: {accessory}");
        Console.WriteLine("\n===== Skills =====");
        Console.WriteLine($"Wood Chopping: {woodChopping}");
        Console.WriteLine($"Fishing: {fishing}");
        Console.WriteLine($"Harvesting: {harvesting}");
        Console.WriteLine($"Crafting: {crafting}");
        Console.WriteLine($"Foraging: {foraging}");
        Console.WriteLine($"Mining: {mining}");
        Console.WriteLine($"Combat: {combat}");

        Console.Write("\nPress any key to return to menu...");
        Console.ReadKey(true);
    }

    static string chooseGender()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose a Gender:");
            Console.WriteLine("[1] Male");
            Console.WriteLine("[2] Female");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Male";
                case 2: return "Female";
                default: Console.WriteLine("Invalid Choice!"); break;

            }
        }
    }
    static string chooseHairStyle()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose your hairstyle:");
            Console.WriteLine("[1] Short");
            Console.WriteLine("[2] Long");
            Console.WriteLine("[3] Curly");
            Console.WriteLine("[4] Spiky");
            Console.WriteLine("[5] Bald");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Short";
                case 2: return "Long";
                case 3: return "Curly";
                case 4: return "Spiky";
                case 5: return "Bald";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string chooseHairColor()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose hair color:");
            Console.WriteLine("[1] Black");
            Console.WriteLine("[2] Brown");
            Console.WriteLine("[3] Blonde");
            Console.WriteLine("[4] Red");
            Console.WriteLine("[5] Gray");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Black";
                case 2: return "Brown";
                case 3: return "Blonde";
                case 4: return "Red";
                case 5: return "Gray";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string chooseEyeColor()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose eye color:");
            Console.WriteLine("[1] Brown");
            Console.WriteLine("[2] Blue");
            Console.WriteLine("[3] Green");
            Console.WriteLine("[4] Hazel");
            Console.WriteLine("[5] Gray");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Brown";
                case 2: return "Blue";
                case 3: return "Green";
                case 4: return "Hazel";
                case 5: return "Gray";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string chooseShirtType()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose shirt type:");
            Console.WriteLine("[1] T-Shirt");
            Console.WriteLine("[2] Button-Up");
            Console.WriteLine("[3] Tank Top");
            Console.WriteLine("[4] Sweater");
            Console.WriteLine("[5] Hoodie");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "T-Shirt";
                case 2: return "Button-Up";
                case 3: return "Tank Top";
                case 4: return "Sweater";
                case 5: return "Hoodie";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string chooseShirtColor()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose shirt color:");
            Console.WriteLine("[1] Red");
            Console.WriteLine("[2] Blue");
            Console.WriteLine("[3] Green");
            Console.WriteLine("[4] White");
            Console.WriteLine("[5] Black");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Red";
                case 2: return "Blue";
                case 3: return "Green";
                case 4: return "White";
                case 5: return "Black";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string choosePantsType()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose pants type:");
            Console.WriteLine("[1] Jeans");
            Console.WriteLine("[2] Shorts");
            Console.WriteLine("[3] Slacks");
            Console.WriteLine("[4] Sweatpants");
            Console.WriteLine("[5] Overalls");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Jeans";
                case 2: return "Shorts";
                case 3: return "Slacks";
                case 4: return "Sweatpants";
                case 5: return "Overalls";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string choosePantsColor()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose pants color:");
            Console.WriteLine("[1] Blue");
            Console.WriteLine("[2] Black");
            Console.WriteLine("[3] Brown");
            Console.WriteLine("[4] Gray");
            Console.WriteLine("[5] White");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Blue";
                case 2: return "Black";
                case 3: return "Brown";
                case 4: return "Gray";
                case 5: return "White";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string chooseShoesType()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose shoes type:");
            Console.WriteLine("[1] Sneakers");
            Console.WriteLine("[2] Boots");
            Console.WriteLine("[3] Sandals");
            Console.WriteLine("[4] Loafers");
            Console.WriteLine("[5] Work Boots");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Sneakers";
                case 2: return "Boots";
                case 3: return "Sandals";
                case 4: return "Loafers";
                case 5: return "Work Boots";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string chooseShoesColor()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose shoes color:");
            Console.WriteLine("[1] Black");
            Console.WriteLine("[2] Brown");
            Console.WriteLine("[3] White");
            Console.WriteLine("[4] Blue");
            Console.WriteLine("[5] Gray");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Black";
                case 2: return "Brown";
                case 3: return "White";
                case 4: return "Blue";
                case 5: return "Gray";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static string chooseAccessory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose accessory:");
            Console.WriteLine("[1] Hat");
            Console.WriteLine("[2] Glasses");
            Console.WriteLine("[3] Watch");
            Console.WriteLine("[4] Necklace");
            Console.WriteLine("[5] None");

            byte choice = getChoice();
            switch (choice)
            {
                case 1: return "Hat";
                case 2: return "Glasses";
                case 3: return "Watch";
                case 4: return "Necklace";
                case 5: return "None";
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static void showCampaign()
    {
        Console.Clear();
        Console.WriteLine("===== Campaign Mode =====");
        Console.WriteLine("Due to a Nuclear Powerplant meltdown, the farm's water source has been severely contaminated." +
            "Due to this, some critters\nhave mutated into something harmful for the farm and for people." +
            "Your farm has become an experiment site for pesticides." +
            "\nMost farmers left the once fertile fields of Greenfields, Now a hollow shell of its former glory, a few stubborn \nresidents remain in the \"no-grow zones\"." +
            "It is up to you to cooperate with the experiments and develop them further to \ntake control of the once fertile land of Greenfields." +
            "\r\n\r\nAfter the meltdown, Greenfields shifted into a place where every task felt like a challenge from the land itself." +
            "\nThe forests grew thicker and darker, forcing you to clear paths just to move between what’s left of the farms." +
            "\nStrange creatures lurk in the shadows now, some skittish, others hostile enough to charge at anything that moves." +
            "\nBecause of this, everyone carries something for protection, even during simple chores." +
            "\nThe residents have learned that staying prepared is the only way to avoid becoming another cautionary tale." +
            "\r\n\r\nThe rivers and lakes didn’t escape mutation either, filled with odd-looking fish that behave unpredictably." +
            "\nLocals still brave the waters, hoping to catch something edible, though nobody ever knows what might come up with the \nline." +
            " Beneath the ground, old mining tunnels glow faintly from minerals twisted by radiation, valuable but dangerous to extract." +
            " Those who venture down there return with materials that help craft tools needed to survive the changing \nenvironment." +
            " Little by little, people adapt, shaping whatever resources remain into their lifeline.");
        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey(true);
    }

    static void showCredits()
    {
        Console.Clear();
        Console.WriteLine("===== Credits =====");
        Console.WriteLine("Made by:");
        Console.WriteLine("San Diego, Janssen Danielle P.");
        Console.WriteLine("Palisoc, Jenna Kim A.");
        Console.WriteLine("Lucas, Jonel P.");
        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey(true);
    }

    static byte getChoice()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        return (byte)(key.KeyChar - '0');
    }

    public static void Main(string[] args)
    {
        

        while (true)
        {
            showMainMenu();
            Console.Write("Enter your choice (1-4): ");
            byte choice = getChoice();
            Console.WriteLine();

            switch (choice)
            {
                case 1: goNewGame(); break;
                case 2: showCampaign(); break;
                case 3: showCredits(); break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("===== Program Terminated =====");
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice!");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}