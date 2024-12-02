namespace MiniUrl.Logic
{
    public class GuidHasher : IUrlHasher
    {
        private int HashLength { get; set; }

        public GuidHasher(int hashLength = Constants.HASH_LENGTH)
        {
            this.HashLength = hashLength;
        }

        public string Hash(string url)
        {
            string hashedUrl = Guid.NewGuid().ToString();

            return new string(hashedUrl.Where(char.IsLetterOrDigit).Take(HashLength).ToArray());
        }
    }
}
