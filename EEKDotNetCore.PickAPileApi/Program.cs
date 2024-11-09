using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/pickapile", () =>
{
    string folderPath = "Data/PickAPile.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<PickAPileresponseModel>(jsonStr)!;
    return Results.Ok(result.Questions);
})
.WithName("GetQuestions")
.WithOpenApi();

app.MapGet("/pickapile/answers/{id}", (int id) =>
{
    string folderPath = "Data/PickAPile.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<PickAPileresponseModel>(jsonStr)!;

    var item = result.Answers.FirstOrDefault(x => x.AnswerId == id);
    if (item is null) return Results.BadRequest("No data found.");

    return Results.Ok(item);
})
.WithName("GetAnswer")
.WithOpenApi();

app.Run();


public class PickAPileresponseModel
{
    public QuestionModel[] Questions { get; set; }
    public AnswerModel[] Answers { get; set; }
}

public class QuestionModel
{
    public int QuestionId { get; set; }
    public string QuestionName { get; set; }
    public string QuestionDesp { get; set; }
}

public class AnswerModel
{
    public int AnswerId { get; set; }
    public string AnswerImageUrl { get; set; }
    public string AnswerName { get; set; }
    public string AnswerDesp { get; set; }
    public int QuestionId { get; set; }
}

