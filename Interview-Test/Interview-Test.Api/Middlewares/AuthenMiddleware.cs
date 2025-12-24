using System.Security.Cryptography;
using System.Text;

namespace Interview_Test.Middlewares;

public class AuthenMiddleware : IMiddleware
{
    // Hashed value of "interview-test-2024" using SHA512
    private const string hashedKey = "2C6EB66208C915145193BF1807494A7A6C3D5F0953907893A311974FAB6412FADEB3F98B38221CDB7FE11D819EB0CCFCB15C646BF8449AAD2DDDAE5F92E25F0D";
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var apiKeyHeader = context.Request.Headers["x-api-key"];
        if (string.IsNullOrEmpty(apiKeyHeader))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }
        
        // Hash the incoming x-api-key using SHA512
        var hashedApiKey = HashApiKey(apiKeyHeader!);
        
        // Compare the hashed key with the stored hashed key
        if (!string.Equals(hashedApiKey, hashedKey, StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        
        // If valid, proceed to the next middleware
        await next(context);
    }
    
    private static string HashApiKey(string apiKey)
    {
        using var sha512 = SHA512.Create();
        var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
        return Convert.ToHexString(hashBytes);
    }
}