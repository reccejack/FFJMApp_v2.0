using System;
using System.Collections;
using System.Collections.Generic;

namespace FreeFallJumpmasterApp
{
    public class ParachuteSystem
    {
        public string SystemName { get; set; }
        public double FREEFALL_K_FACTOR { get; set; }
        public double CANOPY_K_FACTOR { get; set; }
        //public double FORWARD_SPEED { get; set; }

        public ParachuteSystem(string systemName, double freefallKfactor, double canopyKfactor)
        {
            SystemName = systemName;
            FREEFALL_K_FACTOR = freefallKfactor;
            CANOPY_K_FACTOR = canopyKfactor;
            //FORWARD_SPEED = forwardSpeed;
        }

        public ParachuteSystem(string systemName, double freefallKfactor)
        {
            SystemName = systemName;
            FREEFALL_K_FACTOR = freefallKfactor;
        }

    }
}
