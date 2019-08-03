using ConectarDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SDI.Controllers
{
    public class cursosController : ApiController
    {
        private Sis_InscripcionesEntities dbContext = new Sis_InscripcionesEntities();

        [HttpGet]

        public IEnumerable<Curso> Get()
        {
            using (Sis_InscripcionesEntities sis_InscripcionesEntities = new Sis_InscripcionesEntities())
            {
                return sis_InscripcionesEntities.Cursos.ToList();
            }
        }

        // Visualiza solo un registro (api / cursos / 1)

        [HttpGet]
        public Curso Get(int id)
        {
            using (Sis_InscripcionesEntities sis_incripcionesentities = new Sis_InscripcionesEntities())
            {
                return sis_incripcionesentities.Cursos.FirstOrDefault(e => e.id_curso == id);
            }
        }


        // Graba nuevos registros en la base de datos employees

        [HttpPost]
        public IHttpActionResult AgregarCurso([FromBody] Curso cur)
        {
            if (ModelState.IsValid)
            {
                dbContext.Cursos.Add(cur);
                dbContext.SaveChanges();
                return Ok(cur);
            }

            else
            {
                return BadRequest();

            }
        }

        [HttpPut]
        public IHttpActionResult ActualizarCurso(int id, [FromBody] Curso cur)
        {
            if (ModelState.IsValid)
            {
                var CursoExiste = dbContext.Cursos.Count(c => c.id_curso == id) > 0;

                if (CursoExiste)
                {
                    dbContext.Entry(cur).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return Ok();

                }

                else
                {
                    return NotFound();
                }

            }

            else
            {
                return BadRequest();

            }
        }


        // Borra un registro (api/employees/1)

        [HttpDelete]
        public IHttpActionResult EliminarCurso(int id)
        {
            var cur = dbContext.Cursos.Find(id);

            if (cur != null)
            {
                dbContext.Cursos.Remove(cur);
                dbContext.SaveChanges();

                return Ok(cur);
            }

            else
            {
                return NotFound();

            }
        }

    }
}
