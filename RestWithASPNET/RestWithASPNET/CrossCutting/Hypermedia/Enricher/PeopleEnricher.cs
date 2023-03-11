using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.CrossCutting.Hypermedia.Constants;
using RestWithASPNET.CrossCutting.ValueObject;
using System.Text;

namespace RestWithASPNET.CrossCutting.Hypermedia.Enricher
{
    public class PeopleEnricher : ContentResponseEnricher<PeopleValueObject>
    {
        private readonly object _lock = new object(); // por ser executado em paralelismo, e necessario bloquear para evitar problemas

        protected override Task EnrichModel(PeopleValueObject content, IUrlHelper urlHelper)
        {
            var path = "api/people/v1";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self, // self?
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PATCH,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPatch
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });

            return null;
        }

        private string GetLink(Guid id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
