using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Services.Implementatitons
{
    public class PersonServiceImpl : IPersonService
    {

        private MySQLContext _context;
        public PersonServiceImpl(MySQLContext context)
        {
            _context = context;
        }

        // Metodo responsável por criar uma nova pessoa
        // nesse momento adicionamos o objeto ao contexto
        // e finalmente salvamos as mudanças no contexto
        // na base de dados
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Método responsável por retornar todas as pessoas
        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        // Método responsável por retornar uma pessoa
        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person)
        {
            if (!Exist(person.Id))
            {
                return new Person();
            }

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;

        }

        private bool Exist(long? id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
