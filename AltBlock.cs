using System;
using System.Collections;
using System.Collections.Generic;

namespace FreeFallJumpmasterApp
{
    public class AltBlock
    {
        public int AltitudeBlock { get; set; }
        public int WindDirection { get; set; }
        public int WindVelocity { get; set; }

        public AltBlock() { }
        public AltBlock(int altitudeBlock, int windDirection, int windVelocity)
        {
            this.AltitudeBlock = altitudeBlock;
            this.WindDirection = windDirection;
            this.WindVelocity = windVelocity;

        }
    }
}