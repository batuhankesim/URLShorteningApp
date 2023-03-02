namespace URLShorteningAPI.Models
{
    public class UrlModel
    {
        public long Id { get; set; }

        public string? Url { get; set; }

        public string? ShortenedUrl { get; set; }

    }
}
