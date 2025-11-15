using Microsoft.Data.SqlClient;

// Update this connection string if your SQL Server instance differs
var connectionString = "Server=localhost\\SQLEXPRESS;Database=SalesOrderDB;Trusted_Connection=True;TrustServerCertificate=True;";

var clients = new[]
{
    new { Name = "Acme Corporation", Address1 = "123 Industry Rd", Address2 = "Suite 10", Address3 = "Building A", Suburb = "Northbay", City = "Metropolis", State = "CA", PostCode = "90001" },
    new { Name = "Beta Traders", Address1 = "45 Market St", Address2 = "", Address3 = "", Suburb = "Central", City = "Gotham", State = "NY", PostCode = "10001" },
    new { Name = "Gamma Supplies", Address1 = "9 Commerce Ave", Address2 = "Floor 2", Address3 = "", Suburb = "Harbor", City = "Coast City", State = "FL", PostCode = "33010" }
};

var items = new[]
{
    new { ItemCode = "ITEM001", Description = "Product A", Price = 12.50m },
    new { ItemCode = "ITEM002", Description = "Product B", Price = 25.00m },
    new { ItemCode = "ITEM003", Description = "Product C", Price = 7.99m }
};

Console.WriteLine("Seeding clients into SalesOrderDB...");

using var conn = new SqlConnection(connectionString);
await conn.OpenAsync();

// Ensure table exists (basic check) - if not, fail fast
var checkCmd = new SqlCommand("IF OBJECT_ID('dbo.Client', 'U') IS NULL SELECT 0 ELSE SELECT 1", conn);
var exists = (int)await checkCmd.ExecuteScalarAsync();
if (exists == 0)
{
    Console.WriteLine("Client table not found in database. Make sure migrations were applied.");
    return;
}

// Insert clients if not already present by name
foreach (var c in clients)
{
    var existsCmd = new SqlCommand("SELECT COUNT(1) FROM dbo.Client WHERE Name = @name", conn);
    existsCmd.Parameters.AddWithValue("@name", c.Name);
    var count = (int)await existsCmd.ExecuteScalarAsync();
    if (count > 0)
    {
        Console.WriteLine($"Client '{c.Name}' already exists, skipping.");
        continue;
    }

    var insert = new SqlCommand(@"
INSERT INTO dbo.Client (Name, Address1, Address2, City, Address3, Suburb, State, PostCode)
VALUES (@name, @a1, @a2, @city, @a3, @suburb, @state, @postcode)", conn);
    insert.Parameters.AddWithValue("@name", c.Name);
    insert.Parameters.AddWithValue("@a1", c.Address1 ?? (object)DBNull.Value);
    insert.Parameters.AddWithValue("@a2", c.Address2 ?? (object)DBNull.Value);
    insert.Parameters.AddWithValue("@city", c.City ?? (object)DBNull.Value);
    insert.Parameters.AddWithValue("@a3", c.Address3 ?? (object)DBNull.Value);
    insert.Parameters.AddWithValue("@suburb", c.Suburb ?? (object)DBNull.Value);
    insert.Parameters.AddWithValue("@state", c.State ?? (object)DBNull.Value);
    insert.Parameters.AddWithValue("@postcode", c.PostCode ?? (object)DBNull.Value);

    var rows = await insert.ExecuteNonQueryAsync();
    Console.WriteLine($"Inserted '{c.Name}' ({rows} row(s)).");
}

Console.WriteLine("Seeding complete.");

// Seed items
Console.WriteLine("Seeding items into SalesOrderDB...");
foreach (var it in items)
{
    var existsCmd = new SqlCommand("SELECT COUNT(1) FROM dbo.Item WHERE ItemCode = @code", conn);
    existsCmd.Parameters.AddWithValue("@code", it.ItemCode);
    var count = (int)await existsCmd.ExecuteScalarAsync();
    if (count > 0)
    {
        Console.WriteLine($"Item '{it.ItemCode}' already exists, skipping.");
        continue;
    }

    var insert = new SqlCommand(@"
INSERT INTO dbo.Item (ItemCode, Description, Price)
VALUES (@code, @desc, @price)", conn);
    insert.Parameters.AddWithValue("@code", it.ItemCode);
    insert.Parameters.AddWithValue("@desc", it.Description ?? (object)DBNull.Value);
    insert.Parameters.AddWithValue("@price", it.Price);

    var rows = await insert.ExecuteNonQueryAsync();
    Console.WriteLine($"Inserted item '{it.ItemCode}' ({rows} row(s)).");
}

Console.WriteLine("Item seeding complete.");
