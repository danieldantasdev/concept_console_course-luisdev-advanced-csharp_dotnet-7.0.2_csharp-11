using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http.Json;
using CSharpAdvanced.AsyncAwait;
using CSharpAdvanced.Oop.Infrastructure;
using CSharpAdvanced.Shared;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

Console.WriteLine(Process.GetCurrentProcess().Threads.Count);

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseInMemoryDatabase("MyDb");
var options = optionsBuilder.Options;

var context = new AppDbContext(options);

var genericRepository = new GenericRepository<Customer>(context);

// Async Await


Console.WriteLine("Começando com Async Await");

var customer = new Customer("LuisDev");
var thread = new Thread(() => genericRepository.InsertAsync(customer).Wait());
thread.Start();

var task = Task.Run(() => genericRepository.InsertAsync(customer));
//task.Wait();
await task;

genericRepository.Insert(customer);
genericRepository.Insert(customer);
genericRepository.Insert(customer);

var customerFromDbNoAwaitTask = genericRepository.GetByIdAsync(customer.Id);
var customerFromDbAwait = await genericRepository.GetByIdAsync(customer.Id);

var customerFromDbNoAwait = customerFromDbNoAwaitTask.Result; // Bloqueia a thread

var client = new HttpClient();

// Task Continuation 
Console.WriteLine("Começando com Task Continuation");
var result = client.GetFromJsonAsync<List<User>>("https://63178ecbece2736550b65df3.mockapi.io/api/v1/users")
    .ContinueWith(response =>
    {
        if (response.Status == TaskStatus.RanToCompletion)
        {
            var users = response.Result;
            ProcessUsers(users);
        }
        else if (response.Status == TaskStatus.Faulted)
        {
            Exception exception = response.Exception;
            Console.WriteLine($"An error occurred: {exception}");
        }
    });

result.Wait();

var resultAsync = await client.GetFromJsonAsync<List<User>>("https://63178ecbece2736550b65df3.mockapi.io/api/v1/users");

ProcessUsers(resultAsync);

//var forceResult = client.GetFromJsonAsync<List<User>>("https://63178ecbece2736550b65df3.mockapi.io/api/v1/users").Result;
var resultAwaiting = await client
    .GetFromJsonAsync<List<User>>("https://63178ecbece2736550b65df3.mockapi.io/api/v1/users")
    .ConfigureAwait(false);
// https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.configureawait?view=net-7.0


void ProcessUsers(List<User> users)
{
    foreach (var user in users) Console.WriteLine($"User: {user.Name}, Id: {user.Id}");
}

//if (resultAwaiting is null)
//    return;

// Semaphore
Console.WriteLine("Começando com Semaphore");

var _semaphore = new SemaphoreSlim(10); // Limiting to 10 concurrent operations
var _usersToProcess = new ConcurrentQueue<User>();
var _processedUsers = new ConcurrentBag<User>();

foreach (var user in resultAwaiting) _usersToProcess.Enqueue(user);

var tasks = new List<Task>();
for (var i = 0; i < resultAwaiting.Count; i++) tasks.Add(ProcessUserAsync());

await Task.WhenAll(tasks);

async Task ProcessUserAsync()
{
    await _semaphore.WaitAsync();

    try
    {
        if (_usersToProcess.TryDequeue(out var user))
        {
            // Verification & Transformation (As an example, we just delay for simulation)
            await Task.Delay(TimeSpan.FromSeconds(1));

            Console.WriteLine(user.Name);

            // Simulating adding the processed user to a concurrent collection
            _processedUsers.Add(user);
        }
    }
    finally
    {
        _semaphore.Release();
    }
}

// Lock
Console.WriteLine("Começando com Lock");

var lockObject = new object();
var customersToProcessList = new List<Customer>
    { new("Cassiano"), new("Anthony"), new("Ramon") }; // Replace with your collection

var tasksLocked = new List<Task>();
var sharedCustomers = new List<Customer>();

for (var i = 0; i < customersToProcessList.Count; i++) tasksLocked.Add(ProcessUserLockAsync(customersToProcessList[i]));

await Task.WhenAll(tasksLocked);

Task ProcessUserLockAsync(Customer customer)
{
    if (genericRepository is null)
        return Task.CompletedTask;

    lock (lockObject)
    {
        sharedCustomers.Add(customer); // Idealmente, usar coleções concorrentes
    }

    genericRepository.Insert(customer);

    return Task.CompletedTask;
}

genericRepository.Save();

var allCustomers = genericRepository.GetAll();

foreach (var item in allCustomers) Console.WriteLine(item.Name);

Console.ReadKey();

namespace CSharpAdvanced.AsyncAwait
{
    public class User
    {
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Id { get; set; }
    }
}