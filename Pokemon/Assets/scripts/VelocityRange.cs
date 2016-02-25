using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// UTILITY CLASS
namespace Assets.scripts
{
    [System.Serializable]
    public class VelocityRange
    {
        //PUBLIC INSTANC VARIABLES
        public float minimum;
        public float maximum;

        //CONSTUCTOR
        public VelocityRange(float minimum, float maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }
    }
}
