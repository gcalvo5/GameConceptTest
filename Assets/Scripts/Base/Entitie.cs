using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Entitie 
{
    public CharacteristicsModel characteristics { get; set; }

    private int turnoActual;

    public Action<int> accionAlCambiarTurno = (int a) => { };

    public bool finalizarTurno = false;
    public IEnumerator EntrarTurno()
    {
        if (accionAlCambiarTurno != null)
        {
            accionAlCambiarTurno(turnoActual);
            finalizarTurno = false;
            while (!finalizarTurno)
            {
                yield return null;
            }
        }
    }
}
