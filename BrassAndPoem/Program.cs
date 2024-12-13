
//create a "products" variable here to include at least five Product instances. Give them appropriate ProductTypeIds.
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Linq;

List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "Trumpet",
        Price = 150.00M,
        ProductTypeId = 1,
    },
    new Product()
    {
        Name = "Trombone",
        Price = 250.00M,
        ProductTypeId = 1,
    },
    new Product()
    {
        Name = "French Horn",
        Price = 450.00M,
        ProductTypeId = 1,
    },
    new Product()
    {
        Name = "Tuba",
        Price = 600.00M,
        ProductTypeId = 1,
    },
    new Product()
    {
        Name = "Cornet",
        Price = 200.00M,
        ProductTypeId = 1,
    },
    new Product()
    {
        Name = "Euphonium",
        Price = 500.00M,
        ProductTypeId = 1,
    },
    new Product()
    {
        Name = "The Sun and Her Flowers",
        Price = 15.99M,
        ProductTypeId = 2,
    },
    new Product()
    {
        Name = "Milk and Honey",
        Price = 12.99M,
        ProductTypeId = 2,
    },
    new Product()
    {
        Name = "Leaves of Grass",
        Price = 20.99M,
        ProductTypeId = 2,
    },
    new Product()
    {
        Name = "The Waste Land",
        Price = 10.99M,
        ProductTypeId = 2,
    },
    new Product()
    {
        Name = "Ariel",
        Price = 18.99M,
        ProductTypeId = 2,
    },
    new Product()
    {
        Name = "The Essential Rumi",
        Price = 25.99M,
        ProductTypeId = 2,
    }
};
//create a "productTypes" variable here with a List of ProductTypes, and add "Brass" and "Poem" types to the List. 
List<ProductType> productTypes = new List<ProductType>()
{
    new ProductType()
    {
        Title = "Brass",
        Id = 1
    },
    new ProductType()
    {
        Title = "Poem",
        Id = 2
    }
};
//put your greeting here

string greeting = "Welcome to Brass and Poem.";

Console.WriteLine(greeting);
string choice = null;
while (choice != "5")
{
    DisplayMenu();
}
//implement your loop here

void DisplayMenu()
{
    Console.WriteLine(@"Please choose an option:
                    1. Display all products
                    2. Delete a product
                    3. Add a new product
                    4. Update product properties
                    5. Exit");
                    
    choice = Console.ReadLine();
    if (choice == "5")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        DisplayAllProducts(products, productTypes);
    } 
    else if (choice == "2")
    {
        DeleteProduct(products, productTypes);
    } 
    else if (choice == "3")
    {
        AddProduct(products, productTypes);
    } 
    else if (choice == "4")
    {
        UpdateProduct(products, productTypes);
    } 
}

void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
{
    for (int i = 0; i < products.Count; i++)
    {
        var productType = productTypes.FirstOrDefault(prodType => prodType.Id == products[i].ProductTypeId);
        string productTypeTitle = productType != null ? productType.Title : "Unknown";
        Console.WriteLine($"{i + 1}. {products[i].Name} is {products[i].Price} dollars. Type: {productTypeTitle}");
    }
}

void DeleteProduct(List<Product> products, List<ProductType> productTypes)
{
    DisplayAllProducts(products, productTypes);

    Product chosenProduct = null;

    while (chosenProduct == null)
    {
        Console.WriteLine(@"Please enter a product number to remove:");
        try
        {
            int choice = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[choice - 1];
            products.RemoveAt(choice -1);
            Console.WriteLine("Deleted!");

            DisplayAllProducts(products, productTypes);
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a product number!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing product only!");
        }
        catch (Exception except)
        {
            Console.WriteLine(except);
            Console.WriteLine("Try again!");
        }
    }
}

void AddProduct(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("Add a new product");
    Console.WriteLine("Enter the product name:");
    string name = Console.ReadLine().Trim();
    decimal price;
    while (true)
    {
        Console.WriteLine("Enter the product price:");
        if (decimal.TryParse(Console.ReadLine().Trim(), out price) && price > 0)
        {
            break;
        }
        Console.WriteLine("Please enter a valid price.");
    }
    int productTypeId;
    while (true)
    {
        Console.WriteLine("Choose a product type:");
        for (int i = 0; i < productTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
        }
        if (int.TryParse(Console.ReadLine().Trim(), out int choice) && choice > 0 && choice <= productTypes.Count)
        {
            productTypeId = productTypes[choice -1].Id;
            break;
        }
        Console.WriteLine("Please choose a product type.");
    }
    Product newProduct = new Product
    {
        Name = name,
        Price = price,
        ProductTypeId = productTypeId
    };
    products.Add(newProduct);
    Console.WriteLine($"Product '{name}' added!");

    DisplayAllProducts(products, productTypes);
}

void UpdateProduct(List<Product> products, List<ProductType> productTypes)
{
    DisplayAllProducts(products, productTypes);

    Product chosenProduct = null;

    while (chosenProduct == null)
    {
        Console.WriteLine("Please enter the number of the product you want to update:");
        try
        {
            int choice = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[choice - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a product number!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing product only!");
        }
        catch (Exception except)
        {
            Console.WriteLine(except);
            Console.WriteLine("Try again!");
        }
    }

    Console.WriteLine($"Current Name: {chosenProduct.Name}");
    Console.WriteLine("Enter a new name:");
    string newName = Console.ReadLine().Trim();
    if (!string.IsNullOrEmpty(newName))
    {
        chosenProduct.Name = newName;
    }
    
    Console.WriteLine($"Current Price: {chosenProduct.Price}");
    Console.WriteLine("Enter a new price:");
    string priceInput = Console.ReadLine().Trim();
    if (decimal.TryParse(priceInput, out decimal newPrice) && newPrice > 0)
    {
        chosenProduct.Price = newPrice;
    }
    
    Console.WriteLine($"Current Type: {productTypes.FirstOrDefault(prodType => prodType.Id == chosenProduct.ProductTypeId)?.Title ?? "Unknown"}");
    Console.WriteLine("Choose a new type:");
    for (int i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
    }
    if (int.TryParse(Console.ReadLine().Trim(), out int typeChoice) && typeChoice > 0 && typeChoice <= productTypes.Count)
    {
        chosenProduct.ProductTypeId = productTypes[typeChoice -1].Id;
    }

    Console.WriteLine("Updated!");

    DisplayAllProducts(products, productTypes);
    
}


// don't move or change this!
public partial class Program { }