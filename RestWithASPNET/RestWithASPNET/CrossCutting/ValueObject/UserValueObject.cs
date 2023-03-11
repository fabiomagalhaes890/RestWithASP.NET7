using RestWithASPNET.CrossCutting.Hypermedia;
using RestWithASPNET.CrossCutting.Hypermedia.Abstract;

namespace RestWithASPNET.CrossCutting.ValueObject
{
    public class UserValueObject : ISupportHypermedia
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
