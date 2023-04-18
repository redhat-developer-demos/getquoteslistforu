using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

namespace getquoteslistforu.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotesController : ControllerBase
{
    private readonly ILogger<QuotesController> _logger;

    public QuotesController(ILogger<QuotesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "quotes")]
    public BsonDocument Get()
    {

        // TODO: Read list from MongoDB
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        //var connectionString = "mongodb://quote:quote@quote/quote";
        if (connectionString == null)
        {
            Console.WriteLine("You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable");
            Environment.Exit(0);
        }

        var client = new MongoClient(connectionString);

        var collection = client.GetDatabase("quote").GetCollection<BsonDocument>("quote");

        var document = collection.Find(new BsonDocument()).FirstOrDefault();

        //var filter = Builders<BsonDocument>.Filter.Eq("id", 2);

        //var document = collection.Find(filter).First();

        return document;
    }
}
