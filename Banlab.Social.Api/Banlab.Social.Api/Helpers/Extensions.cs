namespace Banlab.Social.Api.Helpers
{
    public static class Extensions
    {
        public static string GetPartitionKey(this string Id)
        {
            var segments = Id.Split('-');
            var partitionKey = segments[1];

            if (segments.Length < 2)
            {
                throw new InvalidOperationException("Unable to get partition key");
            }
            return partitionKey.ToString();
        }
    }
}
