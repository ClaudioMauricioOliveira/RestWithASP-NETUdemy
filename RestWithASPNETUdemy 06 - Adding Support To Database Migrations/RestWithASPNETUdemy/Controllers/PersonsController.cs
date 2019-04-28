﻿using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Business;

namespace RestWithASPNETUdemy.Controllers
{
    /*
     * Mapeia as requisições de http:localhost:{porta}/api/person 
     * Por padrão o ASP .NET Core mapeia todas as classes que extendem Controller
     * pegando a primeira parte do nome da classe em lower case [Person]Controller
     * e expõe como o endpoint REST
     */
     [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //Declaração do serviço usado
        private IPersonBusiness _personBusiness;

        /*
         * Injeção de uma instancia de IPersionService ao criar
         * uma instancia de PersonController
         */
        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        // Mapeia as requisições GET para http://localhost:{porta}/api/person/
        // GET sem parâmentros para o FindAll --> Busca Todos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            
            return Ok(person);
        }

        //Mapeia as requisições POST para http://localhost:{porta}/api/person/
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return NotFound();
            return new ObjectResult(_personBusiness.Create(person));
        }

        //Mapeia as requisições PUT para http://localhost:{porta}/api/person/{id}
        //O [FromBody] consome o Objeto JSON enviado no corpo da requisição
        [HttpPut()]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            if (person == null) return NotFound();
            return new ObjectResult(_personBusiness.Update(person));
        }

        //Mapeia as requisições DELETE para http://localhost:{porta}/api/person/{id}
        //recebendo um ID como no Path da requisição
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
