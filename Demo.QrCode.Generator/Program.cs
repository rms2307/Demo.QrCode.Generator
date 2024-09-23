using QRCoder;
using SixLabors.ImageSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/gerar-qrcode", GerarQrCodeAsync).WithName("GerarQrCode").WithOpenApi();

app.Run();

async Task<IResult> GerarQrCodeAsync(GerarQrCodeRequest request)
{
    const string ImageType = "image/png";

    try
    {
        const int pixelsPerModule = 25;
        QRCodeData qrCodeData;
        using (QRCodeGenerator qrGenerator = new())
        {
            qrCodeData = qrGenerator.CreateQrCode(request.Url, QRCodeGenerator.ECCLevel.Q);
        }

        var qrCode = new Base64QRCode(qrCodeData);
        string qrCodeImageAsBase64 = qrCode.GetGraphic(pixelsPerModule, Color.Black, Color.White);

        byte[] qrCodeBytes = Convert.FromBase64String(qrCodeImageAsBase64);

        await Task.Delay(600);
        return TypedResults.File(qrCodeBytes, ImageType);
    }
    catch
    {
        throw;
    }
}
