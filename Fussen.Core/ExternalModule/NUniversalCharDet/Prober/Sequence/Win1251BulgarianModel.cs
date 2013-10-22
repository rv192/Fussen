﻿
namespace Mozilla.NUniversalCharDet.Prober.Sequence
{
    public class Win1251BulgarianModel : BulgarianModel
    {
        ////////////////////////////////////////////////////////////////
        // methods
        ////////////////////////////////////////////////////////////////
        public Win1251BulgarianModel()
            : base(win1251BulgarianCharToOrderMap, Constants.CHARSET_WINDOWS_1251)
        {

        }


        ////////////////////////////////////////////////////////////////
        // constants
        ////////////////////////////////////////////////////////////////
        private static short[] win1251BulgarianCharToOrderMap = new short[] {
        255,255,255,255,255,255,255,255,255,255,254,255,255,254,255,255,  //00
        255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,  //10
        253,253,253,253,253,253,253,253,253,253,253,253,253,253,253,253,  //20
        252,252,252,252,252,252,252,252,252,252,253,253,253,253,253,253,  //30
        253, 77, 90, 99,100, 72,109,107,101, 79,185, 81,102, 76, 94, 82,  //40
        110,186,108, 91, 74,119, 84, 96,111,187,115,253,253,253,253,253,  //50
        253, 65, 69, 70, 66, 63, 68,112,103, 92,194,104, 95, 86, 87, 71,  //60
        116,195, 85, 93, 97,113,196,197,198,199,200,253,253,253,253,253,  //70
        206,207,208,209,210,211,212,213,120,214,215,216,217,218,219,220,  //80
        221, 78, 64, 83,121, 98,117,105,222,223,224,225,226,227,228,229,  //90
         88,230,231,232,233,122, 89,106,234,235,236,237,238, 45,239,240,  //a0
         73, 80,118,114,241,242,243,244,245, 62, 58,246,247,248,249,250,  //b0
         31, 32, 35, 43, 37, 44, 55, 47, 40, 59, 33, 46, 38, 36, 41, 30,  //c0
         39, 28, 34, 51, 48, 49, 53, 50, 54, 57, 61,251, 67,252, 60, 56,  //d0
          1, 18,  9, 20, 11,  3, 23, 15,  2, 26, 12, 10, 14,  6,  4, 13,  //e0
          7,  8,  5, 19, 29, 25, 22, 21, 27, 24, 17, 75, 52,253, 42, 16,  //f0
    };
    }
}
