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
                        age SMALLINT,
                        body_type VARCHAR(50),
                        pet VARCHAR(50),
                        hair_style VARCHAR(50),
                        hair_color VARCHAR(50),
                        eye_color VARCHAR(50),
                        facial_hair VARCHAR(50),
                        shirt_type VARCHAR(50),
                        shirt_color VARCHAR(50),
                        pants_type VARCHAR(50),
                        pants_color VARCHAR(50),
                        shoes_type VARCHAR(50),
                        shoes_color VARCHAR(50),
                        accessory VARCHAR(50),
                        hat VARCHAR(50),
                        hat_color VARCHAR(50),
                        wood_chopping SMALLINT DEFAULT 1,
                        fishing SMALLINT DEFAULT 1,
                        farming SMALLINT DEFAULT 1,
                        crafting SMALLINT DEFAULT 1,
                        foraging SMALLINT DEFAULT 1,
                        mining SMALLINT DEFAULT 1,
                        combat SMALLINT DEFAULT 1,
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
                            Age = reader.GetInt16("age"),
                            BodyType = reader.GetString("body_type"),
                            Pet = reader.GetString("pet"),
                            HairStyle = reader.GetString("hair_style"),
                            HairColor = reader.GetString("hair_color"),
                            EyeColor = reader.GetString("eye_color"),
                            FacialHair = reader.GetString("facial_hair"),
                            ShirtType = reader.GetString("shirt_type"),
                            ShirtColor = reader.GetString("shirt_color"),
                            PantsType = reader.GetString("pants_type"),
                            PantsColor = reader.GetString("pants_color"),
                            ShoesType = reader.GetString("shoes_type"),
                            ShoesColor = reader.GetString("shoes_color"),
                            Accessory = reader.GetString("accessory"),
                            Hat = reader.GetString("hat"),
                            HatColor = reader.GetString("hat_color"),
                            WoodChopping = reader.GetInt16("wood_chopping"),
                            Fishing = reader.GetInt16("fishing"),
                            Farming = reader.GetInt16("farming"),
                            Crafting = reader.GetInt16("crafting"),
                            Foraging = reader.GetInt16("foraging"),
                            Mining = reader.GetInt16("mining"),
                            Combat = reader.GetInt16("combat"),
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

        public static bool InsertCharacter(Character character)
        {
            try
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Database connection is not open!");
                    return false;
                }

                string query = @"INSERT INTO characterData 
                    (player_name, farm_name, gender, age, body_type, pet,
                     hair_style, hair_color, eye_color, facial_hair,
                     shirt_type, shirt_color, pants_type, pants_color,
                     shoes_type, shoes_color, accessory, hat, hat_color,
                     wood_chopping, fishing, farming, crafting, foraging, mining, combat) 
                    VALUES 
                    (@PlayerName, @FarmName, @Gender, @Age, @BodyType, @Pet,
                     @HairStyle, @HairColor, @EyeColor, @FacialHair,
                     @ShirtType, @ShirtColor, @PantsType, @PantsColor,
                     @ShoesType, @ShoesColor, @Accessory, @Hat, @HatColor,
                     @WoodChopping, @Fishing, @Farming, @Crafting, @Foraging, @Mining, @Combat)";

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@PlayerName", character.PlayerName);
                    command.Parameters.AddWithValue("@FarmName", character.FarmName);
                    command.Parameters.AddWithValue("@Gender", character.Gender);
                    command.Parameters.AddWithValue("@Age", character.Age);
                    command.Parameters.AddWithValue("@BodyType", character.BodyType);
                    command.Parameters.AddWithValue("@Pet", character.Pet);
                    command.Parameters.AddWithValue("@HairStyle", character.HairStyle);
                    command.Parameters.AddWithValue("@HairColor", character.HairColor);
                    command.Parameters.AddWithValue("@EyeColor", character.EyeColor);
                    command.Parameters.AddWithValue("@FacialHair", character.FacialHair);
                    command.Parameters.AddWithValue("@ShirtType", character.ShirtType);
                    command.Parameters.AddWithValue("@ShirtColor", character.ShirtColor);
                    command.Parameters.AddWithValue("@PantsType", character.PantsType);
                    command.Parameters.AddWithValue("@PantsColor", character.PantsColor);
                    command.Parameters.AddWithValue("@ShoesType", character.ShoesType);
                    command.Parameters.AddWithValue("@ShoesColor", character.ShoesColor);
                    command.Parameters.AddWithValue("@Accessory", character.Accessory);
                    command.Parameters.AddWithValue("@Hat", character.Hat);
                    command.Parameters.AddWithValue("@HatColor", character.HatColor);
                    command.Parameters.AddWithValue("@WoodChopping", character.WoodChopping);
                    command.Parameters.AddWithValue("@Fishing", character.Fishing);
                    command.Parameters.AddWithValue("@Farming", character.Farming);
                    command.Parameters.AddWithValue("@Crafting", character.Crafting);
                    command.Parameters.AddWithValue("@Foraging", character.Foraging);
                    command.Parameters.AddWithValue("@Mining", character.Mining);
                    command.Parameters.AddWithValue("@Combat", character.Combat);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Character saved to database! Rows affected: {rowsAffected}");
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting character: {ex.Message}");
                return false;
            }
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
                                Age = reader.GetInt16("age"),
                                BodyType = reader.GetString("body_type"),
                                Pet = reader.GetString("pet"),
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
                                WoodChopping = reader.GetInt16("wood_chopping"),
                                Fishing = reader.GetInt16("fishing"),
                                Crafting = reader.GetInt16("crafting"),
                                Foraging = reader.GetInt16("foraging"),
                                Mining = reader.GetInt16("mining"),
                                Combat = reader.GetInt16("combat"),
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
    }
}