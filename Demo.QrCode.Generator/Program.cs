using Demo.QrCode.Generator.Services;
using Demo.QrCode.Generator.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBlazorServer();

builder.Services.AddTransient<IQrCodeService, QrCodeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseBlazorServer();

app.MapPost("/gerar-qrcode", GerarQrCodeAsync).WithName("GerarQrCode").WithOpenApi();

app.Run();

async Task<IResult> GerarQrCodeAsync(IQrCodeService service, GerarQrCodeRequest request)
{
    const string ImageType = "image/png";

    try
    {
        byte[] qrCode = await service.GerarQrCodeAsync(request);
        return TypedResults.File(qrCode, ImageType);
    }
    catch (Exception ex)
    {
        return TypedResults.BadRequest(ex.Message);
    }
}
