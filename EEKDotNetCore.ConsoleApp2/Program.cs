// See https://aka.ms/new-console-template for more information
using EEKDotNetCore.Database.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var lst = db.TblBlogs.ToList();

var blog = new BlogModel
{
    ID = 1,
    Title = "Test Title",
    Author = "Test Author",
    Content = "Test Content",
};

/*string jsonStr = JsonConvert.SerializeObject(blog, Formatting.Indented);*/  //C# to Json
string jsonStr = blog.ToJson();  //C# to Json

Console.WriteLine(jsonStr);

string jsonStr2 = """{"ID":1,"Title":"Test Title","Author":"Test Author","Content":"Test Content"}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2);

Console.WriteLine(blog2.Title);

Console.ReadLine();

public class BlogModel
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }

}

public static class Extentions //DevCode
{
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
}