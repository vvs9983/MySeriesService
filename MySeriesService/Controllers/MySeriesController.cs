using Castle.Core.Logging;
using MySeriesService.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace MySeriesService.Controllers
{
    [Route("api/series")]
    public class MySeriesController : ApiController
    {
        private readonly ISeriesLogic _seriesLogic;
        private ILogger _logger { get; set; }

        public MySeriesController(ISeriesLogic seriesLogic)
        {
            if (seriesLogic == null)
            {
                throw new ArgumentNullException(nameof(seriesLogic));
            }

            _seriesLogic = seriesLogic;
        }

        [HttpGet, Route("api/series/{series}")]
        public async Task<IHttpActionResult> GetSeriesElement(string series, int? n = null)
        {
            if (n == null)
            {
                var message = "Must pass integer 'n' value as query parameter";
                return BadRequest(message);
            }

            try
            {
                return Json(new { series, index = n, result = await _seriesLogic.Evaluate(series, n.Value) });
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString());

                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetSeriesEvaluators()
        {
            try
            {
                return Json(new { series = await _seriesLogic.GetSeriesEvaluators() });
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString());

                return BadRequest(ex.ToString());
            }
        }
    }
}
