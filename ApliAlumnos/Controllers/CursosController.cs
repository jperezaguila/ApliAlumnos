using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Description;
using ApliAlumnos.Models;

namespace ApliAlumnos.Controllers
{
    public class CursosController : ApiController
    {
        private alumno02Entities db;

        public CursosController()
        {
            db = new alumno02Entities();
          
        }

        public IQueryable<Curso> Get()
        {
            return db.Curso;
        }

        [ResponseType(typeof (Curso))]

        public IHttpActionResult GetPorId(int id)
        {
            var data = db.Curso.Find(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

    }
}
