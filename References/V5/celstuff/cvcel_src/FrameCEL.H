/* This source is part of the Cv5.x project coded in 1999 by TeLAMoN of 2XS.
   You may use this as long as proper credit is given. Algorithmic changes 
   should be reported to the author. Compiling was done with MS VC++ 5.0.
   If you want to contact the author write a mail to mickyk@cs.tu-berlin.de
   or try it per ICQ (UIN 1289212).
*/
//---------------------------------------------------------------------------
#ifndef FrameCEL_H
#define FrameCEL_H
//---------------------------------------------------------------------------

#include "Frame.h"

class FrameCEL : public Frame
{
public:
	FrameCEL(BYTE* Buffer, unsigned long Length);
};

#endif // FrameCEL_H
