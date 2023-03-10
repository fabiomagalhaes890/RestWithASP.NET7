using RestWithASPNET.CrossCutting.Hypermedia.Abstract;

namespace RestWithASPNET.CrossCutting.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentReponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
