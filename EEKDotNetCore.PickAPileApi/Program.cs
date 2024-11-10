using Newtonsoft.Json;
using System.Reflection.Metadata;

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
    return Results.Ok(result);
})
.WithName("GetPickAPile")
.WithOpenApi();

app.MapGet("/pickapile/{id}", (int id) =>
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

app.MapPost("/pickapile", (QuestionModel requestModel) =>
{
    string folderPath = "Data/PickAPile.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<PickAPileresponseModel>(jsonStr)!;

    requestModel.QuestionId = result.Questions.Count == 0 ? 1 : result.Questions.Max(x => x.QuestionId) + 1;
    result.Questions.Add(requestModel);

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(requestModel);
})
.WithName("CreateQuestion")
.WithOpenApi();

app.MapPut("/pickapile/{id}", (int id, QuestionModel requestModel) =>
{
    string folderPath = "Data/PickAPile.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<PickAPileresponseModel>(jsonStr)!;

    var item = result.Questions.FirstOrDefault(x => x.QuestionId == id);
    if (item is null) return Results.BadRequest("No data found.");

    item.QuestionName = requestModel.QuestionName;
    item.QuestionDesp = requestModel.QuestionDesp;

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(item);
})
.WithName("UpdateQuestion")
.WithOpenApi();

app.MapPatch("/pickapile/{id}", (int id, QuestionModel requestModel) =>
{
    string folderPath = "Data/PickAPile.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<PickAPileresponseModel>(jsonStr)!;

    var item = result.Questions.FirstOrDefault(x => x.QuestionId == id);
    if (item is null) return Results.BadRequest("No data found.");

    if (!string.IsNullOrEmpty(requestModel.QuestionName))
    {
        item.QuestionName = requestModel.QuestionName;
    }
    if (!string.IsNullOrEmpty(requestModel.QuestionDesp))
    {
        item.QuestionDesp = requestModel.QuestionDesp;
    }

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(item);
})
.WithName("PatchQuestion")
.WithOpenApi();

app.MapDelete("/pickapile/{id}", (int id) =>
{
    string folderPath = "Data/PickAPile.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<PickAPileresponseModel>(jsonStr)!;

    var item = result.Answers.FirstOrDefault(x => x.AnswerId == id);
    if (item is null) return Results.BadRequest("No data found.");

    result.Answers.Remove(item);

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok("Deleted");
})
.WithName("DeleteAnswer")
.WithOpenApi();
app.Run();



public class PickAPileresponseModel
{
    public List<QuestionModel> Questions { get; set; }
    public List<AnswerModel> Answers { get; set; }
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

