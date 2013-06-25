/* This source is part of the Cv5.x project coded in 1999 by TeLAMoN of 2XS.
   You may use this as long as proper credit is given. Algorithmic changes 
   should be reported to the author. Compiling was done with MS VC++ 5.0.
   If you want to contact the author write a mail to mickyk@cs.tu-berlin.de
   or try it per ICQ (UIN 1289212).
*/
//---------------------------------------------------------------------------
#include <memory.h>
#include <math.h>
#include "FrameCEL.h"
//---------------------------------------------------------------------------

// Konstruktor

FrameCEL::FrameCEL(BYTE* BufferMem, unsigned long Size)
:Frame(0,0,0)
{
		FrameTyp                = TYPE_CEL;
      dx                      = 0;
		dy                      = 0;

      unsigned int x          = 0;
		unsigned long c         = 0;
		BYTE h                  = 0;

/* first calculate the needed buffer-size (using dy for that var) */

		if ((BYTE)BufferMem[c] > 0x10 && (BYTE)BufferMem[c] < 0x80)
		{
			// regelmäßig
         while (c<Size)
         {
				while ((BYTE)BufferMem[c] == 127)
				{
					dy = dy +  127;
					c = c + 128;
				}
            dy = dy + BufferMem[c];
				c = c + BufferMem[c] + 1;
         }
      }
		else
		{
		// unregelmässig
			if ((unsigned char)BufferMem[c] < 0x10)
				c = c + BufferMem[c];
			while (c < Size)
			{
				BYTE in = BufferMem[c++];
				if (in >= 0x80)
				{
					dy = dy + 0x100-in;
					continue;
				}
				if (in < 0x80)
				{
					dy = dy + in;
					c = c + in;
					continue;
				}
			}

		}

/* and now lets fill the buffer */

		c        = 0;
		x        = 0;
		FrameMem = 0;
      unsigned int block      = 0;
      unsigned int blockpos   = 0;
		
      if (dy > 0)
		{
			FrameMem = new BYTE [dy];
			memset (FrameMem, 67,dy);

			if ((BYTE)BufferMem[c] > 0x10 && (BYTE)BufferMem[c] < 0x80)
			{
				// regelmäßig
            int add = 0;
				do
				{
					int breite=(BYTE)BufferMem[c++];
					for (int i=0; i<breite; i++)
					{
							FrameMem[add++] = BufferMem[c++];
					}
				   // let's set the dx-dimension to the size of the first row
               if (breite < 127 && dx == 0)
                  dx = add;
				}while (c<Size);
            // dy is easy to calculate...
            dy = dy / dx;
            
			}
			else
			{
		// unregelmäßig

				if ((BYTE)BufferMem[c] < 0x10)
            {

               blockpos = (BYTE)BufferMem[2] + (BYTE)BufferMem[3]*256;
					c=c+(BYTE)BufferMem[c];
            }

				while (c < (Size))
				{
					if (c == blockpos)
                  block = x;
               h = BufferMem[c++];
					if (h  >= 0x80)
					{
						x = x + (0x100-h);
						continue;
					}
					if (h  < 0x80)
					{
						for (int i=0; i<h; i++)
						{
							FrameMem[x++] = BufferMem[c++];
						}
						continue;
					}
			   }
            
            // ok, let's try to calculate the dimensions
            // if we have the frame blocked, we can calculate the dimensions
            if (block != 0)
            {
               dx = block / 32;
               dy = dy / dx;
            }
            else
            {
            // if not, we have to aproximate with best cases...
               unsigned int w = (unsigned int)sqrt(dy);
               while ((dy%w) != 0)
                  w--;
               dx = dy / w;
               dy = w;
            }
		}
	}		// Ende CEL
   FrameSize = dx*dy;
}

