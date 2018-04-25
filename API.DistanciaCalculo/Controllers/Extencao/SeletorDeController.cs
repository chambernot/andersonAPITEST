using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace API.DistanciaCalculo.Controllers.Extensao
{
    public class SeletorDeController : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly IEnumerable<Type> _controllerTypes;


        public SeletorDeController(HttpConfiguration configuration, Assembly assembly) : base(configuration)
        {
            _configuration = configuration;
            _controllerTypes = assembly.GetTypes()
                .Where(i => typeof(IHttpController).IsAssignableFrom(i));
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllerName = GetControllerName(request);
            var version = request.GetRouteData().Values["version"]?.ToString();

            var matchedController =
                _controllerTypes.SingleOrDefault(i =>
                    Regex.IsMatch(i.FullName, $"(Controllers[.]{version}[.])(?:\\w+[.])*?({controllerName}Controller$)",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant));

            if (matchedController == null)
                return base.SelectController(request);

            return new HttpControllerDescriptor(_configuration, controllerName, matchedController);
        }

    }
}