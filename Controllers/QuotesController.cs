using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

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
        List<BsonDocument> documentList = await collection.Find(new BsonDocument()).ToListAsync();
        var dotNetObjList = documentList.ConvertAll(BsonTypeMapper.MapToDotNetValue);
        String s = String.Format("Returning JSON list containing {0} objects", dotNetObjList.Count);
        Console.WriteLine(s);
        // get random entry
        int mx = dotNetObjList.Count;
        Random rnd = new Random();
        int x = rnd.Next(0,mx-1);
        Quote q = new Quote();
        q = JsonConvert.DeserializeObject<Quote>(dotNetObjList[mx].ToJson());
        return q;
    }
}
