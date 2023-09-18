using CSharpAdvanced.Oop.Infrastructure;
using CSharpAdvanced.Oop.Notifications;
using CSharpAdvanced.Shared;
using Microsoft.EntityFrameworkCore;

NotificationService email = new EmailService();
NotificationService sms = new SmsService();
NotificationService push = new PushNotificationService();

var services = new List<NotificationService>
{
    email,
    sms,
    push
};

foreach (var service in services) service.SendNotification("Hello, world!");

// Generics
var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseInMemoryDatabase("MyDb");
var options = optionsBuilder.Options;

var context = new AppDbContext(options);

var genericRepository = new GenericRepository<Customer>(context);

var customerRepository = new CustomerRepository(genericRepository);

var customer = new Customer("LuisDev");
var id = customerRepository.Add(customer);

var efCoreExpression = context.Customers.Where(c => c.Id == 1);

var existingCustomer = customerRepository.GetCustomerById(id);

Console.ReadKey();