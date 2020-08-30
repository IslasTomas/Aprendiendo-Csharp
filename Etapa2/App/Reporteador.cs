using System.Collections.Generic;
using CorEscuela.Entidades;
using System;
using System.Linq;


namespace CorEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;// el _ es una convencion para indicar q es privado
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
        {
            if (dicObjEsc == null) // comprobamos q el parametro no sea null
            {
                Console.WriteLine("--------------------\nInforma  por medio de un error que el diccionario es NULL");
                throw new ArgumentNullException(nameof(dicObjEsc));
                //throw da una informe de error
            }

            _diccionario = dicObjEsc;
        }
        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            var lista = _diccionario.GetValueOrDefault(LlaveDiccionario.Evaluaciones);//develve el valor especificado, sino lo encuentra retorna null
            var respuesta = _diccionario.TryGetValue(LlaveDiccionario.Evaluaciones, out IEnumerable<ObjetoEscuelaBase> listado);
            //TryGetValue va a devolver un booleano, si la lista existe en el dicc va a ser true y en el output (salida) nos da la lista
            // si no encontro la lista devuelve false
            //return listado.Cast<Evaluacion>(); de esta forma retornamos el output de TryGetValue

            //mejor forma de implementacion
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluaciones, out IEnumerable<ObjetoEscuelaBase> listado2))
            {
                return listado2.Cast<Evaluacion>();
            }
            else
            {
                Console.WriteLine($"la LLave {LlaveDiccionario.Evaluaciones} esta vacia");
                return new List<Evaluacion>();
            }

        }
        public IEnumerable<string> GetListaMaterias()
        {
            var listaEvaluaciones = GetListaEvaluaciones();
         //   return (from ev in listaEvaluaciones  //esto lo hacemos con Linq(nos permite hacer consultas similares a un query)
          //          select ev.Materia.Nombre);    //esto de cada "ev" selecciona el nombre de la materia q corresponde
            return (from ev in listaEvaluaciones
                    //Where  ev.Nota > 3.0f  (con ese where podemos filtrar em cada parametro de "ev" 
                    select ev.Materia.Nombre).Distinct(); // con el funcion Distinct solo va a devolver "ev.materia.nombre" que sean distintos

        }


    }
}
