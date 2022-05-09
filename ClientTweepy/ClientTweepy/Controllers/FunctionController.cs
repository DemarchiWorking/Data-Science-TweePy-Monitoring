using Application.Service.Interfaces;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace ClientTweepy.Controllers
{
    [Route("funcoes")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFunctionService _functionService;


        public FunctionController(
            ILogger logger,
            IFunctionService functionService

            )
        {
            _logger = logger;
            _functionService = functionService;
        }


        [HttpGet("usuarios-top-seguidores")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> TopFollowersUsersList()
        {
            try
            {
                var response = _functionService.TopFollowersUsersList();

                if (response.isEmpty == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Nenhum usuário com seguidores foi encontrado!",
                        Status = 204
                    });
                }
                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de top usuários encontrada com sucesso!",
                        Status = 200,
                        List = response.List,
                        isSuccess = true
                        
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de top usuários!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionController] Exception in TopFollowersUsersList!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }
        [HttpGet("tweets-por-hora")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> TweetsGroupedByHour()
        {
            try
            {
                var response = _functionService.TweetsGroupedByHour();

                if (response.isEmpty == true)
                {
                    return Ok(new Response()
                    {
                        isSuccess = true,
                        Title = "Nenhum tweet com data de criação foi encontrado!",
                        Status = 204
                    });
                }
                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        isSuccess = true,
                        Title = "Lista tweets por hora encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        isSuccess = false,
                        Title = "Não foi possivel encontrar a lista de tweets por hora!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionController] Exception in TweetsGroupedByHour!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpGet("quantidade-tweets-por-idioma")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> NumberTweetPerLanguage()
        {
            try
            {
                var response = _functionService.NumberTweetPerLanguage();

                if (response.isEmpty == true)
                {
                    return Ok(new Response()
                    {
                        isEmpty = true,
                        isSuccess = true,
                        Title = "Nenhum tweet com idioma foi localizado!",
                        Status = 204
                    });
                }
                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        isSuccess = true,
                        Title = "Lista de quantidade de tweets por idioma encontrado com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        isSuccess = false,
                        Title = "Não foi possivel encontrar a lista de quantidade de tweets por idioma!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FunctionController] Exception in NumberTweetPerLanguage!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

    }
}
