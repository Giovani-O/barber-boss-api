namespace BarberBoss.Tests.Utilities.Requests.Tools;

public class StringGenerator
{
    /// <summary>
    /// Generate a random string of the specified length
    /// </summary>
    /// <param name="length">int</param>
    /// <returns>string</returns>
    public static string NewString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var randomString = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return randomString;
    }

    /// <summary>
    /// Generate a random email
    /// </summary>
    /// /// <param name="length">int</param>
    /// <returns>string</returns>
    public static string NewEmail(int length)
    {
        return $"{NewString(length)}@{NewString(length)}.com";
    }
}