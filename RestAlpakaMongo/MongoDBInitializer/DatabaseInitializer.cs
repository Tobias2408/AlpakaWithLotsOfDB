using MongoDB.Driver;
using RestAlpakaMongo.Models;

namespace RestAlpakaMongo.MongoDBInitializer;

public class DatabaseInitializer
{
    private readonly IMongoClient _mongoClient;

    public DatabaseInitializer(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public void Initialize()
    {
        CreateBookingDateIndex();
        CreateCustomerFirstNameIndex();
        CreateAlpakaNameIndex();
        CreateEventDateIndex();
        CreateCompoundIndexForCustomers();
        CreateCompoundIndexForLocation();

    }

    private void CreateBookingDateIndex()
    {
        var database = _mongoClient.GetDatabase("Alpaka");
        var collection = database.GetCollection<Booking>("Bookings");
        var indexKeysDefinition = Builders<Booking>.IndexKeys.Ascending(booking => booking.Booking_date);
        collection.Indexes.CreateOne(new CreateIndexModel<Booking>(indexKeysDefinition));
    }
    
    private void CreateCustomerFirstNameIndex()
    {
        var database = _mongoClient.GetDatabase("Alpaka");
        var customersCollection = database.GetCollection<Customers>("Customers");
        var indexKeysDefinition = Builders<Customers>.IndexKeys.Ascending(customer => customer.First_name);
        customersCollection.Indexes.CreateOne(new CreateIndexModel<Customers>(indexKeysDefinition));
    }

    private void CreateAlpakaNameIndex()
    {
        var database = _mongoClient.GetDatabase("Alpaka");
        var alpakaCollection = database.GetCollection<Alpaka>("Alpakas");
        var indexKeysDefinition = Builders<Alpaka>.IndexKeys.Ascending(alpaka => alpaka.AlpakaName);
        alpakaCollection.Indexes.CreateOne(new CreateIndexModel<Alpaka>(indexKeysDefinition));
    }
    private void CreateEventDateIndex()
    {
        var database = _mongoClient.GetDatabase("Alpaka");
        var eventCollection = database.GetCollection<Event>("Events");
        var indexKeysDefinition = Builders<Event>.IndexKeys.Ascending(eEvent => eEvent.EventDate);
        eventCollection.Indexes.CreateOne(new CreateIndexModel<Event>(indexKeysDefinition));
    }
    private void CreateCompoundIndexForCustomers()
    {
        var database = _mongoClient.GetDatabase("your_database_name");
        var customersCollection = database.GetCollection<Customers>("Customers");

        var indexKeysDefinition = Builders<Customers>.IndexKeys
            .Ascending(customer => customer.First_name)
            .Ascending(customer => customer.Last_name);

        customersCollection.Indexes.CreateOne(new CreateIndexModel<Customers>(indexKeysDefinition));
    }
    private void CreateCompoundIndexForLocation()
    {
        var database = _mongoClient.GetDatabase("your_database_name");
        var locationCollection = database.GetCollection<Location>("Locations");

        var indexKeysDefinition = Builders<Location>.IndexKeys
            .Ascending(location => location.City)
            .Ascending(location => location.Postalcode);

        locationCollection.Indexes.CreateOne(new CreateIndexModel<Location>(indexKeysDefinition));
    }
}