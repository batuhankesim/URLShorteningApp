namespace URLShorteningAPI.DataTransferObjects
{
    public class DTOUrl
    {
        public string? Url { get; set; }

        public string? ShortUrl { get; set; }
        public bool IsAutoGenerate { get; set; }
        public DTOUrl() { }
    }
}
