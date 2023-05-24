using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization;

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

    [HttpGet]
    public async Task<List<object>> Get()
    {

        // Read list from MongoDB
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        //var connectionString = "mongodb://quote:quote@quote/quote";
        if (connectionString == null)
        {
            Console.WriteLine("You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable");
            Environment.Exit(0);
        }
        var client = new MongoClient(connectionString);
        var collection = client.GetDatabase("quote").GetCollection<BsonDocument>("quote");
        List<BsonDocument> documentList = await collection.Find(new BsonDocument()).ToListAsync();
        var dotNetObjList = documentList.ConvertAll(BsonTypeMapper.MapToDotNetValue);    
        String s = String.Format("Returning JSON list containing {0} objects", dotNetObjList.Count);
        Console.WriteLine(s);
        return dotNetObjList;
    }

    [HttpGet]
    [Route("/quotes/random")]
    public async Task<Quote> GetRandom() {
        // Read list from MongoDB
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        //var connectionString = "mongodb://quote:quote@quote/quote";
        if (connectionString == null)
        {
            Console.WriteLine("You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable");
            Environment.Exit(0);
        }
        var client = new MongoClient(connectionString);
        var collection = client.GetDatabase("quote").GetCollection<BsonDocument>("quote");
        var documentList = await collection.Find(new BsonDocument()).ToListAsync();
        //var dotNetObjList = documentList.ConvertAll(BsonTypeMapper.MapToDotNetValue);

        //List<Quote> listOfQuotes = dotNetObjList.Select(v => BsonSerializer.Deserialize<Quote>(v).ToList());

        String s = String.Format("Returning JSON list containing {0} objects", documentList.Count);
        Console.WriteLine(s);
        // get random entry
        int mx = documentList.Count;
        Random rnd = new Random();
        int x = rnd.Next(0,mx);
        Quote q = new Quote();
        q.quoteID = 555;
        q.author = "not me";
        q.quotation = "you just ruined this, Don";

        q = BsonSerializer.Deserialize<Quote>(documentList[x]);

        return q;
    }
}
