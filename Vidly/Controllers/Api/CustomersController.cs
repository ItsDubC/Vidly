using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers()  
        {
            var customerDtos = _context.Customers.Include(x => x.MembershipType).ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        // GET api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return NotFound();
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return Ok(Mapper.Map<Customer, CustomerDto>(customer));
                //return Mapper.Map<Customer, CustomerDto>(customer);
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var newCustomer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            customerDto.Id = newCustomer.Id;

            //return customerDto;
            return Created(new Uri(Request.RequestUri + "/" + newCustomer.Id), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)  // can return Customer as well
        {
            if (!ModelState.IsValid)
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customerInDb == null)
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);
            //customerInDb.Name = customerDto.Name;
            //customerInDb.BirthDate = customerDto.BirthDate;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customerInDb == null)
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
