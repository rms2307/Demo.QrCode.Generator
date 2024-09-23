namespace Demo.QrCode.Generator.Services
{
    public interface IQrCodeService
    {
        Task<byte[]> GerarQrCodeAsync(GerarQrCodeRequest request);
    }
}
