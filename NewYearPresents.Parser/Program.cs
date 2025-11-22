namespace NewYearPresents.Parser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //var app = builder.Build();

            //app.Run(async (context) =>
            //{
            //    var response = context.Response;
            //    var request = context.Request;
            //    if (request.Path == "/api/parse")
            //    {
            //        var message = "";   // содержание сообщения по умолчанию
            //        try
            //        {
            //            // пытаемся получить данные json
            //            var path = await request.ReadFromJsonAsync<string>();
            //            if (path != null) // если данные сконвертированы в Person
            //                message = path;
            //        }
            //        catch { }
            //        // отправляем пользователю данные
            //        await response.WriteAsJsonAsync(new XlsmParser().InitialParse(message));
            //    }
            //});

            //app.Run();
        }
    }
}