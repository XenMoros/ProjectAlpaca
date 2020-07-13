using UnityEngine;
using System.Text.RegularExpressions;

public class CinematicByText : CinematicManager
{// Extension del Cinematic Manager para leer las fases de entrada segun un documento de texto separado por ";"

    public TextAsset file; // Cacheo del fichero de animaciones

    internal override void Start()
    {
        base.Start(); // Start base
    }

    internal override void Update()
    {
        base.Update(); // Update base
    }

    internal override void LateUpdate()
    {
        base.LateUpdate(); // LateUpdate base
    }

    internal override void RellenarArrayAnimaciones()
    {// Override de la funcion para rellenar las fases de animacion
        string[] fLines = Regex.Split(file.text, "\n"); // Separa las lineas del fichero

        arrayAnimaciones = new AnimationGuide[fLines.Length]; // Crea la array de fases segun el numero de lineas

        for (int i = 0; i < fLines.Length; i++)
        {
            Debug.Log(gameObject.name + "  " + i);
            string valueLine = fLines[i]; // Selecciona cada una de las lineas
            string[] values = Regex.Split(valueLine, ";"); // Separa las lineas por ;

            string name = values[0]; // El nombre de animacion es el primer valor
            float time =  float.Parse(values[1]); // El tiempo es el segundo valor
            
            bool moving;
            if (int.Parse(values[2]) == 0) moving = false;
            else moving = true; // El flag de movimiento es el tercer valor, si es 0 no se mueve, si es otra cosa si

            float delay = float.Parse(values[3]); // El delay de movimiento es el 4 valor
            float finish = float.Parse(values[4]); // El fiempo de finalizar el movimiento es el ultimo valor

            arrayAnimaciones[i] = new AnimationGuide(name, time, moving, delay, finish); // Assigna la animacion segun los valores leidos
        }

    }
}