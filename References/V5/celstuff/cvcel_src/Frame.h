/* This source is part of the Cv5.x project coded in 1999 by TeLAMoN of 2XS.
   You may use this as long as proper credit is given. Algorithmic changes 
   should be reported to the author. Compiling was done with MS VC++ 5.0.
   If you want to contact the author write a mail to mickyk@cs.tu-berlin.de
   or try it per ICQ (UIN 1289212).
*/
//---------------------------------------------------------------------------
#ifndef Frame_H
#define Frame_H

#ifndef BYTE
#define BYTE unsigned char
#endif

//---------------------------------------------------------------------------

#define TYPE_CEL     0x01
#define TYPE_CL2     0x02
#define TYPE_GRP     0x03
#define TYPE_GIF     0x04
#define TYPE_RAW     0x05
#define TYPE_UNKNOWN 0x0F

class Frame
{
public:
   Frame(BYTE* Buffer, int Dx, int Dy);
	~Frame();

// Methoden
	int           GetTyp(){return FrameTyp;};
   int           GetFrameX(){return dx;};
	int           GetFrameY(){return dy;};
	void          SetFrameX(int X){dx = X;};
	void          SetFrameY(int Y){dy = Y;};
   BYTE*         GetFrameMem(){return FrameMem;};
   unsigned long GetFrameSize(){return FrameSize;};
// Daten

protected:
// Methoden



// Daten
   int            FrameTyp;
   BYTE*          FrameMem;
   unsigned long  FrameSize;
   unsigned long  dx;
   unsigned long  dy;
};

#endif // Frame_H
