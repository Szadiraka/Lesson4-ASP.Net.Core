
// загрузка файлов на сервер
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context)=>{
    
    var response= context.Response;
    var request= context.Request;
    string? path = request.Path.Value;
    response.ContentType = "text/html";

    if (path=="/upload" && request.Method == "POST")
    {
        var collection = request.Form.Files;

        //путь где будет храниться файл
        string uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        Directory.CreateDirectory(uploadPath);

        foreach (var file in collection)
        {
            string fullPath = $"{uploadPath}/{file.FileName}";
            using (var fs = new FileStream(fullPath,FileMode.Create))
            {
               await file.CopyToAsync(fs); 
            }

        }
        await response.WriteAsync("Files are  downloaded");
    }
    else
    {
        await response.SendFileAsync("html/index.html");

    }

});
app.Run();
