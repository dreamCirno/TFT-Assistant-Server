using System.Data;
using Dapper;
using Grpc.Core;
using MySql.Data.MySqlClient;
using Tft;
using TFT_Assistant_Server.Services;

class Program
{
    public static IDbConnection Connection;
    public static bool IsDatabaseConnected { get; private set; }

    private const string DbServerName = "localhost";
    private const string DbServerPort = "3306";
    private const string DbServerUser = "root";
    private const string DbServerPassword = "BABY0311";
    private const string DbServerDatabase = "tft";

    public static void Main(string[] args)
    {
        ConnectToDatabase();
        PrepareForDatabase();

        var server = new Server
        {
            Services = {TftAssistant.BindService(new TftService())},
            Ports = {new ServerPort("localhost", 50051, ServerCredentials.Insecure)}
        };
        server.Start();

        Console.WriteLine("RouteGuide server listening on port " + 50051);
        Console.WriteLine("Press any key to stop the server...");
        Console.ReadKey();

        server.ShutdownAsync().Wait();
    }

    /// <summary>
    /// 连接数据库
    /// </summary>
    private static void ConnectToDatabase()
    {
        Connection = new MySqlConnection(
            $"Server={DbServerName};Port={DbServerPort};User={DbServerUser};Password={DbServerPassword};Database={DbServerDatabase}");
        try
        {
            Connection.Open();
            IsDatabaseConnected = true;
            Console.WriteLine("数据库已建立连接");
        }
        catch (Exception e)
        {
            IsDatabaseConnected = false;
            Console.WriteLine(e.Message);
            Connection.Close();
            Console.WriteLine("数据库连接已关闭");
        }
    }

    private static void PrepareForDatabase()
    {
        // var origins = Connection
        //     .Query<Origin>("SELECT originClassId as 'id', name, description FROM OriginClass where isOrigin = 1")
        //     .ToList();
        // foreach (var origin in origins)
        // {
        //     Console.WriteLine(origin.Name);
        // }

        var heroes = Connection.Query<Hero>("SELECT heroId as 'Id', name, cost FROM hero where seasonId = 7").ToList();
        foreach (var hero in heroes)
        {
            Console.WriteLine(hero.Name);
        }
    }
}