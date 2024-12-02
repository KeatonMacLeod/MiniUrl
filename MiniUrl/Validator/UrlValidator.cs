namespace MiniUrl.Validator
{
    public class UrlValidator
    {
        public UrlValidator() { }

        public bool ValidateUrl(string url)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(url))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
