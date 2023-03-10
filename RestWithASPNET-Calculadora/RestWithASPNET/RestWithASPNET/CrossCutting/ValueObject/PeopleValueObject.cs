using RestWithASPNET.CrossCutting.Hypermedia;
using RestWithASPNET.CrossCutting.Hypermedia.Abstract;

namespace RestWithASPNET.CrossCutting.ValueObject
{
    public class PeopleValueObject : ISupportHypermedia
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<HyperMediaLink> Links { get ; set; } = new List<HyperMediaLink>();
    }
}
