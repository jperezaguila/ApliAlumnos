using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using ApliAlumnos.Models;

namespace ApliAlumnos.Controllers
{
    public class AlumnosController : ApiController
    {
        private alumno02Entities db;

        public AlumnosController()
        {
            db = new alumno02Entities();
        }

        public IQueryable<Alumno> Get()
        {
            return db.Alumno;
        }

        [ResponseType(typeof (Alumno))]
        public IHttpActionResult Get(String id)
        {
            var a = db.Alumno.Find(id);
            if (a == null)
                return NotFound();
            return Ok(a);
            
        }
        //consulta por ID de Curso
        //iqueriyable icollection ilist vienen a ser lo mismo
        public ICollection<Alumno> GetbyCurso(int idCurso)
        {
            var c = db.Curso.Include("Alumno").FirstOrDefault(o => o.idCurso == idCurso);
            if (c == null)
                return null;
            return c.Alumno;
        }
        //consulta de alumnos que dan todos los cursos

        public ICollection<Alumno> GetbyProfe(int idProfesor)
        {
            var c = db.ProfesorAula.Where(o => o.idProfesor == idProfesor).Select(o => o.idCurso);
            var al = db.Curso.Where(o => c.Contains(o.idCurso)).Select(o => o.Alumno);
            var l=new List<Alumno>();
            foreach (var a  in al)
            {
                l.AddRange(a);
            }
            return l;
            

        }
        //Alta
        [ResponseType(typeof (Alumno))]
        public IHttpActionResult Post(Alumno alumno)
        {
            db.Alumno.Add(alumno);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

                return BadRequest("Error en el Alta");
            }
            return Created("ApiAlumnos", alumno);
        }

        //
        [ResponseType(typeof (Alumno))]
        public IHttpActionResult Put(Alumno al)
        {
            //busca la entrada al y pon que el estado ha sido modificado:
            db.Entry(al).State=EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

                return BadRequest();
            }
            return Ok();
        }

        //Delete:
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(String al)
        {
            //busca la entrada al y pon que el estado ha sido modificado:
            db.Entry(al).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

                return BadRequest();
            }
            return Ok();
        }


    }
}
