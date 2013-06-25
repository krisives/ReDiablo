/* This source is part of the Cv5.x project coded in 1999 by TeLAMoN of 2XS.
   You may use this as long as proper credit is given. Algorithmic changes 
   should be reported to the author. Compiling was done with MS VC++ 5.0.
   If you want to contact the author write a mail to mickyk@cs.tu-berlin.de
   or try it per ICQ (UIN 1289212).
*/
//---------------------------------------------------------------------------
#include <memory.h>
#include "Frame.h"
//---------------------------------------------------------------------------

// Konstruktor

Frame::Frame(BYTE* BufferMem, int Dx, int Dy)
{
   FrameMem = 0;
   FrameSize = 0;
   dx = 0;
   dy = 0;
   FrameTyp = TYPE_RAW;
   if (dx > 0 && dy > 0)
   {
      FrameSize = Dx*Dy;
      dx = Dx;
      dy = Dy;
      FrameMem = new BYTE[FrameSize];
      if (BufferMem != 0)
      {
         memset(FrameMem,0,FrameSize);
         for (unsigned long i=0; i<FrameSize; i++)
         {
            FrameMem[i] = BufferMem[i];
         }
      }
   }
}


// Destruktor
Frame::~Frame()
{
	if (FrameMem != 0)
		delete FrameMem;
}

