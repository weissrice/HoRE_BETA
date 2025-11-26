using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HoRE_BETA
{
    public static class DatabaseHelper
    {
        private static string connectionString = "Server=localhost;Database=character_project;User ID=root;Password=W31ssR1ce277353!;";
        private static bool tableChecked = false;
        private static MySqlConnection _connection;

        public static bool InitializeDatabase()
        {
            try
            {
                _connection = new MySqlConnection(connectionString);
                _connection.Open();
                Console.WriteLine("Database Connection Established.");

                if (!tableChecked)
                {
                    CreateCharactersTable();
                    tableChecked = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
                return false;
            }
        }

        public static void CloseDatabase()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
                Console.WriteLine("Database Connection Closed.");
            }
        }

        private static void CreateCharactersTable()
        {
            try
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Database connection is not open!");
                    return;
                }

                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS characterData (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        player_name VARCHAR(100) NOT NULL,
                        farm_name VARCHAR(100) NOT NULL,
                        gender VARCHAR(20),
                        hair_style VARCHAR(50),
                        hair_color VARCHAR(50),
                        eye_color VARCHAR(50),
                        shirt_type VARCHAR(50),
                        shirt_color VARCHAR(50),
                        pants_type VARCHAR(50),
                        pants_color VARCHAR(50),
                        shoes_type VARCHAR(50),
                        shoes_color VARCHAR(50),
                        accessory VARCHAR(50),
                        wood_chopping INT DEFAULT 0,
                        fishing INT DEFAULT 0,
                        harvesting INT DEFAULT 0,
                        crafting INT DEFAULT 0,
                        foraging INT DEFAULT 0,
                        mining INT DEFAULT 0,
                        combat INT DEFAULT 0,
                        created_date DATETIME DEFAULT CURRENT_TIMESTAMP
                    )";

                using (MySqlCommand command = new MySqlCommand(createTableQuery, _connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Characters table checked/created successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table: {ex.Message}");
            }
        }

        public static List<Character> LoadAllCharacters()
        {
            var characters = new List<Character>();

            try
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Database connection is not open!");
                    return characters;
                }

                string query = "SELECT * FROM characterData ORDER BY created_date DESC";

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var character = new Character
                        {
                            Id = reader.GetInt32("id"),
                            PlayerName = reader.GetString("player_name"),
                            FarmName = reader.GetString("farm_name"),
                            Gender = reader.GetString("gender"),
                            HairStyle = reader.GetString("hair_style"),
                            HairColor = reader.GetString("hair_color"),
                            EyeColor = reader.GetString("eye_color"),
                            ShirtType = reader.GetString("shirt_type"),
                            ShirtColor = reader.GetString("shirt_color"),
                            PantsType = reader.GetString("pants_type"),
                            PantsColor = reader.GetString("pants_color"),
                            ShoesType = reader.GetString("shoes_type"),
                            ShoesColor = reader.GetString("shoes_color"),
                            Accessory = reader.GetString("accessory"),
                            WoodChopping = reader.GetInt32("wood_chopping"),
                            Fishing = reader.GetInt32("fishing"),
                            Harvesting = reader.GetInt32("harvesting"),
                            Crafting = reader.GetInt32("crafting"),
                            Foraging = reader.GetInt32("foraging"),
                            Mining = reader.GetInt32("mining"),
                            Combat = reader.GetInt32("combat"),
                            CreatedDate = reader.GetDateTime("created_date")
                        };
                        characters.Add(character);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading characters: {ex.Message}");
            }

            return characters;
        }

        public static Character LoadCharacterById(int characterId)
        {
            try
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Database connection is not open!");
                    return null;
                }

                string query = "SELECT * FROM characterData WHERE id = @CharacterId";

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@CharacterId", characterId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Character
                            {
                                Id = reader.GetInt32("id"),
                                PlayerName = reader.GetString("player_name"),
                                FarmName = reader.GetString("farm_name"),
                                Gender = reader.GetString("gender"),
                                HairStyle = reader.GetString("hair_style"),
                                HairColor = reader.GetString("hair_color"),
                                EyeColor = reader.GetString("eye_color"),
                                ShirtType = reader.GetString("shirt_type"),
                                ShirtColor = reader.GetString("shirt_color"),
                                PantsType = reader.GetString("pants_type"),
                                PantsColor = reader.GetString("pants_color"),
                                ShoesType = reader.GetString("shoes_type"),
                                ShoesColor = reader.GetString("shoes_color"),
                                Accessory = reader.GetString("accessory"),
                                WoodChopping = reader.GetInt32("wood_chopping"),
                                Fishing = reader.GetInt32("fishing"),
                                Harvesting = reader.GetInt32("harvesting"),
                                Crafting = reader.GetInt32("crafting"),
                                Foraging = reader.GetInt32("foraging"),
                                Mining = reader.GetInt32("mining"),
                                Combat = reader.GetInt32("combat"),
                                CreatedDate = reader.GetDateTime("created_date")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading character: {ex.Message}");
            }

            return null;
        }

        public static bool DeleteCharacter(int characterId)
        {
            try
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Database connection is not open!");
                    return false;
                }

                string query = "DELETE FROM characterData WHERE id = @CharacterId";

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@CharacterId", characterId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting character: {ex.Message}");
                return false;
            }
        }

        public static bool InsertCharacter(string playerName, string farmName, string gender,
    string hairStyle, string hairColor, string eyeColor,
    string shirtType, string shirtColor, string pantsType,
    string pantsColor, string shoesType, string shoesColor,
    string accessory, int woodChopping, int fishing,
    int harvesting, int crafting, int foraging, int mining, int combat)
        {
            string query = @"INSERT INTO characterData 
            (player_name, farm_name, gender, hair_style, hair_color, eye_color, 
             shirt_type, shirt_color, pants_type, pants_color, shoes_type, shoes_color, 
             accessory, wood_chopping, fishing, harvesting, crafting, foraging, mining, combat, created_date) 
            VALUES 
            (@PlayerName, @FarmName, @Gender, @HairStyle, @HairColor, @EyeColor, 
             @ShirtType, @ShirtColor, @PantsType, @PantsColor, @ShoesType, @ShoesColor, 
             @Accessory, @WoodChopping, @Fishing, @Harvesting, @Crafting, @Foraging, @Mining, @Combat, @CreatedDate)";

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@PlayerName", playerName);
                command.Parameters.AddWithValue("@FarmName", farmName);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@HairStyle", hairStyle);
                command.Parameters.AddWithValue("@HairColor", hairColor);
                command.Parameters.AddWithValue("@EyeColor", eyeColor);
                command.Parameters.AddWithValue("@ShirtType", shirtType);
                command.Parameters.AddWithValue("@ShirtColor", shirtColor);
                command.Parameters.AddWithValue("@PantsType", pantsType);
                command.Parameters.AddWithValue("@PantsColor", pantsColor);
                command.Parameters.AddWithValue("@ShoesType", shoesType);
                command.Parameters.AddWithValue("@ShoesColor", shoesColor);
                command.Parameters.AddWithValue("@Accessory", accessory);
                command.Parameters.AddWithValue("@WoodChopping", woodChopping);
                command.Parameters.AddWithValue("@Fishing", fishing);
                command.Parameters.AddWithValue("@Harvesting", harvesting);
                command.Parameters.AddWithValue("@Crafting", crafting);
                command.Parameters.AddWithValue("@Foraging", foraging);
                command.Parameters.AddWithValue("@Mining", mining);
                command.Parameters.AddWithValue("@Combat", combat);
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Character saved to database! Rows affected: {rowsAffected}");
                return rowsAffected > 0;
            }
        }

        public static bool TestConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Database connection is active.");
                return true;
            }
            else
            {
                Console.WriteLine("Database connection is not open.");
                return false;
            }
        }
    }
}