using System.Reflection;
using Newtonsoft.Json;

namespace CSharpAdvanced.Dynamic;

internal class AdvancedDynamic
{
    public static void MyMethod()
    {
        var json = @"{
                ""specifications"": [
                    {
                        ""Field"": ""my_name"",
                        ""Destination"": ""Name""
                    },
                    {
                        ""Field"": ""my_age"",
                        ""Destination"": ""Age""
                    }
                ],
                ""values"": [
                    {
                        ""my_name"": ""Luis"",
                        ""my_age"": 30
                    }
                ]
            }";

        var data = JsonConvert.DeserializeObject<DataObject>(json);

        var persons = new List<Person>();

        var personProperties = typeof(Person).GetProperties();

        var specificationPropertyCache = new Dictionary<string, PropertyInfo>();

        foreach (var spec in data.Specifications)
        {
            var targetProperty = personProperties.FirstOrDefault(prop => prop.Name == spec.Destination);

            if (targetProperty != null) specificationPropertyCache[spec.Field] = targetProperty;
        }

        foreach (var value in data.Values)
        {
            var person = new Person();

            foreach (var spec in data.Specifications)
                if (value.TryGetValue(spec.Field, out var fieldValue) &&
                    specificationPropertyCache.TryGetValue(spec.Field, out var targetProperty))
                {
                    var convertedValue = Convert.ChangeType(fieldValue, targetProperty.PropertyType);
                    targetProperty.SetValue(person, convertedValue);
                }

            var sayHelloMethod = typeof(Person).GetMethod("SayHello");
            sayHelloMethod.Invoke(person, null);

            var sayHelloWithParamMethod = typeof(Person).GetMethod("SayHelloWithParam");
            sayHelloWithParamMethod.Invoke(person, new object[] { "Luis" });

            persons.Add(person);
        }

        Console.Read();
    }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void SayHello()
    {
        Console.WriteLine($"SayHello(): Hello, I'm {Name}, I'm {Age} years old!");
    }

    public void SayHelloWithParam(string name)
    {
        Console.WriteLine($"SayHelloWithParam(string name): Hello, I'm {name}!");
    }
}

public class FieldSpecification
{
    public string Field { get; set; }
    public string Destination { get; set; }
}

public class DataObject
{
    public List<FieldSpecification> Specifications { get; set; }
    public List<Dictionary<string, object>> Values { get; set; }
}