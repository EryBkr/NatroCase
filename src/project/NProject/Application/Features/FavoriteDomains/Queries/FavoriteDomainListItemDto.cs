namespace Application.Features.FavoriteDomains.Queries
{
    public sealed class FavoriteDomainListItemDto
    {
        public Guid Id { get; set; }
        public string Domain { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastUpdate { get; init; }
    }
}
