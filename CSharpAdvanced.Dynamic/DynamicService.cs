using System.Dynamic;
using Newtonsoft.Json;

namespace CSharpAdvanced.Dynamic;

internal class DynamicService
{
    public void Example1()
    {
        var jsonData = GetDataFromExternalApi();

        var parsedData = JsonConvert.DeserializeObject<dynamic>(jsonData)!;

        string name = parsedData.name;
        int age = parsedData.age;

        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Age: {age}");
    }

    private static string GetDataFromExternalApi()
    {
        var apiUrl = "http://localhost:5032/Product/v1";

        using (var client = new HttpClient())
        {
            try
            {
                var response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return json;
                }

                Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                throw new HttpRequestException($"Erro na solicitação: {response.StatusCode}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na solicitação: {ex.Message}");
                throw new HttpRequestException($"Erro na solicitação: {ex.Message}");
            }
        }
    }

    public void Example2()
    {
        var jsonData = GetDataFromExternalApiV2();

        var products = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

        foreach (var product in products)
        {
            Console.WriteLine($"Product Type: {product.type}");

            switch (product.type.ToString())
            {
                case "book":
                    Book book = JsonConvert.DeserializeObject<Book>(product.ToString());
                    Console.WriteLine($"Type: {book.Type}");
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                    break;
                case "electronics":
                    Console.WriteLine($"Name: {product.name}");
                    Console.WriteLine($"Brand: {product.brand}");
                    Console.WriteLine($"Price: ${product.price}");
                    break;
                case "clothing":
                    Console.WriteLine($"Item: {product.item}");
                    Console.WriteLine($"Size: {product.size}");
                    Console.WriteLine($"Color: {product.color}");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static string GetDataFromExternalApiV2()
    {
        var apiUrl = "http://localhost:5032/Product/v2";

        using (var client = new HttpClient())
        {
            try
            {
                var response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return json;
                }

                Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                throw new HttpRequestException($"Erro na solicitação: {response.StatusCode}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na solicitação: {ex.Message}");
                throw new HttpRequestException($"Erro na solicitação: {ex.Message}");
            }
        }
    }

    public void Example3()
    {
        dynamic person = new ExpandoObject();

        person.Name = "Alice";
        person.Age = 30;

        Console.WriteLine($"Name: {person.Name}");
        Console.WriteLine($"Age: {person.Age}");

        person.City = "New York";

        Console.WriteLine($"City: {person.City}");

        ((IDictionary<string, object>)person).Remove("City");

        Console.WriteLine($"City: {person.City}");
    }
}